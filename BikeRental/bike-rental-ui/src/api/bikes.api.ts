import { http } from './client'
import type { Bike, NewBike, UpdateBike } from '../types/bike.type'

/**
 * Base API path for all bike-related requests.
 */
const base = '/bikes'

/**
 * BikesApi consolidates all REST endpoints for managing bikes.
 * Each method returns a typed HTTP request.
 */
export const BikesApi = {
  /**
   * Retrieve all bikes.
   */
  list: () => http.get<Bike[]>(base),

  /**
   * Retrieve a single bike by ID.
   */
  get: (id: number | string) => http.get<Bike>(`${base}/${id}`),

  /**
   * Create a new bike.
   */
  create: (data: NewBike) => http.post<Bike>(base, data),

  /**
   * Update an existing bike.
   */
  update: (id: number | string, data: UpdateBike) =>
    http.put<Bike>(`${base}/${id}`, data),

  /**
   * Delete a bike by ID.
   */
  delete: (id: number | string) => http.del<void>(`${base}/${id}`),

  /**
   * Filter bikes by status.
   */
  getByStatus: (status: string) =>
    http.get<Bike[]>(`${base}?status=${encodeURIComponent(status)}`),

  /**
   * Filter bikes by manufacturer ID.
   */
  getByManufacturer: (manufacturerId: number | string) =>
    http.get<Bike[]>(`${base}?manufacturer_id=${manufacturerId}`),

  /**
   * Filter bikes by a year range.
   */
  getByYearRange: (startYear: number, endYear: number) =>
    http.get<Bike[]>(
      `${base}?start_year=${startYear}&end_year=${endYear}`,
    ),

  /**
   * Filter bikes by a price range.
   */
  getByPriceRange: (minPrice: number, maxPrice: number) =>
    http.get<Bike[]>(
      `${base}?min_price=${minPrice}&max_price=${maxPrice}`,
    ),
}
