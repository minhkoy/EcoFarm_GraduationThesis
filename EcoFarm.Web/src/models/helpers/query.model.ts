export type QueryRequest<T = object> = {
  [x in keyof T]: T[x]
} & {
  page?: number
  limit?: number
  keyword?: string
}
