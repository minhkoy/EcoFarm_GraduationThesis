import { axiosClient, axiosClientTaxCode } from '@/config/lib/axiosConfig'
import { type LoginSchemaType, type SignUpSchemaType } from '@/config/schema'
import { type PersonalTaxModel } from '@/models/helpers/personal-tax.model'
import { type ResponseUser } from '@/models/user.model'
import { type AxiosPromise } from 'axios'

const controller = '/Authentication'

export const loginApi = async (
  params: LoginSchemaType,
): AxiosPromise<ResponseUser> => axiosClient.post(`${controller}/Login`, params)

export const signupApi = async (
  params: SignUpSchemaType,
): AxiosPromise<ResponseUser> =>
  axiosClient.post(
    `${controller}/${
      params.accountType === 'Customer' ? 'SignupAsUser' : 'SignupAsEnterprise'
    }`,
    params,
  )

export const getTaxCodeApi = async (
  q: string,
  signal: AbortSignal,
): AxiosPromise<PersonalTaxModel> =>
  axiosClientTaxCode.get('/tax_code', {
    signal,
    params: {
      q,
    },
  })
