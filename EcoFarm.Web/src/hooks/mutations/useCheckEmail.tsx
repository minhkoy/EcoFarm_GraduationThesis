import { checkEmailApi } from '@/config/apis/account'
import { useMutation } from '@tanstack/react-query'

export default function useCheckEmail() {
  const { mutate, isPending, data } = useMutation({
    mutationKey: ['checkEmail'],
    mutationFn: (mail: string) => checkEmailApi(mail),
  })
  return {
    mutate,
    isPending,
    data: data?.data.value,
  }
}
