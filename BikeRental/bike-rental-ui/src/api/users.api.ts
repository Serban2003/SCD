import { http } from './client'
import type { User, NewUser, UpdateUser } from '../types/user.type'

const base = '/users';

export const UserApi = {
  list:   () => http.get<User[]>(base),
  get:    (id: number | string) => http.get<User>(`${base}/${id}`),
  create: (data: NewUser) => http.post<User>(base, data),
  update: (id: number | string, data: UpdateUser) => http.put<User>(`${base}/${id}`, data),
  delete: (id: number | string) => http.del<void>(`${base}/${id}`),
};