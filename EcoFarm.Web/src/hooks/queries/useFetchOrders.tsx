import { getListOrders } from "@/config/apis/orders"
import { useQuery } from "@tanstack/react-query"
import { shallowEqual } from "react-redux"
import { useDebounce } from "use-debounce"
import { useAppSelector } from "../redux/useAppSelector"

const queryKey = ['orders', 'getListOrders']
export default function useFetchOrders(timeout?: number) {
  const [params] = useDebounce(useAppSelector((state) => state.orders, shallowEqual), timeout ?? 500)

  const { data, isLoading } = useQuery({
    staleTime: 1000,
    queryKey: [...queryKey, params],
    queryFn: () =>
      getListOrders({
        ...params,
      }),
  })

  return {
    orderData: data?.data.value,
    isLoading: isLoading,
  }
}
