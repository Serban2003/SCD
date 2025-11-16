export type Field = {
  key: string
  label: string
  type?: 'text' | 'number' | 'boolean' | 'datetime' | 'select' | 'textarea'
  required?: boolean
  options?: { label: string; value: any }[]   // for dropdowns
  format?: (v: any) => string                 // optional formatting
}
