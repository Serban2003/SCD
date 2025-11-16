<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import DataPage from '@/components/DataPage.vue'
import { BikesApi, ManufacturersApi, UsersApi } from '@/api'
import type { Bike, NewBike, UpdateBike } from '@/types/bike.type'
import type { Manufacturer } from '@/types/manufacturer.type'
import type { User } from '@/types/user.type'

/**
 * Local state for bikes, manufacturers, users, and loading indicator.
 */
const bikes = ref<Bike[]>([])
const manufacturers = ref<Manufacturer[]>([])
const users = ref<User[]>([])
const loading = ref(false)

/**
 * Fetch bikes, manufacturers, and users in parallel.
 * Populates local state for the page.
 */
async function fetchList() {
  loading.value = true
  try {
    const [b, m, u] = await Promise.all([
      BikesApi.list(),
      ManufacturersApi.list(),
      UsersApi.list(),
    ])
    bikes.value = b
    manufacturers.value = m
    users.value = u
  } finally {
    loading.value = false
  }
}

onMounted(fetchList)

/**
 * Create a new bike and append it to the local list.
 */
async function createBike(data: NewBike) {
  const created = await BikesApi.create(data)
  bikes.value.push(created)
}

/**
 * Update an existing bike and replace it in the local list.
 */
async function updateBike(id: number, data: UpdateBike) {
  const updated = await BikesApi.update(id, data)
  const idx = bikes.value.findIndex(b => b.bike_id === id)
  if (idx !== -1) bikes.value[idx] = updated
}

/**
 * Remove a bike both on the server and from the local list.
 */
async function removeBike(id: number) {
  await BikesApi.delete(id)
  bikes.value = bikes.value.filter(b => b.bike_id !== id)
}

/**
 * Column definitions for the bikes data grid.
 */
const headers = [
  { title: 'ID', key: 'bike_id' },
  { title: 'Manufacturer', key: 'manufacturerName' },
  { title: 'Model', key: 'model' },
  { title: 'Year', key: 'year' },
  { title: 'Renter', key: 'renterName' },
  { title: 'Rental date', key: 'rentDate' },
  { title: 'Status', key: 'status' },
  { title: 'Price / hour (RON)', key: 'price' },
]

/**
 * Static filter options for bike status.
 */
const statusFilterOptions = [
  { label: 'All', value: 'ALL' },
  { label: 'Available', value: 'AVAILABLE' },
  { label: 'Rented', value: 'RENTED' },
  { label: 'Maintenance', value: 'MAINTENANCE' },
]

/**
 * Dynamic filter options for manufacturers (includes "All").
 */
const manufacturerFilterOptions = computed(() => [
  { label: 'All', value: 'ALL' },
  ...manufacturers.value.map(m => ({
    label: m.name,
    value: m.manufacturer_id,
  })),
])

/**
 * Derived bounds for the year range filter based on current bikes.
 */
const minYear = computed(() => {
  if (!bikes.value.length) return 2000
  return Math.min(...bikes.value.map(b => b.year ?? 2000))
})

const maxYear = computed(() => {
  if (!bikes.value.length) return new Date().getFullYear()
  return Math.max(
    ...bikes.value.map(b => b.year ?? new Date().getFullYear()),
  )
})

/**
 * Derived bounds for the price range filter based on current bikes.
 */
const minPrice = computed(() => {
  if (!bikes.value.length) return 0
  return Math.min(...bikes.value.map(b => b.price ?? 0))
})

const maxPrice = computed(() => {
  if (!bikes.value.length) return 100
  return Math.max(...bikes.value.map(b => b.price ?? 100))
})

/**
 * Filter configuration passed down to DataPage for client-side filtering.
 */
const bikeFilters = computed(() => [
  {
    key: 'status',
    label: 'Status',
    type: 'select',
    options: statusFilterOptions,
  },
  {
    key: 'manufacturer_id',
    label: 'Manufacturer',
    type: 'select',
    options: manufacturerFilterOptions.value,
  },
  {
    key: 'year',
    label: 'Year range',
    type: 'range',
    min: minYear.value,
    max: maxYear.value,
    step: 1,
  },
  {
    key: 'price',
    label: 'Price range (RON)',
    type: 'range',
    min: minPrice.value,
    max: maxPrice.value,
    step: 0.1,
  },
])
</script>

<template>
  <DataPage
    title="Bikes"
    :headers="headers"
    :loading="loading"
    id-key="bike_id"
    :create="createBike"
    :update="updateBike"
    :remove="removeBike"
    @refresh="fetchList"
    :bikes="bikes"
    :manufacturers="manufacturers"
    :users="users"
    :filters="bikeFilters"
  />
</template>
