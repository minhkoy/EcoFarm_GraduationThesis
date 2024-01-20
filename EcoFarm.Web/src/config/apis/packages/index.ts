import { axiosClient } from '@/config/lib/axiosConfig'
import { type CreatePackageSchemaType, type UpdatePackageSchemaType } from '@/config/schema'
import { type ResponseModel } from '@/models/helpers/response.model'
import { type QueryPackages, type QuerySinglePackage, type ResponsePackage, type ResponsePackages } from '@/models/package.model'
import { type AxiosPromise } from 'axios'

const controller = '/FarmingPackage'

//Get
export const getListPackages = async (
  params: QueryPackages
): AxiosPromise<ResponsePackages> =>
  axiosClient.get(`${controller}/GetList`, { params })

export const getSinglePackage = async (
  params: QuerySinglePackage,
): AxiosPromise<ResponsePackage> =>
  axiosClient.get(`${controller}/Get`, { params })

//Post 

export const createPackage = async (
  body: CreatePackageSchemaType
): AxiosPromise<ResponsePackage> =>
  axiosClient.post(`${controller}/Create`, body)

// Put
export const updatePackage = async (body: UpdatePackageSchemaType)
  : AxiosPromise<ResponsePackage> => axiosClient.put(`${controller}/Update`, body)

export const registerPackage = async (id?: string): AxiosPromise<ResponseModel<boolean>> =>
  axiosClient.post(`${controller}/Register/${id}`);

//Patch -- for erp
export const closeRegisterPackage = async (id?: string): AxiosPromise<ResponseModel<boolean>> =>
  axiosClient.patch(`${controller}/CloseRegister/${id}`);

export const startPackage = async (id?: string): AxiosPromise<ResponseModel<boolean>> =>
  axiosClient.patch(`${controller}/Start/${id}`);

export const endPackage = async (id?: string): AxiosPromise<ResponseModel<boolean>> =>
  axiosClient.patch(`${controller}/End/${id}`);

//Patch -- for admin
export const approvePackage = async (id?: string): AxiosPromise<ResponseModel<boolean>> =>
  axiosClient.patch(`${controller}/Approve/${id}`);

export const rejectPackage = async (id?: string): AxiosPromise<ResponseModel<boolean>> =>
  axiosClient.patch(`${controller}/Reject/${id}`);