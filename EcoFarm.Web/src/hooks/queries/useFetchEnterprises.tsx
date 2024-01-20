import { GetListEnterprise } from "@/config/apis/enterprise"
import { useQuery } from "@tanstack/react-query"
import { shallowEqual } from "react-redux"
import { useDebounce } from "use-debounce"
import { useAppSelector } from "../redux/useAppSelector"

const queryKey = ['enterprises', 'getListEnterprises']
export default function useFetchEnterprises(timeout?: number) {
  const [params] = useDebounce(useAppSelector((state) => state.enterprises, shallowEqual), timeout ?? 500)

  const { data, isLoading } = useQuery({
    staleTime: 1000,
    queryKey: [...queryKey, params],
    queryFn: () =>
      GetListEnterprise({
        ...params,
      }),
  })

  return {
    enterpriseData: data?.data.value,
    isLoading: isLoading,
  }
}
