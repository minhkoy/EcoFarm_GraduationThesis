import { axiosClient } from "@/config/lib/axiosConfig";
import { type createProductSchemaType } from "@/config/schema/product";
import { type QueryProducts, type ResponseProduct, type ResponseProducts } from "@/models/product.model";
import { type AxiosPromise } from "axios";

const controller = '/Product'
export const getListProducts = async (
  params: QueryProducts
): AxiosPromise<ResponseProducts> =>
  axiosClient.get(`${controller}/Get`, { params })

export const createProduct = async (
  body: createProductSchemaType
): AxiosPromise<ResponseProduct> =>
  axiosClient.post(`${controller}/Create`, body)