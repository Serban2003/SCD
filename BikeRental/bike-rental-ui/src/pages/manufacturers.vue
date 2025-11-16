<script setup lang="ts">
import { ref, onMounted } from 'vue'
import DataPage from '@/components/DataPage.vue'
import { ManufacturersApi } from '@/api'
import type {
  Manufacturer,
  NewManufacturer,
  UpdateManufacturer,
} from '@/types/manufacturer.type'

/**
 * Local state for manufacturer records and loading state.
 */
const manufacturers = ref<Manufacturer[]>([])
const loading = ref(false)

/**
 * Load all manufacturers from the backend.
 */
async function fetchList() {
  loading.value = true
  try {
    manufacturers.value = await ManufacturersApi.list()
  } finally {
    loading.value = false
  }
}

onMounted(fetchList)

/**
 * Create a new manufacturer and append it locally.
 */
async function createManufacturer(data: NewManufacturer) {
  const created = await ManufacturersApi.create(data)
  manufacturers.value.push(created)
}

/**
 * Update a manufacturer and replace it locally.
 */
async function updateManufacturer(id: number, data: UpdateManufacturer) {
  const updated = await ManufacturersApi.update(id, data)
  const idx = manufacturers.value.findIndex(m => m.manufacturer_id === id)
  if (idx !== -1) manufacturers.value[idx] = updated
}

/**
 * Remove a manufacturer both on the server and locally.
 */
async function removeManufacturer(id: number) {
  await ManufacturersApi.delete(id)
  manufacturers.value = manufacturers.value.filter(
    m => m.manufacturer_id !== id,
  )
}

/**
 * Column metadata for DataPage.
 */
const headers = [
  { title: 'ID', key: 'manufacturer_id', required: true },
  { title: 'Name', key: 'name', required: true },
  { title: 'Description', key: 'description', required: false },
]
</script>

<template>
  <DataPage
    title="Manufacturers"
    :headers="headers"
    :loading="loading"
    id-key="manufacturer_id"
    :create="createManufacturer"
    :update="updateManufacturer"
    :remove="removeManufacturer"
    @refresh="fetchList"
    :users="manufacturers"
  />
</template>
