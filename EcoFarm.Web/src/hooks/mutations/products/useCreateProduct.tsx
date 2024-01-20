import { createProduct } from "@/config/apis/products"
import { ToastHelper } from "@/utils/helpers/ToastHelper"
import { useMutation } from "@tanstack/react-query"
import { useTranslation } from "next-i18next"
import { useRouter } from "next/router"

export default function useCreateProduct() {
  const router = useRouter();
  const { t } = useTranslation()
  const { mutate, data, isPending } = useMutation({
    mutationKey: ['createProduct'],
    mutationFn: createProduct,
    onSuccess: ({ data }) => {
      if (data.isSuccess) {
        ToastHelper.success(
          'Thành công',
          data.successMessage
          //t('success', { ns: 'farm-package-review' }),
        )
        void router.push(`/seller/products/${data.value.id}`)
      } else {
        ToastHelper.error(
          'Lỗi',
          data.errors.join('. ') //data.errors.join('. '),
        )
      }
    },
  })
  return {
    mutate: mutate,
    data: data?.data.value,
    isPending: isPending
  }
}