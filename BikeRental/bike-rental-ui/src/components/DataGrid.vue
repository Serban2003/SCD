<template>
  <v-card variant="outlined">
    <v-data-table
      :headers="headers"
      :items="items"
      :loading="loading"
      :items-per-page="itemsPerPage"
      :sort-by="sortBy"
      :item-key="itemKey"
      hover
      density="comfortable"
      :row-props="rowProps"
      @click:row="handleClick"
    >
      <!-- Cell rendering for each column -->
      <template
        v-for="col in headers"
        :key="col.key"
        #[`item.${col.key}`]="{ item }"
      >
        <slot :name="`item.${col.key}`" :item="item">
          {{ formatCell(item[col.key]) }}
        </slot>
      </template>
    </v-data-table>
  </v-card>
</template>

<script setup>
/*
  Component Props:
  - headers: array defining the displayed columns
  - items: array of raw data rows
  - selected: currently selected row (optional)
  - itemsPerPage: pagination size, default = 10
*/
const props = defineProps({
  headers: Array,
  items: Array,
  selected: Object,
  loading: Boolean,
  sortBy: Array,
  itemKey: String,
  itemsPerPage: {
    type: Number,
    default: 10,
  },
})

/*
  Component Events:
  - update:selected: notify parent about row selection changes
  - row:click: emit raw row payload on row click
*/
const emit = defineEmits(['update:selected', 'row:click'])

/*
  Detects the most probable primary key for a row.
  Supports multi-entity tables (bikes, manufacturers, users).
*/
function getRowKey(raw) {
  return raw?.bike_id ?? raw?.manufacturer_id ?? raw?.user_id ?? raw?.id
}

/*
  Row styling function used by Vuetify.
  Applies highlighting on the selected row.
*/
function rowProps(wrapper) {
  const raw = wrapper?.internalItem?.raw
  const sel = props.selected

  if (!raw || !sel) return {}

  if (getRowKey(raw) === getRowKey(sel)) {
    return { class: 'selected-row' }
  }

  return {}
}

/*
  Row click handler (Vuetify passes `event` first and wrapper second).
  Handles:
  - toggling selection
  - emitting selection updates
  - forwarding raw row data to parent listener
*/
function handleClick(event, wrapper) {
  const raw = wrapper?.internalItem?.raw
  if (!raw) return

  const clickedKey = getRowKey(raw)
  const selectedKey = props.selected ? getRowKey(props.selected) : null

  if (clickedKey === selectedKey) {
    emit('update:selected', null)       // deselect
  } else {
    emit('update:selected', raw)        // select new row
  }

  emit('row:click', raw)
}

/*
  Formats displayed cell values.
  Performs ISO datetime detection and formatting.
*/
function formatCell(value) {
  if (!value) return ''

  // ISO-like date detection
  if (typeof value === 'string' && /\d{4}-\d{2}-\d{2}T/.test(value)) {
    const d = new Date(value)
    if (isNaN(d)) return value

    const day = d.getDate().toString().padStart(2, '0')
    const month = d.toLocaleString('en', { month: 'short' })
    const year = d.getFullYear()

    const hours = d.getHours().toString().padStart(2, '0')
    const mins = d.getMinutes().toString().padStart(2, '0')

    return `${day} ${month} ${year}, ${hours}:${mins}`
  }

  return String(value)
}
</script>

<style>
/* Highlight for the selected row */
.selected-row {
  background-color: rgba(230, 74, 25, 0.12) !important;
}

.selected-row:hover {
  background-color: rgba(230, 74, 25, 0.18) !important;
}
</style>
