import { http } from './client'
import type { Manufacturer, NewManufacturer, UpdateManufacturer } from '../types/manufacturer.type'

const base = '/manufacturers';

export const ManufacturerApi = {
  list:   () => http.get<Manufacturer[]>(base),
  get:    (id: number | string) => http.get<Manufacturer>(`${base}/${id}`),
  create: (data: NewManufacturer) => http.post<Manufacturer>(base, data),
  update: (id: number | string, data: UpdateManufacturer) => http.put<Manufacturer>(`${base}/${id}`, data),
  delete: (id: number | string) => http.del<void>(`${base}/${id}`),
};