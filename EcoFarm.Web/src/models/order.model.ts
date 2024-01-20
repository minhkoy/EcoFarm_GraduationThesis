import {
  type CURRENCY_TYPE,
  type PAYMENT_METHOD
} from '@/utils/constants/enums'
import { type EnterpriseModel } from './enterprise.model'
import { type QueryRequest } from './helpers/query.model'
import { type ResponseModel } from './helpers/response.model'
import { type ProductModel } from './product.model'
import { type UserModel } from './user.model'

export type OrderModel = {
  orderId: string
  orderCode: string
  name: string
  userId: Pick<UserModel, 'id'>
  note: string
  seller: EnterpriseModel
  listProducts: Array<ProductModel>
  addressId: string
  addressDescription: string
  receiverName: string
  receiverPhone: string
  createdAt: Date
  createdBy?: string
  updatedAt?: Date
  totalPrice?: number
  totalQuantity?: number
  currency?: keyof typeof CURRENCY_TYPE
  currencyName?: string
  paymentMethod?: keyof typeof PAYMENT_METHOD
  paymentMethodName?: string
  status?: number
  statusName?: string
}

export type ResponseOrders = ResponseModel<Array<OrderModel>>
export type ResponseOrder = ResponseModel<OrderModel>

export type QueryOrders = QueryRequest<{
  status?: number;
  createdFrom?: Date;
  createdTo?: Date;
  keyword?: string;
}>


