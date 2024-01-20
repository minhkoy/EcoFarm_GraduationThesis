import { env } from '@/env'
import { ACCESS_TOKEN, ERROR_CODES } from '@/utils/constants/enums'
import { ToastHelper } from '@/utils/helpers/ToastHelper'
import axios from 'axios'
import { getCookie } from 'cookies-next'
import { i18n } from 'next-i18next'
import queryString from 'query-string'

export const axiosClient = axios.create({
  baseURL: env.NEXT_PUBLIC_API_URL,
  headers: {
    Accept: 'application/json',
    'Content-Type': 'application/json; charset=utf-8',
  },
  paramsSerializer: (params) => queryString.stringify(params),
})

export const axiosClientTaxCode = axios.create({
  baseURL: env.NEXT_PUBLIC_TAX_CODE_API,
  headers: {
    Authorization: `Bearer ${env.NEXT_PUBLIC_TAX_CODE_ACCESS_TOKEN}`,
    Accept: 'application/json',
    'Content-Type': 'application/json; charset=utf-8',
  },
  paramsSerializer: (params) => queryString.stringify(params),
})

axiosClient.interceptors.request.use(async (config) => {
  if (getCookie(ACCESS_TOKEN)) {
    config.headers.Authorization = `Bearer ${getCookie(ACCESS_TOKEN)}`
  }
  config.headers['Accept-Language'] = i18n?.language
  // Log the request method and URL
  console.log(`Request: ${config.method?.toUpperCase()} ${config.url}`)

  // Log the request headers
  console.log('Headers:', config.headers)

  // Log the request data
  if (config.data) {
    console.log('Data:', config.data)
  }

  return config
})

axiosClient.interceptors.response.use(
  (response) => {
    return response
  },
  async (error) => {
    if (axios.isAxiosError(error)) {
      debugger;
      switch (error.response?.status) {
        case ERROR_CODES.UNAUTHORIZED:
          ToastHelper.error(
            "Lỗi",//i18n?.t('access-expired.title', { ns: 'error' }) ?? 'Error',
            //i18n?.t('access-expired.description', { ns: 'error' }) ??
            'Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại'// 'Access expired',
          )
          break
        case ERROR_CODES.FORBIDDEN:
          ToastHelper.error(
            "Lỗi",//i18n!.t('access-denied.title', { ns: 'error' }) ?? 'Error',
            //i18n!.t('access-denied.description', { ns: 'error' }) ??
            'Bạn không có quyền truy cập thông tin này'// 'Access denied',
          )
          break
        case ERROR_CODES.INTERNAL_SERVER_ERROR:
          ToastHelper.error(
            "Lỗi",//i18n?.t('server-error.title', { ns: 'error' }) ?? 'Error',
            "Đã có lỗi xảy ra. Vui lòng thử lại sau"//i18n?.t('server-error.description', { ns: 'error' }) ?? 'Something went wrong'
          )
        case ERROR_CODES.BAD_REQUEST:
        case ERROR_CODES.NOT_FOUND:
          ToastHelper.error(
            "Lỗi",//i18n?.'Lỗi' ?? 'Error',
            // eslint-disable-next-line @typescript-eslint/no-unsafe-argument, @typescript-eslint/no-unsafe-member-access
            error.response.data.errors ??
            //i18n?.t('default-error.description', { ns: 'error' }) ??
            'Đã có lỗi xảy ra',
          )
          break
      }
    }
    return Promise.reject(error)
  },
)
