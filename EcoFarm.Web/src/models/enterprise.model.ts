import { type AccountModel } from './account.model'
import { type QueryRequest } from './helpers/query.model'
import { type ResponseModel } from './helpers/response.model'

export type EnterpriseModel = AccountModel & {
  enterpriseId: string
  address: string
  taxCode: string
  description: string
  avatarUrl: string
  hotline: string
}

export type ResponseEnterprise = ResponseModel<EnterpriseModel>
export type ResponseEnterprises = ResponseModel<Array<EnterpriseModel>>

export type QueryEnterprises = QueryRequest<{
  id?: string;
  accountId?: string;
  keyword?: string;

}>