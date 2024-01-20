
// export const addToCartSchema = z.object({
//   productId: z.string()
//   .min(1, { message: "Cần có thông tin sản phẩm!" })
// })

import { z } from "zod";

// export type AddToCartSchemaType = z.infer<typeof addToCartSchema>

export const removeFromCartSchema = z.object({
  cartId: z.string(),
  //.min(1, { message: "Cần có thông tin giỏ hàng!" }),
  productIds: z.array(z.string())
  //.min(1, { message: "Cần có thông tin sản phẩm!" })
})

export type RemoveFromCartSchemaType = z.infer<typeof removeFromCartSchema>