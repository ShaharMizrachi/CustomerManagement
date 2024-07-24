// src/redux/slices/customerSlice.ts
import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { iCustomerData } from "../../interfaces/Ilogin";

const initialState: iCustomerData = {
  version: "",
  customers: [],
};

const customerSlice = createSlice({
  name: "customer",
  initialState,
  reducers: {
    setCustomers: (state, action: PayloadAction<iCustomerData>) => {
      state.version = action.payload.version;
      state.customers = action.payload.customers;
    },
    loadFromLocalStorage: (state) => {
      const data = localStorage.getItem("data");
      if (data) {
        const parsedData: iCustomerData = JSON.parse(data);
        state.version = parsedData.version;
        state.customers = parsedData.customers;
      }
    },
  },
});

export const { setCustomers, loadFromLocalStorage } = customerSlice.actions;
export default customerSlice.reducer;
