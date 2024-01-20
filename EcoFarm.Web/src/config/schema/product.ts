import { z } from "zod";

export const createProductSchema = () => z.object({
  code: z.string()
    .min(1, { message: 'Cần có thông tin mã sản phẩm' })
    .max(10, { message: 'Mã sản phẩm không được vượt quá 10 ký tự' }),
  name: z.string()
    .min(1, { message: 'Cần có thông tin tên sản phẩm' })
    .max(100, { message: 'Tên sản phẩm không được vượt quá 100 ký tự' }),
  weight: z.number().nullable(),
  description: z.string()
    .min(10, { message: 'Mô tả sản phẩm cần có ít nhất 10 ký tự' })
    .max(800, { message: 'Mô tả sản phẩm không được vượt quá 800 ký tự' }),
  packageId: z.string().nullable(),
  quantity: z.number()
    .min(0, { message: 'Số lượng sản phẩm hiện có cần phải là số dương' }),
  price: z.number()
    .min(1, { message: 'Giá sản phẩm cần lớn hơn 0' }),
  priceForRegistered: z.number()
    //.min(1, { message: 'Giá sản phẩm cho người đã đăng ký gói cần lớn hơn 0' })
    .nullable(),
  avatar: z.string().nullable(),
})

export type createProductSchemaType = z.infer<ReturnType<typeof createProductSchema>>