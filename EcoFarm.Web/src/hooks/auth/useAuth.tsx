import { LINK_AUTH } from '@/utils/constants/links'
import { deleteCookie, getCookies } from 'cookies-next'
import { keys } from 'lodash-es'
import { useRouter } from 'next/router'
import { useCallback } from 'react'
import useGetMyAccountInfo from '../queries/useGetMyAccountInfo'

export default function useAuth() {
  const { replace, locale } = useRouter()
  const { data, isFetching } = useGetMyAccountInfo(true)

  const logOut = useCallback(() => {
    const allCookies = getCookies()
    keys(allCookies).map((key) => deleteCookie(key))
    void replace(LINK_AUTH.LOGIN, undefined, { locale })
  }, [locale, replace])

  return { accountInfo: data?.data.value, isFetching, logOut }
}
