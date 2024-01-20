import { type GENDER } from '@/utils/constants/enums'
import { type AccountModel } from './account.model'
import { type ResponseModel } from './helpers/response.model'

type Address = {
  id: string
  userId: string
  description: string
  receiverName: string
  phone: string
  isPrimary: boolean
  createAt: Date
  lastUpdateAt: Date
}

export type UserModel = AccountModel & {
  id: string
  phoneNumber: string
  gender?: keyof typeof GENDER
  dateOfBirth?: Date
  addresses?: Array<Address>
}

export type ResponseUser = ResponseModel<UserModel>

export type ResponseEmail = ResponseModel<{ isExist: boolean }>
