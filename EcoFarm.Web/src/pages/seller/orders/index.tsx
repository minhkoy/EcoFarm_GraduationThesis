import DefaultOverlay from "@/components/ui/overlay/DefaultOverlay";
import TextTitle from "@/components/ui/texts/TextTitle";
import { setOrderFilterParams } from "@/config/reducers/orders";
import { setFilterParams } from "@/config/reducers/products";
import useAuth from "@/hooks/auth/useAuth";
import useApproveOrder from "@/hooks/mutations/orders/useApproveOrder";
import useFetchOrders from "@/hooks/queries/useFetchOrders";
import useFetchProducts from "@/hooks/queries/useFetchProducts";
import { useAppDispatch } from "@/hooks/redux/useAppDispatch";
import SellerLayout from "@/layouts/seller/sellerLayout";
import { type OrderModel, type QueryOrders } from "@/models/order.model";
import { type NextPageWithLayout } from "@/pages/_app";
import { ORDER_STATUS, ORDER_STATUS_COLORS, ORDER_STATUS_NAME } from "@/utils/constants/enums";
import { dateFormat } from "@/utils/helpers/DateHelper";
import { Button, Card, Flex, Grid, Modal, NumberFormatter, Select, Table, Text, TextInput } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import Link from "next/link";
import { useRouter } from "next/router";
import { useEffect, useState } from "react";
import { EFX } from "src/utils/constants/constants";

