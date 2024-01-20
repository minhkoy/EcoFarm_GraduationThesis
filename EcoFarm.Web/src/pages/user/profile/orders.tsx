import DefaultOverlay from "@/components/ui/overlay/DefaultOverlay";
import { UserInfo } from "@/components/ui/personalInfo/UserInfo";
import TextTitle from "@/components/ui/texts/TextTitle";
import useFetchOrders from "@/hooks/queries/useFetchOrders";
import MainLayout from "@/layouts/common/main";
import { type OrderModel } from "@/models/order.model";
import { type NextPageWithLayout } from "@/pages/_app";
import { dateFormat } from "@/utils/helpers/DateHelper";
import { Card, Flex, Grid, NumberFormatter, Table, Text, type TableData } from "@mantine/core";

const OrderScreen: NextPageWithLayout = () => {
  const { orderData, isLoading } = useFetchOrders();
  if (isLoading) {
    return (
      <DefaultOverlay />
    )
  }

  const cardDetail = (order: OrderModel): TableData => {
    return {
      body: [
        [<Text fw={'bold'} c={'teal'}>Mã đơn hàng</Text>, <Text>{order.orderCode}</Text>],
        [<Text fw={'bold'} c={'teal'}>Trạng thái</Text>, <Text>{order.statusName}</Text>],
        [<Text fw={'bold'} c={'teal'}>Tổng số tiền</Text>, <NumberFormatter thousandSeparator value={order.totalPrice} />],
        [<Text fw={'bold'} c={'teal'}>Ngày tạo</Text>, <Text>{dateFormat(new Date(order.createdAt), "dd/MM/yyyy HH:mm:ss", "vn")}</Text>]
      ]
    }
  }

  return (
    <Grid columns={5}>
      <Grid.Col span={1}>
        <Card shadow="sm" m={5}>
          <UserInfo />
        </Card>
      </Grid.Col>
      <Grid.Col span={4}>
        <Flex direction={'column'} gap={3}>
          <Flex direction={'row'} justify={'center'}>
            <TextTitle>Các đơn hàng của bạn</TextTitle>
          </Flex>
          <Grid columns={3}>
            {
              orderData?.map((order) => (
                <Grid.Col span={1}>
                  <Card shadow="sm" m={5}>
                    <Table data={cardDetail(order)}>
                      {/* <Table.Tr>
                        <Table.Td>
                          Mã đơn hàng
                          </Table.Td>
                        <Table.Td>{order.orderCode}</Table.Td>
                      </Table.Tr>
                      <Table.Tr>
                        <Table.Td>Trạng thái</Table.Td>
                        <Table.Td>{order.statusName}</Table.Td>
                      </Table.Tr>
                      <Table.Tr>
                        <Table.Td>Tổng số tiền</Table.Td>
                        <Table.Td>
                          <NumberFormatter thousandSeparator value={order.totalPrice} />
                        </Table.Td>
                      </Table.Tr>
                      <Table.Tr>
                        <Table.Td>Ngày tạo</Table.Td>
                        <Table.Td>{dateFormat(new Date(order.createdAt), "dd/MM/yyyy HH:mm:ss", "vn")}</Table.Td>
                      </Table.Tr> */}
                    </Table>
                    {/* <Flex direction={'column'} gap={3}>
                      <Text>{order.orderCode}</Text>
                      <Text>{order.status}</Text>
                      <Text>{order.totalPrice}</Text>
                      <Text>{dateFormat(new Date(order.createdAt), "dd/MM/yyyy HH:mm:ss", "vn")}</Text>
                    </Flex> */}
                  </Card>
                </Grid.Col>
              ))
            }
          </Grid>
        </Flex>
      </Grid.Col>
    </Grid>
  )
}

OrderScreen.getLayout = function getLayout(page) {
  return (
    <MainLayout>
      {page}
    </MainLayout>
  )
}
export default OrderScreen;