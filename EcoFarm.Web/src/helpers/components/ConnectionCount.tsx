import { useState, useEffect } from "react";
import signalREvents from "../Utils/SignalREvents";
import * as signalR from "@microsoft/signalr";

const ConnectionCount = () => {
    const [onlineCount, setOnlineCount] = useState(0);
    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:7019/user-connection', {withCredentials: false})
            .build();
        connection.start();
        connection.on(signalREvents.UserOnline, (count) => {
            console.log(count);
            setOnlineCount(count);
        });
    }, []);
    return (
        <section>
            <p className='text-base'>Đang trực tuyến: {onlineCount}</p>
        </section>
    )
}

export default ConnectionCount;