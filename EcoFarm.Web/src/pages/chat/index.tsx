import useGetMyAccountInfo from "@/hooks/queries/useGetMyAccountInfo";
import SellerLayout from "@/layouts/seller/sellerLayout";
import { ACCESS_TOKEN } from "@/utils/constants/enums";
import { Button, Card, Flex, Grid, Input } from "@mantine/core";
import * as signalR from "@microsoft/signalr";
import { getCookie } from "cookies-next";
import { useEffect, useRef, useState } from "react";
import { type NextPageWithLayout } from "../_app";

const ChatScreen: NextPageWithLayout = () => {
  const { data: myAccountInfoResponse } = useGetMyAccountInfo();
  const [newMessage, setNewMessage] = useState<string>('');
  const myAccountInfo = myAccountInfoResponse?.data.value;
  const [messageList, setMessageList] = useState<Array<string>>([]);
  const accessToken = getCookie(ACCESS_TOKEN);
  const conn = useRef<signalR.HubConnection>(new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7019/hubs/chat", {
      withCredentials: false,
      accessTokenFactory: () => `${accessToken}`,
    })
    .build()
  );
  const connection = conn.current;
  useEffect(() => {
    connection.on("ReceiveMessage", (message: string) => {
      setMessageList([...messageList, message]);
    })
    if (connection.state === signalR.HubConnectionState.Disconnected) {
      void connection.start().catch((err) => console.log("Có lỗi xảy ra: ", err))
    }
  }, [connection, messageList])

  // const getLayout = (page) => {
  //   if (myAccountInfo?.accountType === ACCOUNT_TYPE.SELLER)
  //     return (
  //       <SellerLayout>
  //         {page}
  //       </SellerLayout>
  //     )
  //   if (myAccountInfo?.accountType === ACCOUNT_TYPE.CUSTOMER)
  //     return (
  //       <MainLayout>
  //         {page}
  //       </MainLayout>
  //     )
  // }
  return (
    <Card m={5}>
      <Grid columns={7}>
        <Grid.Col span={2} bg={'teal'}>
        </Grid.Col>
        <Grid.Col span={5} bg={'lime'}>
          <Flex direction={'column'} justify={'space-between'}>
            {messageList.map((message, index) => (
              <div key={index}>
                {message}
              </div>
            ))}
            <Flex direction={'row'} justify={'space-between'}>
              <Input
                placeholder="Nhập tin nhắn ở đây..."
                value={newMessage}
                width={'100%'}
                onChange={(event) => setNewMessage(event.currentTarget.value)}
              />
              <Button
                color="teal"
                onClick={() => {
                  connection.send("Send", newMessage)
                    .then(() => setNewMessage(""))
                    .catch((err) => console.log("Có lỗi xảy ra: ", err));
                }}
              >
                Gửi
              </Button>
            </Flex>
          </Flex>
        </Grid.Col>
      </Grid>
    </Card>
  )
}

// ChatScreen.getLayout = function getLayout(page) {

//   return (
//     <MainLayout>
//       {page}
//     </MainLayout>
//   )
// }

ChatScreen.getLayout = function getLayout(page) {
  //const { data: myAccountInfoResponse } = useGetMyAccountInfo();
  return (
    <SellerLayout>
      {page}
    </SellerLayout>
  )
}
export default ChatScreen;
