import { RemoveFromCart } from "@/config/apis/cart";
import { EFX } from "@/utils/constants/constants";
import { ToastHelper } from "@/utils/helpers/ToastHelper";
import { useMutation } from "@tanstack/react-query";
import { useRouter } from "next/router";

export default function useRemoveFromCart() {
  const router = useRouter();
  return useMutation({
    mutationKey: ['removeProductFromCart'],
    mutationFn: RemoveFromCart,
    onSuccess: ({ data }) => {
      if (data.isSuccess) {
        ToastHelper.success(
          'Thành công',
          data.successMessage ?? 'Thêm vào giỏ hàng thành công' //XXX
        )
        router.reload();
      }
      else {
        ToastHelper.error(
          'Lỗi',
          data.errors.join('. ') ?? EFX.DEFAULT_ERROR_MESSAGE//data.errors.join('. '),
        )
      }
    }
  })
}