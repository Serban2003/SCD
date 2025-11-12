export type User = {
    user_id: number;
    firstname: string;
    lastname: string;
    email: string;
    password: string;
};

export type NewUser = Omit<User, 'user_id'>;
export type UpdateUser = Partial<Omit<User, 'user_id'>>;