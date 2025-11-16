<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import DataGrid from '@/components/DataGrid.vue'
import DialogForm from '@/components/DialogForm.vue'

const props = withDefaults(
  defineProps<{
    title: string
    headers: any[]
    idKey?: string
    loading?: boolean
    create: (data: any) => Promise<any> | any
    update: (id: any, data: any) => Promise<any> | any
    remove: (id: any) => Promise<any> | any

    bikes?: any[]
    manufacturers?: any[]
    users?: any[]
    filters?: any[]
  }>(),
  {
    idKey: 'id',
    loading: false,
    bikes: () => [],
    manufacturers: () => [],
    users: () => [],
    filters: () => [],
  },
)

const emit = defineEmits<{
  (e: 'refresh'): void
}>()

// Selected row in the data grid
const selected = ref<any | null>(null)

// Dialog state and mode (add vs. edit)
const dialog = ref({
  open: false,
  editing: false,
})

// Global error and saving flag for CRUD operations
const error = ref<string | null>(null)
const saving = ref(false)

// Active values for all filters (selects, ranges, text fields)
const activeFilters = ref<Record<string, any>>({})

// Initialize filter values whenever the filter configuration changes
watch(
  () => props.filters,
  filters => {
    if (!filters) return
    const defaults: Record<string, any> = {}

    filters.forEach(f => {
      if (f.type === 'select') {
        defaults[f.key] = 'ALL'
      } else if (f.type === 'range') {
        defaults[f.key] = [f.min, f.max]
      } else {
        defaults[f.key] = ''
      }
    })

    activeFilters.value = defaults
  },
  { immediate: true },
)

// Lookup map for manufacturer labels in bikes mode
const manufacturerMap = computed(
  () =>
    new Map(
      (props.manufacturers ?? []).map((m: any) => [m.manufacturer_id, m.name]),
    ),
)

// Lookup map for renter labels in bikes mode
const renterMap = computed(
  () =>
    new Map(
      (props.users ?? []).map((u: any) => [
        u.user_id,
        `${u.firstname ?? ''} ${u.lastname ?? ''}`.trim() || u.email,
      ]),
    ),
)

// Build the items dataset for the grid (bikes mode or users/generic mode) and apply filters
const itemsForGrid = computed(() => {
  let base: any[] = []

  // Bikes mode: normalize fields and enrich with display names
  if (props.bikes && props.bikes.length > 0) {
    base = props.bikes.map((item: any) => {
      const normalized: Record<string, any> = {}

      for (const [key, value] of Object.entries(item as Record<string, any>)) {
        if (value instanceof Date) {
          normalized[key] = value.toISOString().slice(0, 16)
        } else {
          normalized[key] = value
        }
      }

      const mid = item.manufacturer_id
      normalized.manufacturerName =
        manufacturerMap.value.get(mid) ?? `#${mid}`

      const rid = item.currentRenter_id
      normalized.renterName = rid
        ? renterMap.value.get(rid) ?? `#${rid}`
        : '—'

      return normalized
    })
  }
  // Users or other entities: simple normalization of date fields
  else if (props.users && props.users.length > 0) {
    base = props.users.map((item: any) => {
      const normalized: Record<string, any> = {}

      for (const [key, value] of Object.entries(item as Record<string, any>)) {
        if (value instanceof Date) {
          normalized[key] = value.toISOString().slice(0, 16)
        } else {
          normalized[key] = value
        }
      }

      return normalized
    })
  } else {
    return []
  }

  // Apply all configured filters on the client side
  if (props.filters && props.filters.length > 0) {
    base = base.filter(row => {
      return props.filters!.every(filter => {
        const val = activeFilters.value[filter.key]

        // No active value → ignore this filter
        if (
          val === undefined ||
          val === null ||
          val === '' ||
          val === 'ALL'
        ) {
          return true
        }

        // Numeric range filters (e.g., year, price)
        if (filter.type === 'range') {
          if (!Array.isArray(val) || val.length !== 2) return true
          const [min, max] = val
          if (min == null || max == null) return true
          const num = Number(row[filter.key])
          if (Number.isNaN(num)) return false
          return num >= min && num <= max
        }

        // Discrete select filters (e.g., status, manufacturer)
        if (filter.type === 'select') {
          return row[filter.key] === val
        }

        // Text filters (contains search, case-insensitive)
        if (filter.type === 'text') {
          return String(row[filter.key] ?? '')
            .toLowerCase()
            .includes(String(val).toLowerCase())
        }

        return true
      })
    })
  }

  return base
})

// Reset filters to their default values (ALL / full range / empty text)
function refreshFilters() {
  const defaults: Record<string, any> = {}

  props.filters?.forEach(f => {
    if (f.type === 'select') {
      defaults[f.key] = 'ALL'
    } else if (f.type === 'range') {
      defaults[f.key] = [f.min, f.max]
    } else {
      defaults[f.key] = ''
    }
  })

  activeFilters.value = defaults
}

// Optional manual hook for filter changes (if needed from child components)
function onFilterChange(filter: any, value: any) {
  activeFilters.value[filter.key] = value
}

