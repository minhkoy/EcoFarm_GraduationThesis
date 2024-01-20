import { axiosClient } from '@/config/lib/axiosConfig'
import { type ResponseUser } from '@/models/user.model'
import { type AxiosPromise } from 'axios'

const controller = '/User'

export const getUserInfoApi = async (): AxiosPromise<ResponseUser> =>
  axiosClient.get(`${controller}/GetMyUserInfo`)
