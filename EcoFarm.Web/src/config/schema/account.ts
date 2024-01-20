import { z } from "zod";

export const changePasswordSchema = () => z.object({
  oldPassword: z.string(),
  newPassword: z.string(),
  //.regex(new RegExp('/^(?=.*[0-9])(?=.*[A-Z]).{8,}$/'), { message: 'Mật khẩu mới phải có ít nhất 8 ký tự, trong đó có ít nhất 1 chữ hoa và 1 số' }),
  confirmPassword: z.string(),
})
  .refine(data => data.newPassword === data.confirmPassword, {
    message: 'Mật khẩu xác nhận không khớp',
    path: ['confirmPassword']
  })


export type ChangePasswordSchemaType = z.infer<ReturnType<typeof changePasswordSchema>>