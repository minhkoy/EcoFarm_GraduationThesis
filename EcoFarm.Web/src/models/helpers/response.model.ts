type BaseResponse = Readonly<{
  status: number
  isSuccess: boolean
  successMessage: string
  correlationId: string
  errors: ReadonlyArray<string>
  validationErrors: ReadonlyArray<ValidationError>
  resultType: number
  message: string
}>

type ValidationError = {
  identifier: string
  errorMessage: string
}

export type ResponseModel<T> = BaseResponse & {
  value: T
}
