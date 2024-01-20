import { setMainAddress } from "@/config/apis/address"
import { EFX } from "@/utils/constants/constants"
import { ToastHelper } from "@/utils/helpers/ToastHelper"
import { useMutation } from "@tanstack/react-query"
import { useTranslation } from "next-i18next"
import { useRouter } from "next/router"

export default function useSetMainAddress() {
  const { t } = useTranslation()
  const router = useRouter();
  const { mutate, data, isPending } = useMutation({
    mutationKey: ['setMainAddress'],
    mutationFn: setMainAddress,
    onSuccess: ({ data }) => {
      if (data.isSuccess) {
        ToastHelper.success(
          'Thành công',
          data.successMessage ?? ''
          //t('success', { ns: 'farm-package-review' }),
        );
        router.reload();
      } else {
        ToastHelper.error(
          'Lỗi',
          data.errors.join('. ') ?? EFX.DEFAULT_ERROR_MESSAGE //data.errors.join('. '),
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