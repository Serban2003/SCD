<template>
  <v-dialog v-model="model" max-width="500">
    <v-card>
      <v-card-title>
        {{ editing ? 'Update' : 'Add' }} {{ title }}
      </v-card-title>

      <v-divider />

      <v-card-text>
        <v-form ref="formRef" v-model="formValid" validate-on="blur">
          <div
            class="d-grid ga-3"
            style="grid-template-columns: repeat(auto-fit, minmax(220px, 1fr));"
          >
            <template v-for="f in fields" :key="f.key">
              <div class="pa-2">
                <!-- Numeric field -->
                <v-text-field
                  v-if="f.type === 'number'"
                  v-model="draft[f.key]"
                  type="number"
                  :label="f.label"
                  min="0"
                  :rules="[req, positive]"
                  hide-details="auto"
                  density="comfortable"
                  color="deep-orange-darken-2"
                />

                <!-- Manufacturer selector -->
                <v-select
                  v-else-if="f.type === 'manufacturer'"
                  v-model="draft[f.key]"
                  :items="manufacturerItems"
                  item-title="name"
                  item-value="manufacturer_id"
                  :label="'Manufacturer'"
                  :rules="[req, positive]"
                  hide-details="auto"
                  density="comfortable"
                  color="deep-orange-darken-2"
                />

                <!-- User selector (enabled only when status is RENTED) -->
                <v-select
                  v-else-if="f.type === 'user'"
                  v-model="draft[f.key]"
                  :items="userItems"
                  item-title="display"
                  item-value="user_id"
                  :label="'User'"
                  :disabled="!isRented"
                  :rules="[renterRule]"
                  hide-details="auto"
                  density="comfortable"
                  color="deep-orange-darken-2"
                />

                <!-- Floating-point numeric field -->
                <v-text-field
                  v-else-if="f.type === 'float'"
                  v-model="draft[f.key]"
                  type="number"
                  step="0.01"
                  min="0"
                  :label="f.label"
                  :rules="[req, priceRule]"
                  hide-details="auto"
                  density="comfortable"
                  color="deep-orange-darken-2"
                />

                <!-- Year-only numeric field -->
                <v-text-field
                  v-else-if="f.type === 'year'"
                  v-model="draft[f.key]"
                  type="number"
                  min="1900"
                  max="2099"
                  :label="f.label"
                  :rules="[req, yearRule]"
                  hide-details="auto"
                  density="comfortable"
                  color="deep-orange-darken-2"
                />

                <!-- Simple text field -->
                <v-text-field
                  v-else-if="!f.type || f.type === 'text'"
                  v-model="draft[f.key]"
                  type="text"
                  :label="f.label"
                  :rules="[req, textRule]"
                  hide-details="auto"
                  density="comfortable"
                  color="deep-orange-darken-2"
                />

                <!-- Generic select (e.g. status) -->
                <v-select
                  v-else-if="f.type === 'select'"
                  v-model="draft[f.key]"
                  :items="f.options || []"
                  item-title="label"
                  item-value="value"
                  :label="f.label"
                  :rules="[req]"
                  hide-details="auto"
                  density="comfortable"
                  color="deep-orange-darken-2"
                />

                <!-- Date-time field (bound to datetime-local input) -->
                <v-text-field
                  v-else-if="f.type === 'date'"
                  v-model="draft[f.key]"
                  type="datetime-local"
                  :label="f.label"
                  :rules="[rentDateRule]"
                  :disabled="!isRented"
                  hide-details="auto"
                  density="comfortable"
                  color="deep-orange-darken-2"
                />

                <!-- Multiline text field -->
                <v-textarea
                  v-else-if="f.type === 'textarea'"
                  v-model="draft[f.key]"
                  :label="f.label"
                  :rules="[req, positive]"
                  rows="3"
                  hide-details="auto"
                  density="comfortable"
                  color="deep-orange-darken-2"
                />

                <!-- Custom field content via slot -->
                <slot
                  v-else
                  :name="`field:${f.key}`"
                  :draft="draft"
                  :field="f"
                />
              </div>
            </template>
          </div>
        </v-form>
      </v-card-text>

      <v-divider />

      <v-card-actions class="justify-end">
        <v-btn variant="text" @click="cancel">Cancel</v-btn>
        <v-btn
          color="deep-orange-darken-2"
          :loading="saving"
          @click="handleSave"
        >
          {{ editing ? 'Save' : 'Create' }}
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup lang="ts">
import { ref, watch, reactive, computed } from 'vue'

/**
 * Props define dialog configuration, fields, and external data sources.
 */
const props = defineProps<{
  title: string
  modelValue: boolean
  editing: boolean
  fields: any[]
  modelData?: any
  saving?: boolean
  manufacturers?: any[]
  users?: any[]
}>()

/**
 * Emits notify parent about dialog visibility, save actions, and cancellation.
 */
const emit = defineEmits<{
  (e: 'update:modelValue', value: boolean): void
  (e: 'save', payload: any): void
  (e: 'cancel'): void
}>()

