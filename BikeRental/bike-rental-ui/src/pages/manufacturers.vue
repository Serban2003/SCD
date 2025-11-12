<template>
  <DataPage
  title="Manufacturers"
  endpoint="/api/manufacturers"
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

<script setup lang="ts">
    definePage({
    path: '/manufacturers',  // force path to /manufacturers instead of /manufacturers-page
    name: 'Manufacturers',
    // meta: { layout: 'default' },
    })
</script>
