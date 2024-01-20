import * as signalR from '@microsoft/signalr';
import { createContext } from "react";

interface signalRContextProps {
  connection: signalR.HubConnection | null;
  startConnection: () => void;
  stopConnection: () => void;
}

const notificationHubConnection = new signalR.HubConnectionBuilder()
  .withUrl(`https://localhost:7019/hubs/notification`, {
    withCredentials: false,
    //accessTokenFactory: () => `${accessToken}`
  })
  .build();
notificationHubConnection.on("ReceiveNotification", (message) => {
  console.log(message);
})

const SignalRContext = createContext<signalRContextProps>({
  connection: notificationHubConnection,
  startConnection: () => {
    try {
      void notificationHubConnection.start();
    }
    catch (err) {
      console.log('SignalR connect error:', err)
    }
  },
  stopConnection: () => {
    try {
      void notificationHubConnection.stop();
    }
    catch (err) {
      console.log('SignalR disconnect error:', err)
    }
  },
});

// export function signalRProvider({children}) {
//   return
//   <SignalRContext.Provider
//   value={
//   }
//   >
//     {children}
//   </SignalRContext.Provider>
// }