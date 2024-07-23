export interface iCustomer {
  Id?: number;
  Name: string;
  LastName: string;
  Email: string;
  Date?: string;
  Phone?: string;
  Password?: string;
  VersionHolding?: string | null;
}

export interface iCustomerData {
  version: string;
  customers: iCustomer[];
}
