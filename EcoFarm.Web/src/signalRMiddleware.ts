import * as signalR from "@microsoft/signalr";
import { type Middleware } from "@reduxjs/toolkit";
import { getCookie } from "cookies-next";
import { ACCESS_TOKEN } from "./utils/constants/enums";

const notificationMiddleware: Middleware = store => next => action => {
  const accessToken = getCookie(ACCESS_TOKEN);
  const connection = new signalR.HubConnectionBuilder()
    .withUrl(`https://localhost:7019/hubs/notification`, {
      withCredentials: false,
      accessTokenFactory: () => `${accessToken}`
    })
    .build()
  //debugger;

  connection.on("ReceiveNotification", (message) => {
    console.log(message);
  })
  connection.start()
    .then(() => console.log("Connection started!"))
    .catch((err) => console.log("Error while establishing connection: ", err));
  next(action);

}

// export function testSignalRMiddleware() {

// }

export default notificationMiddleware;