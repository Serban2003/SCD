<script setup lang="ts">
  import DataPage from '@/components/DataPage.vue'
  import { BikesApi } from '@/api'

  const list   = () => BikesApi.list()
  const create = (d: any) => BikesApi.create(d)
  const update = (id: any, d: any) => BikesApi.update(id, d)
  const remove = (id: any) => BikesApi.delete(id)
</script>

<template>
  <DataPage
  title="Bikes"
  :list="list"
  :create="create"
  :update="update"
  :remove="remove"
  :columns="[
    { key: 'id', label: 'ID' },
    { key: 'name', label: 'Name' },
    { key: 'status', label: 'Status' },
    { key: 'year', label: 'Year' },
  ]"
  :fields="[
    { key: 'name',  label: 'Name', required: true },
    { key: 'status', label: 'Status', type: 'select', options: [
      { value: 'available', label: 'Available' },
      { value: 'rented',    label: 'Rented' },
      { value: 'service',   label: 'Maintenance' },
    ]},
    { key: 'year',  label: 'Year',  type: 'number' },
  ]"
/>
</template>