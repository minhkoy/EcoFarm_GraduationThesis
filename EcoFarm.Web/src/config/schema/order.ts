import { z } from "zod";

export const createOrderSchema = () => { //<T extends TFunction>(t: T) => {
  return z.object({
    productId: z.string()
      .min(1, 'Không được để trống trường này.'),
    quantity: z.number()
      .min(1, 'Số lượng phải từ 1 trở lên.'),
    note: z.string().nullable(),
    addressId: z.string().nullable(),
    paymentMethod: z.number().nullable(),
    cartProducts: z.array(z.object({
      productId: z.string(),
      quantity: z.number()
    }))
  })
}

export type CreateOrderSchemaType = z.infer<ReturnType<typeof createOrderSchema>>;