import { getListPackages } from '@/config/apis/packages'
import { useQuery } from '@tanstack/react-query'
import { shallowEqual } from 'react-redux'
import { useDebounce } from 'use-debounce'
import { useAppSelector } from '../redux/useAppSelector'

export const queryKey = ['packages', 'getListPackages']

export default function useFetchPackage(timeout?: number) {
  const [params] = useDebounce(useAppSelector((state) => state.package, shallowEqual), timeout ?? 500)

  const { data, isLoading } = useQuery({
    //staleTime: 1000,
    queryKey: [...queryKey, params],
    queryFn: () =>
      getListPackages({
        ...params,
      }),
  })

  return {
    packageData: data?.data.value,
    isLoading: isLoading,
  }
}
