import { axiosClient } from "@/config/lib/axiosConfig";
import { type CreatePackageReviewSchemaType } from "@/config/schema";
import { type ResponsePackageReviews, type QueryPackageReviews, type ResponsePackageReview } from "@/models/packageReview.model";
import { type AxiosPromise } from "axios";

const controller = "/PackageReview";
export const getListPackageReviews = async (
    params: QueryPackageReviews
  ): AxiosPromise<ResponsePackageReviews> =>
    axiosClient.get(`${controller}/GetList`, { params })

export const createPackageReview = async (
    body: CreatePackageReviewSchemaType
  ): AxiosPromise<ResponsePackageReview> => 
    axiosClient.post(`${controller}/Create`, body)