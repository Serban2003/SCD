<template>
  <section class="pa-4 d-flex flex-column ga-4">
    <div class="d-flex align-center justify-space-between">
      <h1 class="text-h5 ma-0">{{ title }} Dashboard</h1>
      <div class="d-flex ga-2">
        <v-btn variant="text" @click="refresh" :loading="loading">Refresh</v-btn>
        <v-btn color="primary" @click="openAdd">Add</v-btn>
        <v-btn :disabled="!selected" @click="openEdit">Update</v-btn>
        <v-btn color="error" :disabled="!selected" @click="onDelete">Delete</v-btn>
      </div>
    </div>

    <v-alert v-if="error" type="error" variant="tonal" :text="error" />
    <v-progress-linear v-if="loading" indeterminate />

    <DataGrid
      v-else
      :headers="headers"
      :items="sortedFiltered"
      :loading="loading"
      :items-per-page="10"
      :sort-by="initialSort"
      :item-key="idKey"
      v-model:selected="selected"
      @row:click="() => {}">
    </DataGrid>

    <v-dialog v-model="dialog.open" max-width="720">
      <v-card>
        <v-card-title>{{ dialog.editing ? 'Update' : 'Add' }} entry</v-card-title>
        <v-divider />
        <v-card-text>
          <v-form ref="formRef" v-model="formValid" validate-on="blur">
            <div class="d-grid ga-3" style="grid-template-columns: repeat(auto-fit, minmax(220px, 1fr));">
              <div v-for="f in fields" :key="f.key">
                <v-text-field
                  v-if="!f.type || f.type==='text' || f.type==='number'"
                  v-model="draft[f.key]" :type="f.type || 'text'" :label="f.label"
                  :rules="f.required ? [req] : []" hide-details="auto" density="comfortable" />
                <v-select
                  v-else-if="f.type==='select'"
                  v-model="draft[f.key]" :items="f.options || []" item-title="label" item-value="value"
                  :label="f.label" :rules="f.required ? [req] : []" hide-details="auto" density="comfortable" />
                <v-text-field
                  v-else-if="f.type==='datetime'"
                  v-model="draft[f.key]" type="datetime-local" :label="f.label"
                  :rules="f.required ? [req] : []" hide-details="auto" density="comfortable" />
                <v-textarea
                  v-else-if="f.type==='textarea'"
                  v-model="draft[f.key]" :label="f.label" :rules="f.required ? [req] : []"
                  rows="3" hide-details="auto" density="comfortable" />
                <slot v-else :name="`field:${f.key}`" :draft="draft" :field="f" />
              </div>
            </div>
          </v-form>
        </v-card-text>
        <v-divider />
        <v-card-actions class="justify-end">
          <v-btn variant="text" @click="closeDialog">Cancel</v-btn>
          <v-btn color="primary" :loading="saving" @click="saveDraft">{{ dialog.editing ? 'Save' : 'Create' }}</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </section>
</template>

<script setup lang="ts">
import { ref, reactive, watch, computed, onMounted } from 'vue'
import DataGrid from '@/components/DataGrid.vue'

export type Column = { key: string; label: string }
export type SelectOption = { value: string | number; label: string }
export type Field = { key: string; label: string; type?: 'text'|'number'|'select'|'datetime'|'textarea'; required?: boolean; options?: SelectOption[] }

const props = defineProps<{
    title: string
    list: () => Promise<any[]>
    create: (data: any) => Promise<any>
    update: (id: string|number, data: any) => Promise<any>
    remove: (id: string|number) => Promise<void>
    columns: Column[]
    fields: Field[]
    idKey?: string
}>()

const idKey = computed(() => props.idKey ?? 'id')

// state
const loading = ref(false)
const saving  = ref(false)
const error   = ref<string | null>(null)
const rows    = ref<any[]>([])
const selected = ref<any | null>(null)
const search = ref(''); const appliedSearch = ref('')
const sort = reactive<{ key: string | null; asc: boolean }>({ key: null, asc: true })

// headers for DataGrid
const headers = computed(() => props.columns.map(c => ({ title: c.label, key: c.key, sortable: true })))
const initialSort = computed(() => sort.key ? [{ key: sort.key!, order: sort.asc ? 'asc' : 'desc' }] : [])

const sortedFiltered = computed(() => {
  let data = rows.value.slice()
  if (appliedSearch.value) {
    const q = appliedSearch.value.toLowerCase()
    data = data.filter(r => Object.values(r).some(v => String(v ?? '').toLowerCase().includes(q)))
  }
  if (sort.key) {
    const k = sort.key, dir = sort.asc ? 1 : -1
    data.sort((a, b) => {
      const va = a[k as string], vb = b[k as string]
      if (va == null && vb == null) return 0
      if (va == null) return 1
      if (vb == null) return -1
      if (typeof va === 'number' && typeof vb === 'number') return (va - vb) * dir
      return String(va).localeCompare(String(vb)) * dir
    })
  }
  return data
})

function applySearch() { appliedSearch.value = search.value }
function clearSearch()  { search.value = ''; appliedSearch.value = '' }
function refresh()      { fetchList() }

const req = (v: any) => (v === undefined || v === null || v === '' ? 'Required' : true)

async function fetchList() {
  loading.value = true
  error.value = null
  try {
    const res = await fetch(props.endpoint)
    if (!res.ok) throw new Error(`GET ${props.endpoint} -> ${res.status}`)
    rows.value = await res.json()
  } catch (e: any) {
    error.value = e?.message ?? 'Failed to load data.'
  } finally {
    loading.value = false
  }
}

// dialog / form
const dialog = reactive({ open: false, editing: false })
const draft  = reactive<Record<string, any>>({})
const formRef = ref(); const formValid = ref(false)

function openAdd() {
  dialog.open = true; dialog.editing = false
  Object.keys(draft).forEach(k => delete draft[k])
  for (const f of props.fields) draft[f.key] = ''
}
function openEdit() {
  if (!selected.value) return
  dialog.open = true; dialog.editing = true
  Object.keys(draft).forEach(k => delete draft[k])
  Object.assign(draft, selected.value)
}
function closeDialog() { dialog.open = false }

async function saveDraft() {
  const form = formRef.value as any
  const ok = await form?.validate?.()
  if (ok && ok.valid === false) return
  try {
    saving.value = true
    if (dialog.editing) await updateItem()
    else await createItem()
    dialog.open = false
    await fetchList()
  } catch (e: any) {
    error.value = e?.message || 'Operation failed'
  } finally {
    saving.value = false
  }
}

async function createItem() {
  const body = { ...draft }; delete body[idKey.value]
  const res = await fetch(props.endpoint, { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(body) })
  if (!res.ok) throw new Error(`POST ${props.endpoint} -> ${res.status}`)
}
async function updateItem() {
  const id = selected.value?.[idKey.value]; if (id == null) return
  const url = `${props.endpoint}/${encodeURIComponent(String(id))}`
  const res = await fetch(url, { method: 'PUT', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(draft) })
  if (!res.ok) throw new Error(`PUT ${url} -> ${res.status}`)
}
async function onDelete() {
  const id = selected.value?.[idKey.value]; if (id == null) return
  if (!confirm('Delete selected entry?')) return
  const url = `${props.endpoint}/${encodeURIComponent(String(id))}`
  const res = await fetch(url, { method: 'DELETE' })
  if (!res.ok) { error.value = `DELETE ${url} -> ${res.status}`; return }
  selected.value = null
  await fetchList()
}

onMounted(fetchList)
watch(() => props.endpoint, fetchList)
</script>

<style scoped>
.filter-input { min-width: 260px; }
</style>
