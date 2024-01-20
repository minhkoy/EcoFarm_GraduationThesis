import { createActivityApi } from "@/config/apis/activity"
import { EFX } from "@/utils/constants/constants"
import { ToastHelper } from "@/utils/helpers/ToastHelper"
import { useMutation } from "@tanstack/react-query"
import { useTranslation } from "next-i18next"

export default function useCreateActivity() {
  const { t } = useTranslation()
  const { mutate, data, isPending } = useMutation({
    mutationKey: ['createActivity'],
    mutationFn: createActivityApi,
    onSuccess: ({ data }) => {
      if (data.isSuccess) {
        ToastHelper.success(
          'Thành công',
          data.successMessage ?? 'Thêm mới hoạt động thành công' //t('success', { ns: 'farm-package-review' }),
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
    mutate: mutate,
    data: data?.data.value,
    isPending: isPending
  }
}