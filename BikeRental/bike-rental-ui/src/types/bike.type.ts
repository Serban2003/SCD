export type Bike = {
    bike_id: number;
    manufacturer_id: number;
    model: string;
    year: number;
    price: number;
    status: 'AVAILABLE' | 'RENTED' | 'MAINTENANCE';
    rent_date?: Date;
    current_renter_id?: number;
};

export type NewBike = Omit<Bike, 'bike_id'>;
export type UpdateBike = Partial<Omit<Bike, 'bike_id'>>;