/**
 * Validation rules used by various input types.
 */
const yearRule = (v: any) => {
  if (!v) return 'Required'
  const year = Number(v)
  const current = new Date().getFullYear()
  if (year < 1900 || year > current) {
    return `Year must be between 1900 and ${current}`
  }
  return true
}

// Price must be a valid positive float
const priceRule = (v: any) => {
  if (v === '' || v === null || v === undefined) return 'Required'
  const num = Number(v)
  if (Number.isNaN(num)) return 'Invalid number'
  if (num <= 0) return 'Must be greater than 0'
  return true
}

// Text field: required, non-empty
const textRule = (v: any) =>
  v === '' || v === null || v === undefined ? 'Required' : true

// General date-time validation (kept for potential reuse)
const dateTimeRule = (v: any) => {
  if (!v) return 'Required'
  const d = new Date(v)
  if (Number.isNaN(d.getTime())) return 'Invalid date'
  return true
}

// User is required only when status is RENTED
const renterRule = (v: any) =>
  draft.status === 'RENTED' && !v ? 'Required when rented' : true

// Rent date is required only when status is RENTED
const rentDateRule = (v: any) =>
  draft.status === 'RENTED' && !v ? 'Date required when rented' : true

// Generic required rule
const req = (v: any) =>
  v === null || v === undefined || v === '' ? 'Required' : true

// Generic non-negative rule
const positive = (v: any) =>
  v === null || v === undefined || v === ''
    ? 'Required'
    : Number(v) >= 0
    ? true
    : 'Must be positive'

/**
 * Local dialog model bound to v-dialog.
 */
const model = ref(props.modelValue)

watch(
  () => props.modelValue,
  v => (model.value = v),
)

watch(model, v => emit('update:modelValue', v))

/**
 * Form instance and validation state.
 */
const formRef = ref()
const formValid = ref(false)

/**
 * Draft object represents the editable state of the current entity.
 */
const draft = reactive<any>({})

/**
 * Helper flag used to control renter-related fields.
 */
const isRented = computed(() => draft.status === 'RENTED')

/**
 * When status is not RENTED, renter fields and rent date are cleared.
 */
watch(
  () => draft.status,
  val => {
    if (val !== 'RENTED') {
      if ('currentRenter_id' in draft) {
        draft.currentRenter_id = ''
      }
      if ('rentDate' in draft) {
        draft.rentDate = ''
      }
    }
  },
)

/**
 * Reset draft when dialog is closed.
 */
watch(model, isOpen => {
  if (!isOpen && !props.editing) {
    props.fields.forEach(f => {
      draft[f.key] = ''
    })
  }
})

/**
 * Manufacturer and user option lists for selects.
 */
const manufacturerItems = computed(() => props.manufacturers ?? [])

const userItems = computed(() =>
  (props.users ?? []).map(u => ({
    ...u,
    display:
      `${u.firstname ?? ''} ${u.lastname ?? ''}`.trim() || u.email,
  })),
)

/**
 * Convert Date or ISO string to the datetime-local input format.
 */
function toDateTimeLocal(val: string | Date): string {
  const d = typeof val === 'string' ? new Date(val) : val
  if (Number.isNaN(d.getTime())) return ''

  const pad = (n: number) => String(n).padStart(2, '0')

  const year = d.getFullYear()
  const month = pad(d.getMonth() + 1)
  const day = pad(d.getDate())
  const hours = pad(d.getHours())
  const minutes = pad(d.getMinutes())

  return `${year}-${month}-${day}T${hours}:${minutes}`
}

/**
 * Synchronize draft data with incoming modelData and mode (add/edit).
 */
watch(
  () => [props.modelData, props.editing],
  ([data, editing]) => {
    Object.keys(draft).forEach(k => delete draft[k])

    if (editing && data) {
      // Populate form in edit mode
      props.fields.forEach((f: any) => {
        const val = data[f.key]

        if (f.type === 'date' && val) {
          draft[f.key] = toDateTimeLocal(val)
        } else {
          draft[f.key] = val ?? ''
        }
      })
    } else {
      // Initialize empty form in add mode
      props.fields.forEach((f: any) => {
        draft[f.key] = ''
      })
    }
  },
  { immediate: true },
)

/**
 * Cancel handler: closes dialog and notifies parent.
 */
function cancel() {
  emit('cancel')
  model.value = false
}

/**
 * Validate form, build payload, and emit save event.
 */
async function handleSave() {
  const form = formRef.value as any
  const result = await form?.validate?.()
  if (result && result.valid === false) return

  const payload: any = {}

  props.fields.forEach((f: any) => {
    let v = draft[f.key]

    if (f.type === 'number') {
      v = v === '' || v === null || v === undefined ? null : Number(v)
    } else if (f.type === 'float') {
      v = v === '' || v === null || v === undefined ? null : parseFloat(v)
    } else if (f.type === 'date') {
      v = draft[f.key] ? new Date(draft[f.key]).toISOString() : null
    }

    payload[f.key] = v
  })

  emit('save', payload)
}
</script>
