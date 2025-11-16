<script setup lang="ts">
import { ref, onMounted } from 'vue'
import DataPage from '@/components/DataPage.vue'
import { UsersApi } from '@/api'
import type { User, NewUser, UpdateUser } from '@/types/user.type'

/**
 * Local state for users and loading indicator.
 */
const users = ref<User[]>([])
const loading = ref(false)

/**
 * Fetch all users from the backend API.
 */
async function fetchList() {
  loading.value = true
  try {
    users.value = await UsersApi.list()
  } finally {
    loading.value = false
    console.log(users.value)
  }
}

onMounted(fetchList)

/**
 * Create a new user and append it locally.
 */
async function createUser(data: NewUser) {
  const created = await UsersApi.create(data)
  users.value.push(created)
}

/**
 * Update an existing user and replace entry locally.
 */
async function updateUser(id: number, data: UpdateUser) {
  const updated = await UsersApi.update(id, data)
  const idx = users.value.findIndex(u => u.user_id === id)
  if (idx !== -1) users.value[idx] = updated
}

/**
 * Remove a user both in the backend and locally.
 */
async function removeUser(id: number) {
  await UsersApi.delete(id)
  users.value = users.value.filter(u => u.user_id !== id)
}

/**
 * Table column configuration for DataPage.
  */
const headers = [
  { title: 'ID', key: 'user_id', required: true },
  { title: 'First Name', key: 'firstname', required: true },
  { title: 'Last Name', key: 'lastname', required: true },
  { title: 'Email', key: 'email', required: true },
  { title: 'Password', key: 'password', required: true },
]
</script>

<template>
  <DataPage
    title="Users"
    :headers="headers"
    :loading="loading"
    id-key="user_id"
    :create="createUser"
    :update="updateUser"
    :remove="removeUser"
    @refresh="fetchList"
    :users="users"
  />
</template>
