import { getTaxCodeApi } from '@/config/apis/authentication'
import { useQuery } from '@tanstack/react-query'
import { isEmpty } from 'lodash-es'
import { useEffect, useMemo, useRef } from 'react'
import { useDebounce } from 'use-debounce'
import { useAppSelector } from '../redux/useAppSelector'

export const queryKey = ['checkTaxCode']

export default function useCheckTaxCode(timeOut?: number) {
  const taxCode = useAppSelector((s) => s.auth.taxCode)
  const [debounced] = useDebounce(taxCode, timeOut ?? 500)
  const abortControllerRef = useRef<AbortController>(new AbortController())
  const { data: resultData, isFetching } = useQuery({
    enabled: !isEmpty(taxCode),
    queryKey: [...queryKey, debounced, abortControllerRef.current.signal],
    queryFn: () => getTaxCodeApi(debounced, abortControllerRef.current.signal),
  })

  useEffect(() => {
    if (!isEmpty(taxCode)) {
      abortControllerRef.current.abort()
      abortControllerRef.current = new AbortController()
    }
  }, [taxCode])

  return useMemo(() => {
    return {
      isError: resultData?.data.result === 'Failed',
      data: resultData?.data.data,
      errorText: resultData?.data.detail,
      isGettingTaxCode: isFetching,
    }
  }, [isFetching, resultData?.data])
}
