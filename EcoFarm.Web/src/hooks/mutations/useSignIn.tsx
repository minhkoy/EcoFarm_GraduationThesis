import { loginApi } from '@/config/apis/authentication'
import { ACCESS_TOKEN, ACCOUNT_TYPE } from '@/utils/constants/enums'
import { COMMON_LINK, SELLER_LINK } from '@/utils/constants/links'
import { ToastHelper } from '@/utils/helpers/ToastHelper'
import * as signalR from '@microsoft/signalr'
import { useMutation } from '@tanstack/react-query'
import { setCookie } from 'cookies-next'
import { useTranslation } from 'next-i18next'
import { useRouter } from 'next/router'

// const notificationHubContext = createContext<signalR.HubConnection | undefined>(new signalR.HubConnectionBuilder()
//   .withUrl(`https://localhost:7019/hubs/notification`, {
//     withCredentials: false,
//     //accessTokenFactory: () => `${accessToken}`
//   })
//   .build())

export default function useSignIn() {
  const router = useRouter()
  const { t } = useTranslation()
  return useMutation({
    mutationKey: ['login'],
    mutationFn: loginApi,
    onSuccess: async ({ data }) => {
      if (data.isSuccess) {
        ToastHelper.success(
          'Thành công',
          t('login.success', { ns: 'auth' }),
        )
        const result = data.value;
        console.log(result)
        setCookie(ACCESS_TOKEN, result.accessToken)

        // const { connection, start } = notificationHubConnection;
        // await start();
        const notificationConnection = new signalR.HubConnectionBuilder()
          .withUrl(`https://localhost:7019/hubs/notification`, {
            withCredentials: false,
            accessTokenFactory: () => `${result.accessToken}`
          })
          .withAutomaticReconnect()
          .build();
        notificationConnection.on("ReceiveNotification", (message) => {
          console.log(message);
        })
        try {
          await notificationConnection.start();
        } catch (err) {
          console.log('SignalR connect error:', err)
        }

        switch (result.accountType) {
          case ACCOUNT_TYPE.SELLER:
            console.log('seller')
            void router.push(`/${SELLER_LINK.HOMEPAGE}`)

            break;
          default:
            void router.push(`/${COMMON_LINK.HOMEPAGE}`)
        }
      } else {
        ToastHelper.error(
          'Lỗi',
          data.errors.join('. '),
        )
      }
    },
  })
}
