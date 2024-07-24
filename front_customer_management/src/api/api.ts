import axios, { AxiosResponse } from "axios";
import { iCustomer } from "../interfaces/Ilogin";

const url = axios.create({
  baseURL: "https://localhost:44365/api/", //local
  headers: {
    "Content-type": "Application/json",
    //'Access-Control-Allow-Origin': '*',
  },
});

export const makeRequest = async (callback: () => Promise<any>, functionName: string) => {
  try {
    return await callback();
  } catch (e) {
    console.log(`[${functionName}]: ${e}`);
  }
};

export const verificationCustomer = async (logInCustomer: iCustomer) => {
  return makeRequest(async () => {
    const userToSend = { ...logInCustomer, id: 0, VersionHolding: null };
    console.log("logInCustomer", userToSend);
    const data = await url.post("CustomerVerification", logInCustomer);
    console.log("ResultverificationCustomer", data);
    return data.data;
  }, "verificationCustomer");
};

export const CustomersList = async (versionNumber: number = 0) => {
  return makeRequest(async () => {
    const data = await url.get(`Customers?customerVersion=${versionNumber}`);
    console.log("CustomersList", data);
    return data.data;
  }, "CustomersList");
};

export const getCustomerById = async (customerId: number) => {
  return makeRequest(async () => {
    const data = await url.get(`Customer/${customerId}`);
    console.log("getCustomerById", data);
  }, "getCustomerById");
};

export const deleteCustomerById = async (customerId: number) => {
  return makeRequest(async () => {
    const data = await url.delete(`Customer/${customerId}`);
    console.log("deleteCustomerById", data);
  }, "deleteCustomerById");
};

// to continue
export const editCustomerById = async (customerId: number) => {
  return makeRequest(async () => {
    const data = await url.put(`Customer/${customerId}`);
    console.log("deleteCustomerById", data);
  }, "deleteCustomerById");
};

export const getVersion = async () => {
  return makeRequest(async () => {
    const response = await url.get("Customerslist/version");
    console.log("getVersion", response.data);
    return response.data;
  }, "getVersion");
};
