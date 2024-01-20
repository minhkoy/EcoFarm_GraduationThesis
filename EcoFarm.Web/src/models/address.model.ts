import { type QueryRequest } from "./helpers/query.model";
import { type ResponseModel } from "./helpers/response.model";

export type AddressModel = {
  id: string;
  userId: string;
  addressDescription: string;
  receiverName: string;
  addressPhone: string;
  isPrimary?: boolean;
  createdAt?: Date;
  modifiedAt?: Date;
}

export type ResponseAddresses = ResponseModel<Array<AddressModel>>
export type ResponseAddress = ResponseModel<AddressModel>

export type QueryAddresses = QueryRequest<{
  keyword?: string;
}>