import { changePasswordApi } from "@/config/apis/account"
import { EFX } from "@/utils/constants/constants"
import { ToastHelper } from "@/utils/helpers/ToastHelper"
import { useMutation } from "@tanstack/react-query"
import { useTranslation } from "next-i18next"

export default function useChangePassword() {
  const { t } = useTranslation()
  const { mutateAsync, data, isPending, isError } = useMutation({
    mutationKey: ['changePassword'],
    mutationFn: changePasswordApi,
    onSuccess: ({ data }) => {
      if (data.isSuccess) {
        ToastHelper.success(
          'Thành công',
          data.successMessage ?? 'Thay đổi mật khẩu thành công' //t('success', { ns: 'farm-package-review' }),
        )
      } else {
        ToastHelper.error(
          'Lỗi',
          data.errors.join('. ') ?? EFX.DEFAULT_ERROR_MESSAGE //data.errors.join('. '),
        )
      }
    },
  })
  return {
    mutate: mutateAsync,
    rawData: data,
    result: data?.data.value,
    isPending: isPending,
    isError: isError,
  }
}