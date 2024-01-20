import { updatePackage } from "@/config/apis/packages"
import { EFX } from "@/utils/constants/constants"
import { ToastHelper } from "@/utils/helpers/ToastHelper"
import { useMutation } from "@tanstack/react-query"
//import { useTranslation } from "next-i18next"
import { useRouter } from "next/router"

export default function useUpdatePackage() {
  const router = useRouter()
  //const { t } = useTranslation()
  return useMutation({
    mutationKey: ['updatePackage'],
    mutationFn: updatePackage,
    onSuccess: ({ data }) => {
      if (data.isSuccess) {
        ToastHelper.success(
          'Thành công',
          data.successMessage ?? 'Cập nhật gói farming thành công!' //t('success', { ns: 'farm-package-review' }),
        )
        void router.push(`/seller/packages/${data.value?.id}`)
      } else {
        ToastHelper.error(
          'Lỗi',
          data.errors.join('. ') ?? EFX.DEFAULT_ERROR_MESSAGE //data.errors.join('. '),
        )
      }
    },
  })
}