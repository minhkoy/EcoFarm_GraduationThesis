import { axiosClient } from '@/config/lib/axiosConfig'
import { type ChangePasswordSchemaType } from '@/config/schema/account'
import { type ResponseAccount } from '@/models/account.model'
import { type ResponseModel } from '@/models/helpers/response.model'
import { type ResponseEmail } from '@/models/user.model'
import { type AxiosPromise } from 'axios'

const controller = '/Account'

export const checkEmailApi = async (
  email: string,
): AxiosPromise<ResponseEmail> =>
  axiosClient.post(`${controller}/CheckEmail`, { email })

export const getMyAccountInfoApi = async (): AxiosPromise<ResponseAccount> =>
  axiosClient.get(`${controller}/GetMyAccountInfo`)

export const changePasswordApi = async (
  body: ChangePasswordSchemaType
): AxiosPromise<ResponseModel<boolean>> =>
  axiosClient.put(`${controller}/ChangePassword`, body)