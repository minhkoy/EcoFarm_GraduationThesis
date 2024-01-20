import { type QueryRequest } from "./helpers/query.model";
import { type ResponseModel } from "./helpers/response.model";

export type ProductModel = {
  id?: string;
  code?: string;
  name?: string;
  description?: string;
  weight?: number;
  //Package info
  packageId?: string;
  packageCode?: string;
  packageName?: string;
  isUserRegisteredPackage?: boolean;
  //Package info ends
  quantity?: number;
  sold?: number;
  quantityRemain?: number;
  price?: number;
  priceForRegister?: number;
  currency?: number; // 0: VND, 1: USD - Not for using now.
  createdTime?: Date;
  sellerEnterpriseId?: string;
  sellerEnterpriseName?: string;
  medias?: Array<ProductMedia>;
}

export type ResponseProducts = ResponseModel<Array<ProductModel>>
export type ResponseProduct = ResponseModel<ProductModel>

export type QueryProducts = QueryRequest<{
  enterpriseId?: string;
  id?: string;
  code?: string;
  isActive?: boolean;
  packageId?: string;
  minimumQuantity?: number;
  maximumQuantity?: number;
  minimumPrice?: number;
  maximumPrice?: number;
}>

export type ProductMedia = {
  imageUrl?: string;
}