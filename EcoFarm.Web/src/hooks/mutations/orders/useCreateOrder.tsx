import { createOrder } from "@/config/apis/orders";
import { ToastHelper } from "@/utils/helpers/ToastHelper";
import { useMutation } from "@tanstack/react-query";
//import { useTranslation } from "next-i18next"

export default function useCreateOrder() {
  const { mutate, data, isPending } = useMutation({
    mutationKey: ['createOrder'],
    mutationFn: createOrder,
    onSuccess: ({ data }) => {
      if (data.isSuccess) {
        ToastHelper.success(
          'Thành công',
          data.successMessage
          //t('success', { ns: 'farm-package-review' }),
        );

      } else {
        ToastHelper.error(
          'Lỗi',
          data.errors.join('. ') //data.errors.join('. '),
        )
      }
    },
  })
  return {
    rawData: data,
    mutate: mutate,
    data: data?.data.value,
    isPending: isPending
  }
}