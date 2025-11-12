export type Manufacturer = {
    manufacturer_id: number;
    name: string;
    description: string;
};

export type NewManufacturer = Omit<Manufacturer, 'manufacturer_id'>;
export type UpdateManufacturer = Partial<Omit<Manufacturer, 'manufacturer_id'>>;