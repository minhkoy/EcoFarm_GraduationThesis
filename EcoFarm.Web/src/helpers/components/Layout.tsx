import { Outlet } from "react-router-dom";
import Navbar from "./Navbar";
import Footer from "./Footer";
// import { useEffect, useState } from "react";
// import * as signalR from "@microsoft/signalr";
// import signalREvents from "../Utils/SignalREvents";

const Layout = () => {
    // const [onlineCount, setOnlineCount] = useState(0);
    // useEffect(() => {
    //     const connection = new signalR.HubConnectionBuilder()
    //         .withUrl('https://localhost:7019/user-connection', {withCredentials: false})
    //         .build();
    //     connection.start();
    //     connection.on(signalREvents.UserOnline, (count) => {
    //         console.log(count);
    //         setOnlineCount(count);
    //     });
    // }, []);
    return (
        <>
        <Navbar />
        <main className=" w-screen">
            <Outlet />
        </main>
        <Footer />
        {/* <p>Số lượng người dùng trực tuyến: {onlineCount}</p> */}
        </>
    )
}

export default Layout;