import { getUserInfoApi } from '@/config/apis/user'
import { useQuery } from '@tanstack/react-query'

export const queryKeys = ['user', 'getMyUserInfo']

export default function useGetUserInfo(enabled = false) {
  return useQuery({
    enabled,
    queryKey: [...queryKeys],
    queryFn: getUserInfoApi,
  })
}
