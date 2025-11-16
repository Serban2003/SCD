/**
 * Domain model for a manufacturer entity returned by the backend.
 */
export type Manufacturer = {
  /** Auto-generated identifier */
  manufacturer_id: number

  /** Display name of the manufacturer */
  name: string

  /** Optional description (backend often allows null or empty string) */
  description: string | null
}

/**
 * Payload for creating a new manufacturer.
 * Excludes the auto-generated ID.
 */
export type NewManufacturer = Omit<Manufacturer, 'manufacturer_id'>

/**
 * Payload for updating an existing manufacturer.
 * Contains only the fields you want to change.
 */
export type UpdateManufacturer = Partial<Omit<Manufacturer, 'manufacturer_id'>>
