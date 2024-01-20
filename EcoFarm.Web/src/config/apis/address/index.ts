import { axiosClient } from "@/config/lib/axiosConfig";
import { type CreateAddressSchemaType } from "@/config/schema/address";
import { type QueryAddresses, type ResponseAddress, type ResponseAddresses } from "@/models/address.model";
import { type AxiosPromise } from "axios";

const controller = "/Address";
export const createAddressApi = async (
  body: CreateAddressSchemaType
): AxiosPromise<ResponseAddress> =>
  axiosClient.post(`${controller}/Create`, body)

export const getListAddress = async (
  params: QueryAddresses
): AxiosPromise<ResponseAddresses> =>
  axiosClient.get(`${controller}/GetList`, { params })

export const setMainAddress = async (
  id: string
): AxiosPromise<ResponseAddress> =>
  axiosClient.put(`${controller}/SetMain/${id}`)

export const deleteAddress = async (
  id: string
): AxiosPromise<ResponseAddress> =>
  axiosClient.delete(`${controller}/Delete/${id}`)