import { getListActivities } from '@/config/apis/activity'
import { useQuery } from '@tanstack/react-query'
import { shallowEqual } from 'react-redux'
import { useDebounce } from 'use-debounce'
import { useAppSelector } from '../redux/useAppSelector'

export const queryKey = ['activities', 'getListActivities']

export default function useFetchActivities(timeout?: number) {
  const [params] = useDebounce(useAppSelector((state) => state.activity, shallowEqual), timeout ?? 500)

  const { data, isLoading } = useQuery({
    staleTime: 1000,
    queryKey: [...queryKey, params],
    queryFn: () =>
      getListActivities({
        ...params,
      }),
  })

  return {
    activityData: data?.data.value,
    isLoading: isLoading,
  }
}
