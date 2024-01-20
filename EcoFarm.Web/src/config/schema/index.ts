import { ACCOUNT_TYPE } from '@/utils/constants/enums'
import { capitalize, omit } from 'lodash-es'
import { type TFunction } from 'next-i18next'
import { z } from 'zod'

export const createCommonSchema = <T extends TFunction>(t: T) => {
  const passwordSchema = z
    .string()
    .min(8, {
      message: capitalize(
        t('auth:validation.password.min', {
          min: 8,
        }),
      ),
    })
    .max(20, {
      message: capitalize(
        t('auth:validation.password.max', {
          max: 20,
        }),
      ),
    })
  // .regex(
  //   /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(.{8,20})$/g,
  //   t('validation.password.invalid', {
  //     ns: 'auth',
  //   }),
  // )

  const emailSchema = z
    .string()
    .min(1, { message: capitalize(t('auth:validation.email.isRequired')) })
    .email({
      message: capitalize(t('auth:validation.email.isInValid')),
    })

  const dateSchema = z.date({
    required_error: capitalize(t('common:validation.date.isRequired')),
    invalid_type_error: capitalize(t('common:validation.date.isInValid')),
  })

  return {
    passwordSchema,
    emailSchema,
    dateSchema,
  }
}

export const createLoginSchema = <T extends TFunction>(t: T) => {
  return z.object({
    usernameOrEmail: z.string().min(1, {
      message: capitalize(t('auth:validation.usernameOrEmail.isRequired')),
    }),
    password: createCommonSchema(t).passwordSchema,
    isRemember: z.boolean().optional().default(false),
  })
}

export const createSignUpSchema = <T extends TFunction>(t: T) => {
  return z
    .object({
      username: z.string(),
      name: z.string(),
      taxCode: z.string(),
      email: createCommonSchema(t).emailSchema,
      password: createCommonSchema(t).passwordSchema,
      confirmPassword: createCommonSchema(t).passwordSchema,
      accountType: z.nativeEnum(omit(ACCOUNT_TYPE, ['ADMIN', 'SA']), {
        errorMap: (issue) => {
          switch (issue.code) {
            case 'invalid_enum_value':
            case 'invalid_type':
              return {
                message: capitalize(
                  t('validation.account-type.isRequired', { ns: 'auth' }),
                ),
              }
            default:
              return {
                message: issue.message ?? '',
              }
          }
        },
      }),
    })
    .refine((data) => data.password === data.confirmPassword, {
      message: capitalize(t('auth:validation.password.notMatch')),
      path: ['confirmPassword'],
    })
    .refine(
      ({ accountType, taxCode }) => {
        if (accountType !== ACCOUNT_TYPE.SELLER) return true
        return taxCode && taxCode.length > 0
      },
      {
        path: ['taxCode'],
        message: capitalize(t('validation.taxCode.isRequired', { ns: 'auth' })),
      },
    )
    .refine(
      ({ accountType, username }) => {
        if (accountType !== ACCOUNT_TYPE.CUSTOMER) return true
        return username && username.length > 0
      },
      {
        path: ['username'],
        message: capitalize(
          t('validation.username.isRequired', { ns: 'auth' }),
        ),
      },
    )
}

export const createForgotPasswordSchema = <T extends TFunction>(t: T) => {
  return z.object({
    email: createCommonSchema(t).emailSchema,
  })
}

// Package
export const createNewPackageSchema = () => {
  return z.object({
    code: z.string(),
    name: z.string()
      .min(5, { message: 'Tên gói dịch vụ cần có ít nhất 5 ký tự' }),
    description: z.string(),
    estimatedStartTime: z.date().nullable(),
    estimatedEndTime: z.date().nullable(),
    price: z.number()
      .min(0, { message: 'Giá gói farming phải là số dương' }),
    quantity: z.number()
      .min(0, { message: 'Số lượng đăng ký phải là số dương' }),
    serviceType: z.number(),
    isAutoCloseRegister: z.boolean(),
    avatar: z.string()
    //avatar: z.unknown() as z.ZodType<Blob>,
  })

}

export const updatePackageSchema = () => {
  return z.object({
    id: z.string(),
    code: z.string(),
    name: z.string()
      .min(5, { message: 'Tên gói dịch vụ cần có ít nhất 5 ký tự' }),
    description: z.string(),
    estimatedStartTime: z.date().nullable(),
    estimatedEndTime: z.date().nullable(),
    price: z.number()
      .min(0, { message: 'Giá gói farming phải là số dương' }),
    quantityRemain: z.number(),
    isAutoCloseRegister: z.boolean(),
    avatar: z.string()
    //avatar: z.unknown() as z.ZodType<Blob>,
  })
}

//Activity

export const createNewActivitySchema = () => {
  return z.object({
    code: z.string(),
    packageId: z.string(),
    title: z.string()
      .min(5, { message: 'Tiêu đề hoạt động cần có ít nhất 5 ký tự' })
      .max(40, { message: 'Tiêu đề hoạt động không được vượt quá 40 ký tự' }),
    shortDescription: z.string()
      .min(5, { message: 'Mô tả ngắn hoạt động cần có ít nhất 5 ký tự' })
      .max(50, { message: 'Mô tả ngắn hoạt động không được vượt quá 50 ký tự' }),
    content: z.string()
      .min(5, { message: 'Nội dung hoạt động cần có ít nhất 5 ký tự' }),
    mainImage: z.string(),
    medias: z.array(z.string()),
    images: z.array(z.string()),
  })
}

// Package review
export const createNewPackageReviewSchema = <T extends TFunction>(t: T) => {
  return z.object({
    packageId: z.string(),
    rating: z.number()

      .min(1, t('error.rating_out_of_range', {
        ns: 'farm-package-review',
        fromRating: 1,
        toRating: 5,
      }))
      .max(5),
    content: z.string(),
  })
}

export type LoginSchemaType = z.infer<ReturnType<typeof createLoginSchema>>
export type SignUpSchemaType = z.infer<ReturnType<typeof createSignUpSchema>>
export type ForgotPasswordSchemaType = z.infer<
  ReturnType<typeof createForgotPasswordSchema>
>

export type CreatePackageSchemaType = z.infer<ReturnType<typeof createNewPackageSchema>>
export type UpdatePackageSchemaType = z.infer<ReturnType<typeof updatePackageSchema>>
export type CreatePackageReviewSchemaType = z.infer<ReturnType<typeof createNewPackageReviewSchema>>;
export type CreateActivitySchemaType = z.infer<ReturnType<typeof createNewActivitySchema>>;