// Infer form field metadata from headers and sample item
const autoColumns = computed(() => {
  if (!itemsForGrid.value || itemsForGrid.value.length === 0) return []

  const sample = itemsForGrid.value[0] as Record<string, any>

  return Object.keys(sample)
    .filter(
      key =>
        key !== props.idKey &&
        key !== 'manufacturerName' &&
        key !== 'renterName',
    )
    .map(key => {
      const value = sample[key]
      let type: string = 'text'

      if (typeof value === 'number') type = 'number'
      if (key.toLowerCase().includes('date')) type = 'date'
      if (key.toLowerCase() === 'year') type = 'year'

      // Explicit overrides for bikes domain
      if (key === 'price') type = 'float'
      if (key === 'status') type = 'select'
      if (key === 'manufacturer_id') type = 'manufacturer'
      if (key === 'currentRenter_id') type = 'user'

      const headerMeta = props.headers.find(h => h.key === key)
      const required = headerMeta?.required ?? false
      const label =
        headerMeta?.title ??
        key
          .replace(/_/g, ' ')
          .replace(/\b\w/g, (c: string) => c.toUpperCase())

      return {
        key,
        label,
        type,
        required,
        options:
          key === 'status'
            ? [
                { label: 'Available', value: 'AVAILABLE' },
                { label: 'Rented', value: 'RENTED' },
                { label: 'Maintenance', value: 'MAINTENANCE' },
              ]
            : undefined,
      }
    })
})

// Trigger data reload in the parent component
function refresh() {
  error.value = null
  emit('refresh')
}

// Open dialog in "create" mode
function openAdd() {
  error.value = null
  dialog.value.open = true
  dialog.value.editing = false
  selected.value = null
}

// Open dialog in "edit" mode for the selected row
function openEdit() {
  if (!selected.value) return
  error.value = null
  dialog.value.open = true
  dialog.value.editing = true
}

// Delete the currently selected item
async function onDelete() {
  if (!selected.value) return
  error.value = null

  try {
    saving.value = true
    const id = (selected.value as any)[props.idKey]
    await props.remove(id)
    selected.value = null
  } catch (err: any) {
    error.value = err?.message ?? 'Failed to delete item'
  } finally {
    saving.value = false
  }
}

// Persist changes from the dialog (create or update)
async function saveDraft(payload: any) {
  error.value = null

  try {
    saving.value = true

    if (dialog.value.editing && selected.value) {
      const id = (selected.value as any)[props.idKey]
      await props.update(id, payload)
    } else {
      await props.create(payload)
    }

    dialog.value.open = false
  } catch (err: any) {
    error.value = err?.message ?? 'Failed to save item'
  } finally {
    saving.value = false
  }
}

// Close the dialog without saving
function closeDialog() {
  dialog.value.open = false
}
</script>

<template>
  <section class="pa-4 d-flex flex-column ga-4">
    <!-- Page header: title and CRUD actions -->
    <div class="d-flex align-center justify-space-between">
      <h1 class="text-h5 ma-0">{{ title }} Dashboard</h1>

      <div class="d-flex ga-2">
        <v-btn variant="text" @click="refresh" :loading="loading">
          <v-icon start>mdi-refresh</v-icon>
          Refresh
        </v-btn>

        <v-btn color="deep-orange-darken-2" @click="openAdd">
          <v-icon start>mdi-plus</v-icon>
          Add
        </v-btn>

        <v-btn color="info" :disabled="!selected" @click="openEdit">
          <v-icon start>mdi-pencil</v-icon>
          Update
        </v-btn>

        <v-btn
          style="background-color: #d32f2f; color: white;"
          variant="flat"
          :disabled="!selected"
          @click="onDelete"
        >
          <v-icon>mdi-delete</v-icon>
          Delete
        </v-btn>
      </div>
    </div>

    <!-- Filter controls row -->
    <div
      v-if="filters && filters.length"
      class="d-flex align-center ga-6 flex-wrap"
    >
      <div
        v-for="filter in filters"
        :key="filter.key"
        class="d-flex flex-column align-center"
      >
        <label :for="`filter-${filter.key}`" class="text-subtitle-2 mb-1">
          {{ filter.label }}
        </label>

        <!-- Select-based filters (status, manufacturer, etc.) -->
        <v-select
          v-if="filter.type === 'select'"
          :id="`filter-${filter.key}`"
          :items="filter.options"
          item-title="label"
          item-value="value"
          v-model="activeFilters[filter.key]"
          density="compact"
          hide-details="auto"
          color="deep-orange-darken-2"
          style="width: 180px;"
        />

        <!-- Range-based filters (year, price, etc.) -->
        <v-range-slider
          v-else-if="filter.type === 'range'"
          :id="`filter-${filter.key}`"
          v-model="activeFilters[filter.key]"
          :min="filter.min"
          :max="filter.max"
          :step="filter.step || 1"
          density="compact"
          hide-details
          thumb-label="always"
          style="width: 260px;"
          thumb-color="deep-orange-darken-2"
          track-fill-color="deep-orange-lighten-2"
          track-color="rgb(73, 69, 79)"
        />
      </div>
    </div>

    <v-alert v-if="error" type="error" variant="tonal" :text="error" />
    <v-progress-linear v-if="loading" indeterminate />

    <!-- Main data grid -->
    <DataGrid
      :headers="headers"
      :items="itemsForGrid"
      :loading="loading"
      :item-key="idKey"
      :items-per-page="15"
      v-model:selected="selected"
    />

    <!-- Add / edit dialog -->
    <DialogForm
      :title="title"
      v-model="dialog.open"
      :editing="dialog.editing"
      :fields="autoColumns"
      :modelData="selected"
      :saving="saving"
      :manufacturers="props.manufacturers"
      :users="props.users"
      @save="saveDraft"
      @cancel="closeDialog"
    />
  </section>
</template>

<style>
/* Position slider thumb labels with reduced visual emphasis */
.v-slider-thumb__label {
  opacity: 0.5;
}

/* Adjust slider thumb label arrow orientation and offset */
.v-slider-thumb__label::after {
  transform: rotate(280deg);
  top: 20px;
}
</style>
