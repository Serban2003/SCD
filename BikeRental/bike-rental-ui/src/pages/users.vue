<template>
  <DataPage
  title="Users"
  endpoint="/api/users"
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
    path: '/users',  // force path to /users instead of /users-page
    name: 'Users',
    // meta: { layout: 'default' },
    })
</script>
