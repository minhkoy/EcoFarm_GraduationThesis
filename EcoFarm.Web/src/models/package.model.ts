import {
  type CURRENCY_TYPE,
  type PACKAGE_REGISTER_STATUS,
  type PACKAGE_STATUS,
  type PACKAGE_TYPE,
  type SERVICE_PACKAGE_APPROVAL_STATUS,
} from '@/utils/constants/enums'
import { type EnterpriseModel } from './enterprise.model'
import { type QueryRequest } from './helpers/query.model'
import { type ResponseModel } from './helpers/response.model'
import { type PackageReviewModel } from './packageReview.model'
import { type UserModel } from './user.model'

export type PackageModel = {
  id: string
  code: string
  name: string
  createAt: Date
  createBy: string
  seller: EnterpriseModel
  description: string
  estimatedStartTime: Date
  estimatedEndTime: Date
  closeRegisterTime: Date
  startTime: Date
  endTime: Date
  price?: number
  currency: keyof typeof CURRENCY_TYPE
  currencyName: keyof typeof CURRENCY_TYPE
  quantityStart: number
  quantityRegistered: number
  quantityRemain: number
  rejectReason?: string
  servicePackageApprovalStatus: keyof typeof SERVICE_PACKAGE_APPROVAL_STATUS
  servicePackageApprovalStatusName?: string
  packageStatus: PACKAGE_STATUS
  packageStatusName?: string
  packageRegisterStatus: PACKAGE_REGISTER_STATUS
  packageRegisterStatusName?: string
  servicePackageType: keyof typeof PACKAGE_TYPE
  serviceTypeName: string
  registeredUsers?: Array<UserModel>
  numbersOfRating?: number
  averageRating?: number
  isRegisteredByCurrentUser?: boolean
  enterprise?: EnterpriseModel
  avatarUrl?: string
  reviews?: Array<PackageReviewModel>
}

export type ResponsePackages = ResponseModel<Array<PackageModel>>
export type ResponsePackage = ResponseModel<PackageModel>

export type QueryPackages = QueryRequest<{
  keyword?: string
  enterpriseId?: string
  priceFrom?: number
  priceTo?: number
  isStarted?: boolean
  isEnded?: boolean
}>

export type QuerySinglePackage = {
  id?: string
  code?: string
}
