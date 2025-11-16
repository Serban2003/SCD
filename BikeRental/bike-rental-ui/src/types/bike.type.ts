/**
 * Domain model for a bike entity.
 * This type reflects what the backend returns.
 */
export type Bike = {
  bike_id: number
  manufacturer_id: number
  model: string
  year: number
  price: number

  /** Current rental status */
  status: 'AVAILABLE' | 'RENTED' | 'MAINTENANCE'

  /**
   * Optional rental date.
   * On the backend this should typically be an ISO string.
   * It is fine to keep this as `string | null` unless you explicitly want a Date object.
   */
  rentDate?: string | null

  /** ID of the current active renter, if any */
  currentRenter_id?: number | null
}

/**
 * Payload for creating a new bike.
 * Excludes the auto-generated primary key.
 */
export type NewBike = Omit<Bike, 'bike_id'>

/**
 * Payload for updating an existing bike.
 * All fields optional, except for the identifier.
 */
export type UpdateBike = Partial<Omit<Bike, 'bike_id'>>
