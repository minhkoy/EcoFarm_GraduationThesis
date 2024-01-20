import { ACCESS_TOKEN } from '@/utils/constants/enums';
import * as signalR from '@microsoft/signalr'
import { Button, Input } from '@nextui-org/react';
import { getCookie } from 'cookies-next';
import { useState } from 'react';

//Test signalR. Deleted soon
const TestChat = () => {
    const [messageList, setMessageList] = useState<Array<string>>([])
    const token = getCookie(ACCESS_TOKEN);
    console.log(token)
    const connection = new signalR.HubConnectionBuilder()
        .withUrl(`https://localhost:7019/chat`, {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets,
            accessTokenFactory: () => `Bearer ${token}`
        })
        .build();
    connection.on('ReceiveMessage', (message: string) => {
        setMessageList([...messageList, message]);
    })
    // async function signalRStart() {
    //     try {
    //         await connection.start();
    //     }
    //     catch (err) {
    //         console.log(err)
    //         setTimeout(signalRStart, 500);
    //     }
    // }
    connection.start()
        .then(() => console.log('Connected'))
        .catch((err: Error) => document.write(err.message));
    // connection.onclose(async () => {
    //     await signalRStart();
    // })
    // signalRStart();
    let msg = '';
    return (
        <>
        {messageList.forEach((msg, index) => {
            <p>{index} - {msg}</p>
        })}
        <Input onKeyDown={(e) => {
            if (e.key === 'Enter') {
                connection.send('SendMessage', {
                    Message: msg,
                })
                .then(() => {
                    msg = ''
                })
                .catch((err: Error) => {
                    console.log(err)
                })
            }
        }} onChange={(e) => msg = e.target.value} />
        <Button onClick={() => {
            connection.send('SendMessage', {
                    Message: msg,
                })
                .then(() => {
                    msg = ''
                })
                .catch((err: Error) => {
                    console.log(err)
                })
        }}>OK</Button>
        </>
    )
}

export default TestChat;