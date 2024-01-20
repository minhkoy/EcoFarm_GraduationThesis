import { axiosClient } from "@/config/lib/axiosConfig";
import { type CreateOrderSchemaType } from "@/config/schema/order";
import { type QueryOrders, type ResponseOrder, type ResponseOrders } from "@/models/order.model";
import { type AxiosPromise } from "axios";

const controller = "/Order";
export const getListOrders = async (
  params: QueryOrders
): AxiosPromise<ResponseOrders> =>
  axiosClient.get(`${controller}/GetList`, { params })

export const createOrder = async (
  body: CreateOrderSchemaType
): AxiosPromise<ResponseOrder> => axiosClient.post(`${controller}/Create`, body)

export const approveOrder = async (
  orderId: string
): AxiosPromise<ResponseOrder> => axiosClient.patch(`${controller}/Approve/${orderId}`)