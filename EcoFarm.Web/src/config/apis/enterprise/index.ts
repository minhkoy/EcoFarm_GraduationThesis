import { axiosClient } from "@/config/lib/axiosConfig";
import { type QueryEnterprises, type ResponseEnterprises } from "@/models/enterprise.model";
import { type AxiosPromise } from "axios";

const controller = "/Enterprise";
export const GetListEnterprise = async (
  params: QueryEnterprises
): AxiosPromise<ResponseEnterprises> => axiosClient.get(`${controller}/Get`, { params })