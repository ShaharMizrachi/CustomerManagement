export interface iCustomer {
  id?: number;
  name: string;
  lastName: string;
  email: string;
  date?: string;
  phone?: string;
  password?: string;
  VersionHolding?: string | null;
}

export interface iCustomerData {
  version: string;
  customers: iCustomer[];
}
