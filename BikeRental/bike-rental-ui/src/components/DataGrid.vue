<template>
  <v-card variant="outlined">
    <v-data-table
      :headers="headers"
      :items="items"
      :loading="loading"
      :items-per-page="itemsPerPage"
      :sort-by="sortBy"
      :hover="true"
      :search="''" 
      :item-key="itemKey"
      density="comfortable"
      @click:row="onRowClick"
    >
      <!-- Pass-through custom cell slots: use #item.<key> in parent of DataGrid -->
      <template
        v-for="col in headers"
        :key="col.key"
        #[`item.${col.key}`]="{ item }"
      >
        <slot :name="`item.${col.key}`" :item="item">
          {{ formatCell(item[col.key]) }}
        </slot>
      </template>

      <template #no-data>
        <div class="pa-6 text-medium-emphasis">No data.</div>
      </template>
    </v-data-table>
  </v-card>
</template>

<script setup lang="ts">
import { defineProps, defineEmits } from 'vue'

type Header = { title: string; key: string; sortable?: boolean }

const props = defineProps<{
  headers: Header[]
  items: any[]
  loading?: boolean
  itemsPerPage?: number
  sortBy?: Array<{ key: string; order?: 'asc' | 'desc' }>
  itemKey?: string
}>()

const emit = defineEmits<{
  (e: 'row:click', row: any): void
  (e: 'update:selected', row: any | null): void
}>()

function onRowClick(payload: any) {
  const row = payload?.item
  emit('update:selected', row)
  emit('row:click', row)
}

function formatCell(val: any) {
  if (val == null) return ''
  if (typeof val === 'string') return val
  if (typeof val === 'number') return String(val)
  try { return JSON.stringify(val) } catch { return String(val) }
}
</script>
