/**
 * Domain model for a user entity returned by the backend.
 * Passwords must NEVER be included in API responses.
 */
export type User = {
  user_id: number
  firstname: string
  lastname: string
  email: string
  password?: string
}

/**
 * Payload for creating a new user.
 * For creation, a password is required.
 */
export type NewUser = {
  firstname: string
  lastname: string
  email: string
  password: string
}

/**
 * Payload for updating an existing user.
 * Password updates should be handled through a dedicated endpoint (not here).
 */
export type UpdateUser = Partial<Omit<NewUser, 'password'>>
