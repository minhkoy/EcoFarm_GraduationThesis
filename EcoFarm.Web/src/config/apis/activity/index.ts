import { axiosClient } from "@/config/lib/axiosConfig";
import { type CreateActivitySchemaType } from "@/config/schema";
import { type QueryActivities, type ResponseActivities, type ResponseActivity } from "@/models/activity.model";
import { type AxiosPromise } from "axios";

const controller = "/Activity";
export const createActivityApi = async (
  body: CreateActivitySchemaType
): AxiosPromise<ResponseActivity> =>
  axiosClient.post(`${controller}/Create`, body)

export const getListActivities = async (
  params: QueryActivities
): AxiosPromise<ResponseActivities> =>
  axiosClient.get(`${controller}/GetList`, { params })