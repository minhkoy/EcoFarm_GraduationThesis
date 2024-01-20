import { type ResponseModel } from "./helpers/response.model";

export type ShoppingCart = {
  id?: string;
  isOrdered?: boolean;
  totalQuantity?: number;
  totalPrice?: number;
  products?: Array<CartDetail>;
}

export type CartDetail = {
  productId?: string;
  productName?: string;
  productImage?: string;
  productPrice?: number;
  quantity?: number;
}

export type ResponseCart = ResponseModel<ShoppingCart>

