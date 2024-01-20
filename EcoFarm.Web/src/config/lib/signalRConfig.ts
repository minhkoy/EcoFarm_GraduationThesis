import { ACCESS_TOKEN } from '@/utils/constants/enums';
import * as signalR from '@microsoft/signalr';
import { getCookie } from 'cookies-next';

const accessToken = getCookie(ACCESS_TOKEN);
const notificationConnection = new signalR.HubConnectionBuilder()
  .withUrl(`https://localhost:7019/hubs/notification`, {
    withCredentials: false,
    accessTokenFactory: () => `${accessToken}`
  })
  .build();
notificationConnection.on("ReceiveNotification", (message) => {
  console.log(message);
})
export const notificationHubConnection = {
  connection: notificationConnection,
  start: async () => {
    try {
      await notificationConnection.start();
    } catch (err) {
      console.log('SignalR connect error:', err)
    }
  }
}

// export const startNotificationConnection = async () => {
//   try {
//     await notificationHubConnection.start();
//   } catch (err) {
//     console.log('SignalR error:', err)
//   }
// }

//export default notificationHubConnection