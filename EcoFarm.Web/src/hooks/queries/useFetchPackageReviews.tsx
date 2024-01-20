import { shallowEqual } from "react-redux"
import { useAppSelector } from "../redux/useAppSelector"
import { useQuery } from "@tanstack/react-query"
import { getListPackageReviews } from "@/config/apis/packageReviews"
import { useAppDispatch } from "../redux/useAppDispatch"
import { setFilterParams } from "@/config/reducers/packageReviews"

export const queryKey = ['packageReviews', 'getPackageReviews']

export default function useFetchPackageReviews(packageId?: string) {
    const appDispatch = useAppDispatch();
    appDispatch(setFilterParams({
        packageId: packageId!
      }))
    const params = useAppSelector((state) => state.packageReviews, shallowEqual)

    const {data, isLoading} = useQuery({
        //enabled: false,
        staleTime: 0,
        queryKey: [...queryKey, params],
        queryFn: () =>
          getListPackageReviews({
            ...params,
          }),
    })
    
    return {
        packageReviewsData: data?.data.value,
        isLoading: isLoading,
    }
}