import { getSinglePackage } from '@/config/apis/packages'
import { useQuery } from '@tanstack/react-query'
import { shallowEqual } from 'react-redux'
import { useAppSelector } from '../redux/useAppSelector'

export const queryKey = ['package', 'getSinglePackage']

export default function useFetchSinglePackage() {
  const params = useAppSelector((state) => state.singlePackage, shallowEqual)
  const { data, isLoading } = useQuery({
    refetchOnWindowFocus: false,
    enabled: (params.id ?? params.code) ? true : false,
    queryKey: [...queryKey, params],
    queryFn: () =>
      getSinglePackage(params),
  })
  return ({
    packageData: data?.data.value,
    isLoading: isLoading,
  })
}