import { getListAddress } from "@/config/apis/address"
import { useQuery } from "@tanstack/react-query"
import { shallowEqual } from "react-redux"
import { useDebounce } from "use-debounce"
import { useAppSelector } from "../redux/useAppSelector"

const queryKey = ['addresses', 'getListAddresses']
export default function useFetchAddresses(timeout?: number) {
  const [params] = useDebounce(useAppSelector((state) => state.addresses, shallowEqual), timeout ?? 300)

  const { data, isLoading, refetch } = useQuery({
    //staleTime: 1000,
    enabled: true,
    queryKey: [...queryKey, params],
    queryFn: () =>
      getListAddress({
        ...params,
      }),
  })

  return {
    addressData: data?.data.value,
    isLoading: isLoading,
    refetch: refetch,
  }
}
