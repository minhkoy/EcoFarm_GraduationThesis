import { getListProducts } from "@/config/apis/products"
import { useQuery } from "@tanstack/react-query"
import { shallowEqual } from "react-redux"
import { useDebounce } from "use-debounce"
import { useAppSelector } from "../redux/useAppSelector"

export const queryKey = ['products', 'getListProducts']

export default function useFetchProducts() {
  const [params] = useDebounce(useAppSelector((state) => state.product, shallowEqual), 500)
  // const [debounced] = useDebounce(params.keyword, timeout ?? 500)
  // const keywords = useAppSelector((state) => state.package.keyword)

  const { data, isLoading } = useQuery({
    staleTime: 1000 * 60 * 5,
    enabled: true,
    queryKey: [...queryKey, params],
    queryFn: () =>
      getListProducts({
        ...params,
      }),
  })

  return {
    productData: data?.data.value,
    isLoading: isLoading,
  }
}
