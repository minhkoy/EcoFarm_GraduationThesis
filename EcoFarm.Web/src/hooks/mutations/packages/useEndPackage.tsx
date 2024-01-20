import { endPackage } from "@/config/apis/packages";
import { ToastHelper } from "@/utils/helpers/ToastHelper";
import { useMutation } from "@tanstack/react-query";
import { useTranslation } from "next-i18next";
import { useRouter } from "next/router";

export default function useClosePackage() {
  const router = useRouter()
  const { t } = useTranslation();
  return useMutation({
    mutationKey: ['endPackage'],
    mutationFn: endPackage,
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