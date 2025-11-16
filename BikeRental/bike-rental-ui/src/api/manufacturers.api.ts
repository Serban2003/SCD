import { http } from './client'
import type {
  Manufacturer,
  NewManufacturer,
  UpdateManufacturer,
} from '../types/manufacturer.type'

/**
 * Base path for all manufacturer endpoints.
 */
const base = '/manufacturers'

/**
 * ManufacturersApi centralizes all CRUD operations for manufacturers.
 */
export const ManufacturersApi = {
  /**
   * Retrieve all manufacturers.
   */
  list: () => http.get<Manufacturer[]>(base),

  /**
   * Retrieve a manufacturer by ID.
   */
  get: (id: number | string) =>
    http.get<Manufacturer>(`${base}/${id}`),

  /**
   * Create a new manufacturer.
   */
  create: (data: NewManufacturer) =>
    http.post<Manufacturer>(base, data),

  /**
   * Update an existing manufacturer.
   */
  update: (id: number | string, data: UpdateManufacturer) =>
    http.put<Manufacturer>(`${base}/${id}`, data),

  /**
   * Delete a manufacturer by ID.
   */
  delete: (id: number | string) =>
    http.del<void>(`${base}/${id}`),
}
