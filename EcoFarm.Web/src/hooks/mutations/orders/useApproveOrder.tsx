import { approveOrder } from "@/config/apis/orders";
import { ToastHelper } from "@/utils/helpers/ToastHelper";
import { useMutation } from "@tanstack/react-query";
import { useTranslation } from "next-i18next";
import { useRouter } from "next/router";

export default function useApproveOrder() {
  const router = useRouter()
  const { t } = useTranslation();
  return useMutation({
    mutationKey: ['approveOrder'],
    mutationFn: approveOrder,
    onSuccess: ({ data }) => {
      if (data.isSuccess) {
        ToastHelper.success(
          'Thành công',
          data.successMessage //XXX
        )
        router.reload()
      }
      else {
        ToastHelper.error(
          'Lỗi',
          data.errors.join('. ') //data.errors.join('. '),
        )
      }
    }
  })
}