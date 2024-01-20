import { type ResponseModel } from "./helpers/response.model"

export type AccountModel = {
  accountId: string
  fullName: string
  username: string
  email: string
  password: string
  isEmailConfirmed?: boolean
  accountType: string
  isActive?: boolean
  lockedReason?: string
  avatarUrl?: string
  accountEntityId?: string
  accessToken?: string
}

export type ResponseAccount = ResponseModel<AccountModel>