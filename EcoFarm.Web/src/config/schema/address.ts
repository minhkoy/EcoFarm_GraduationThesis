import { type TFunction } from "i18next"
import { z } from "zod"

export const createAddressSchema = <T extends TFunction>(t?: T) => {
  return z.object({
    addressDescription: z.string()
      .min(1, 'Không được để trống trường này.'),
    receiverName: z.string()
      .min(1, 'Không được để trống trường này.'),
    addressPhone: z.string()
      .min(1, 'Không được để trống trường này.'),
    isMain: z.boolean().nullable(),
  })
}

export type CreateAddressSchemaType = z.infer<ReturnType<typeof createAddressSchema>>