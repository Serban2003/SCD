import { http } from './client'
import type { Bike, NewBike, UpdateBike } from '../types/bike.type'

const base = '/bikes';

export const BikesApi = {
  list:   () => http.get<Bike[]>(base),
  get:    (id: number | string) => http.get<Bike>(`${base}/${id}`),
  create: (data: NewBike) => http.post<Bike>(base, data),
  update: (id: number | string, data: UpdateBike) => http.put<Bike>(`${base}/${id}`, data),
  delete: (id: number | string) => http.del<void>(`${base}/${id}`),
};
