import { http } from './client'
import type {
  User,
  NewUser,
  UpdateUser,
} from '../types/user.type'

/**
 * Base path for user-related REST endpoints.
 */
const base = '/users'

/**
 * UsersApi provides CRUD operations for managing users.
 * All responses are strongly typed for type safety.
 */
export const UsersApi = {
  /**
   * Retrieve all users.
   */
  list: () => http.get<User[]>(base),

  /**
   * Retrieve a user by ID.
   */
  get: (id: number | string) =>
    http.get<User>(`${base}/${id}`),

  /**
   * Create a new user.
   */
  create: (data: NewUser) =>
    http.post<User>(base, data),

  /**
   * Update an existing user.
   */
  update: (id: number | string, data: UpdateUser) =>
    http.put<User>(`${base}/${id}`, data),

  /**
   * Delete a user by ID.
   */
  delete: (id: number | string) =>
    http.del<void>(`${base}/${id}`),
}