const SellerOrderListScreen: NextPageWithLayout = () => {
  const router = useRouter();
  const { accountInfo, isFetching } = useAuth();
  const [isOpenDetailModal, { open: openDetailModal, close: closeDetailModal }] = useDisclosure(false);
  const [filterOrderParams, setFilterOrderParams] = useState<QueryOrders>({
    //enterpriseId: accountInfo?.accountEntityId,
    keyword: '',
    limit: EFX.DEFAULT_LIMIT,
    page: EFX.DEFAULT_PAGE,
    // priceFrom: 0,
    // priceTo: 0,
    createdFrom: undefined,
    createdTo: undefined,
    status: undefined,

  })
  const [selectedOrder, setSelectedOrder] = useState<OrderModel | undefined>(undefined);
  const appDispatch = useAppDispatch();
  useEffect(() => {
    appDispatch(setOrderFilterParams({
      ...filterOrderParams,
      //enterpriseId: accountInfo?.accountEntityId
    }));
    appDispatch(setFilterParams({
      keyword: '',
      enterpriseId: accountInfo?.accountEntityId
    }))
  }, [accountInfo?.accountEntityId, appDispatch, filterOrderParams])
  const { orderData, isLoading } = useFetchOrders();
  const { productData, isLoading: isLoadingProduct } = useFetchProducts();
  const { mutate: approveOrderMutate, } = useApproveOrder();

  const onApproveOrder = (order: OrderModel | undefined) => {
    if (!order) return;
    approveOrderMutate(order.orderId)
  }
  //appDispatch(setFilterParams(filterOrderParams));
  if (isFetching || isLoading) {
    return (
      <DefaultOverlay />
    )
  }

  const onSubmitSearch = () => {
    appDispatch(setOrderFilterParams(filterOrderParams));
  }
  // if (!accountInfo || accountInfo.accountType !== ACCOUNT_TYPE.SELLER) {
  //   return (
  //     <ForbiddenScreen />
  //   )
  // }

  // const orderDetailButton = (status: number) => {

  // }

  const confirmModal = (order: OrderModel | undefined) => {
    if (!order) return <></>;
    return (
      <Modal opened={isOpenDetailModal}
        onClose={closeDetailModal}
        title={<Text fw={'bold'} c={'teal'}>Chi tiết đơn hàng</Text>}
      >
        <Flex direction={'column'} gap={3} justify={'center'}>
          <Table withColumnBorders={false}>
            <Table.Tr>
              <Table.Td fw={'bold'}>Mã đơn hàng</Table.Td>
              <Table.Td>{order.orderCode}</Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>Ngày tạo</Table.Td>
              <Table.Td>{dateFormat(new Date(order.createdAt), EFX.DATETIME_FORMAT, "vi")}</Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>Trạng thái</Table.Td>
              <Table.Td fw={'bold'}>{order.statusName}</Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>Ghi chú</Table.Td>
              <Table.Td>{order.note}</Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>Địa chỉ</Table.Td>
              <Table.Td>{order.addressDescription}</Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>Số điện thoại</Table.Td>
              <Table.Td>{order.receiverPhone}</Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>Người nhận</Table.Td>
              <Table.Td>{order.receiverName}</Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>Sản phẩm</Table.Td>
              <Table.Td color="teal">
                <Table>
                  <Table.Thead>
                    <Table.Th>STT</Table.Th>
                    <Table.Th>Mã sản phẩm</Table.Th>
                    <Table.Th>Tên sản phẩm</Table.Th>
                    <Table.Th>Số lượng</Table.Th>
                    <Table.Th>Đơn giá</Table.Th>
                    <Table.Th>Thành tiền</Table.Th>
                  </Table.Thead>
                  <Table.Tbody>
                    {
                      order.listProducts.map((product, index) => (
                        <Table.Tr>
                          <Table.Td>{index + 1}</Table.Td>
                          <Table.Td>{product.code}</Table.Td>
                          <Table.Td>
                            <Link href={`/seller/products/${product.id}`}>
                              <Text c={'teal'} fw={'bold'} component="a">{product.name}</Text>
                            </Link>
                          </Table.Td>
                          <Table.Td>{product.quantity}</Table.Td>
                          <Table.Td>
                            <NumberFormatter thousandSeparator value={product.price} />
                          </Table.Td>
                          <Table.Td>
                            <NumberFormatter thousandSeparator value={product.price! * product.quantity!} />
                          </Table.Td>
                        </Table.Tr>
                      ))
                    }
                  </Table.Tbody>
                </Table>
                {/* <Link href={`/seller/products/${order.listProducts[0]?.id}`}>
                  <Text c={'teal'} fw={'bold'} component="a">{order.listProducts[0]?.name}</Text>
                </Link> */}
              </Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>Tổng số lượng</Table.Td>
              <Table.Td>{order.totalQuantity}</Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>Tổng giá</Table.Td>
              <Table.Td color="teal" fw={'bold'}>
                <NumberFormatter thousandSeparator value={order.totalPrice} /> VND
              </Table.Td>
            </Table.Tr>
          </Table>
          <Flex direction={'row'} gap={3} justify={'space-between'}>
            {
              order.status === ORDER_STATUS.WaitingSellerConfirm && (
                <>
                  <Button color="teal" w={'50%'}
                    onClick={() => {
                      onApproveOrder(order);
                      closeDetailModal();
                    }}>Xác nhận</Button>
                  <Button color="red" w={'50%'} >Từ chối</Button>
                </>
              )
            }
            {
              order.status === ORDER_STATUS.SellerConfirmed && (
                <>
                  <Button color="teal" w={'100%'}>Bắt đầu chuẩn bị hàng</Button>
                </>
              )
            }
          </Flex>
        </Flex>
      </Modal>
    )
  }
  return (
    <Card m={5} padding={3}
      shadow="md" radius={'md'} withBorder>
      <Flex direction={'column'} justify={'center'} gap={3}>
        <Flex direction={'row'} justify={'space-between'}>
          <Text size="xl" fw={'bold'} c={'teal'} className="uppercase">Danh sách order do {accountInfo?.fullName} quản lý</Text>
          {/* <Button color={'teal'} onClick={() => {
            void router.push('/seller/orders/create');
          }}>Thêm gói farming mới</Button> */}
        </Flex>
        <form onSubmit={
          (e) => {
            e.preventDefault();
            onSubmitSearch();
          }
        }>
          <Grid columns={3}>
            <Grid.Col span={3}>
              <TextInput m={3}
                // onKeyDown={(e) => {
                //   if (e.key === 'Enter') {

                //   }

                // }}           
                placeholder={'Mã order/ Note ...'}
                onChange={(e) => {
                  setFilterOrderParams({
                    ...filterOrderParams,
                    keyword: e.currentTarget.value

                  })
                }}
              />
            </Grid.Col>
            <Grid.Col span={1}>
              <Select
                m={3}
                placeholder={'Trạng thái'}
                data={ORDER_STATUS_NAME.map((item) => ({
                  value: item.status.toString(),
                  label: item.statusName,
                }))}
                value={filterOrderParams.status?.toString()}
                //clearable
                //value={ORDER_STATUS_NAME[filterOrderParams.status]}
                onChange={(val) => {
                  //console.log(val);
                  if (!val) {
                    setFilterOrderParams({
                      ...filterOrderParams,
                      status: 0
                    })
                    return;
                  }
                  setFilterOrderParams({
                    ...filterOrderParams,
                    status: val ? Number(val) : 0
                  })
                }}
              // onEmptied={() => {
              //   console.log('KK')
              //   setFilterOrderParams({
              //     ...filterOrderParams,
              //     status: undefined
              //   })

              // }}
              />
            </Grid.Col>
            <Grid.Col span={2}>
              <Select
                m={3}
                placeholder="Sản phẩm"
                searchable
                data={productData?.map((item) => ({
                  value: item.id!,
                  label: `${item.code ?? 'Không có mã sản phẩm'} - ${item.name!}`,
                }))}
                onSearchChange={(val) => {
                  appDispatch(setFilterParams({
                    keyword: val,
                    //enterpriseId: accountInfo?.accountEntityId
                  }))
                }}
              />
            </Grid.Col>
            <Grid.Col span={3}>
              <Card
                m={5} padding={3}
                shadow="md" radius={'md'} withBorder
              >
                <Flex direction={'column'} gap={3}>
                  <Flex direction={'row'} justify={'space-between'}>
                    <TextTitle>Các hóa đơn cần xác nhận</TextTitle>
                    <Button color="teal"
                      onClick={() => {
                        setFilterOrderParams({
                          ...filterOrderParams,
                          status: ORDER_STATUS.WaitingSellerConfirm
                        })
                      }}
                    >
                      Xem tất cả
                    </Button>
                  </Flex>
                  <Grid columns={3}>
                    {
                      orderData?.filter((order) => order.status === ORDER_STATUS.WaitingSellerConfirm)
                        .map((item) => (
                          <Grid.Col span={1}>
                            <Card shadow="sm" padding="lg" radius="md" withBorder
                              m={5}
                              onClick={() => {
                                setSelectedOrder(item);
                                openDetailModal();

                              }}
                            >
                              <Flex direction={'column'} justify={'space-between'}>
                                <Text c={'teal'} fw={'bold'}>{item.orderCode}</Text>
                                <Text>{dateFormat(new Date(item.createdAt), EFX.DATETIME_FORMAT, "vi")}</Text>
                                <span>
                                  <Text fw={'bold'} className="inline">Tài khoản tạo: </Text>
                                  <Text fw={'bold'} c={'teal'}
                                    component="a"
                                  // onClick={(e) => {
                                  //   e.preventDefault();
                                  //   void router.push(`/user/${item.userId}`)
                                  // }}
                                  >{item.createdBy}</Text>
                                </span>
                                <span>
                                  <Text fw={'bold'} className="inline">Trạng thái </Text>
                                  <Text fw={'bold'} c={!item.status ? '' : ORDER_STATUS_COLORS.get(item.status)}>{item.statusName}</Text>
                                </span>
                              </Flex>
                            </Card>
                          </Grid.Col>
                        ))
                    }
                  </Grid>
                </Flex>

              </Card>
            </Grid.Col>
            <Grid.Col span={3}>
              <Card
                m={5} padding={3}
                shadow="md" radius={'md'} withBorder
              >
                <Flex direction={'column'} gap={3}>
                  <Flex direction={'row'} justify={'space-between'}>
                    <TextTitle>Các hóa đơn đã xác nhận, chưa chuẩn bị hàng</TextTitle>
                    <Button color="teal"
                      onClick={() => {
                        setFilterOrderParams({
                          ...filterOrderParams,
                          status: ORDER_STATUS.SellerConfirmed
                        })
                      }}
                    >
                      Xem tất cả
                    </Button>
                  </Flex>
                  <Grid columns={3}>
                    {
                      orderData?.filter((order) => order.status === ORDER_STATUS.SellerConfirmed)
                        .map((item) => (
                          <Grid.Col span={1}>
                            <Card shadow="sm" padding="lg" radius="md" withBorder
                              m={5}
                              onClick={() => {
                                setSelectedOrder(item);
                                openDetailModal();

                              }}
                            >
                              <Flex direction={'column'} justify={'space-between'}>
                                <Text c={'teal'} fw={'bold'}>{item.orderCode}</Text>
                                <Text>{dateFormat(new Date(item.createdAt), EFX.DATETIME_FORMAT, "vi")}</Text>
                                <span>
                                  <Text fw={'bold'} className="inline">Tài khoản tạo: </Text>
                                  <Text fw={'bold'} c={'teal'}
                                    component="a"
                                  // onClick={(e) => {
                                  //   e.preventDefault();
                                  //   void router.push(`/user/${item.userId}`)
                                  // }}
                                  >{item.createdBy}</Text>
                                </span>
                                <span>
                                  <Text fw={'bold'} className="inline">Trạng thái </Text>
                                  <Text fw={'bold'} c={!item.status ? '' : ORDER_STATUS_COLORS.get(item.status)}>{item.statusName}</Text>
                                </span>
                              </Flex>
                            </Card>
                          </Grid.Col>
                        ))
                    }
                  </Grid>
                </Flex>

              </Card>
            </Grid.Col>
            {
              orderData?.map((item: OrderModel) => (
                <Grid.Col span={1}>
                  <Card shadow="sm" padding="lg" radius="md" withBorder
                    m={5}
                    onClick={() => {
                      setSelectedOrder(item);
                      openDetailModal();

                    }}
                  >
                    <Flex direction={'column'} justify={'space-between'}>
                      <Text c={'teal'} fw={'bold'}>{item.orderCode}</Text>
                      <Text>{dateFormat(new Date(item.createdAt), EFX.DATETIME_FORMAT, "vi")}</Text>
                      <span>
                        <Text fw={'bold'} className="inline">Tài khoản tạo: </Text>
                        <Text fw={'bold'} c={'teal'}
                          component="a"
                        // onClick={(e) => {
                        //   e.preventDefault();
                        //   void router.push(`/user/${item.userId}`)
                        // }}
                        >{item.createdBy}</Text>
                      </span>
                      <span>
                        <Text fw={'bold'} className="inline">Trạng thái </Text>
                        <Text fw={'bold'} c={!item.status ? '' : ORDER_STATUS_COLORS.get(item.status)}>{item.statusName}</Text>
                      </span>
                    </Flex>
                  </Card>
                </Grid.Col>
              ))
            }
          </Grid>
        </form>
        {/* <Pagination.Root total={1}>
          <Flex direction={'row'} justify={'center'} gap={2}>
            <Pagination.Previous component="a"
            />
            <Pagination.Next component="a" />
          </Flex>
        </Pagination.Root> */}
        <Flex direction={'row'} justify={'center'} gap={3}>
          <Button color="indigo" disabled={filterOrderParams.page! <= 1}
            onClick={() => {
              setFilterOrderParams({
                ...filterOrderParams,
                page: filterOrderParams.page! - 1
              })
            }}
          >
            {'<'}
          </Button>
          <TextInput
            min={1}
            //width={50}
            size="sm"
            value={filterOrderParams.page}
            onKeyDown={(e) => {
              if (e.key === 'Enter') {
                appDispatch(setOrderFilterParams(filterOrderParams))
              }
            }}
            onChange={(e) => {
              setFilterOrderParams({
                ...filterOrderParams,
                page: Number(e.currentTarget.value)
              })
            }}
          />
          <Button color="indigo"
            disabled={(orderData?.length) ? (orderData.length >= filterOrderParams.limit! ? false : true) : true}
            onClick={() => {
              setFilterOrderParams({
                ...filterOrderParams,
                page: filterOrderParams.page! + 1
              })
            }}
          > {'>'} </Button>
        </Flex>
      </Flex>
      {
        confirmModal(selectedOrder)
      }
    </Card>
  )
}

SellerOrderListScreen.getLayout = function getLayout(page) {
  return (
    <SellerLayout>
      {page}
    </SellerLayout>
  )
}

export default SellerOrderListScreen;