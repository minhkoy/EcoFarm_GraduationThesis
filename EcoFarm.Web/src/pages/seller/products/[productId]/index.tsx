import DefaultOverlay from "@/components/ui/overlay/DefaultOverlay"
import { setFilterParams } from "@/config/reducers/products"
import useFetchOrders from "@/hooks/queries/useFetchOrders"
import useFetchProducts from "@/hooks/queries/useFetchProducts"
import { useAppDispatch } from "@/hooks/redux/useAppDispatch"
import SellerLayout from "@/layouts/seller/sellerLayout"
import { type NextPageWithLayout } from "@/pages/_app"
import { Button, Card, Flex, Grid, Image, NumberFormatter, Table, Text } from "@mantine/core"
//import { Button, Card, Image, Table, TableBody, TableCell, TableColumn, TableHeader, TableRow } from "@nextui-org/react"
//import { Datepicker } from "flowbite-react"
import { isArray, isUndefined } from "lodash-es"
import Link from "next/link"
import { useRouter } from "next/router"
import { useEffect, useMemo } from "react"

const SellerProductDetailScreen: NextPageWithLayout = () => {
  const router = useRouter()
  const query = router.query;
  const productId = useMemo(() => {
    if (isArray(query.productId)) {
      return isUndefined(query.productId[0])
        ? ''
        : query.productId[0]
    } else {
      return isUndefined(query.productId) ? '' : query.productId
    }
  }, [query.productId])
  const appDispatch = useAppDispatch()
  useEffect(() => {
    if (productId) {
      appDispatch(setFilterParams({
        id: productId
      }))
    }
  }, [productId, appDispatch])
  const { productData, isLoading } = useFetchProducts();
  const { orderData, isLoading: isLoadingOrders } = useFetchOrders();
  // useEffect(() => {

  // })

  if (isLoading) {
    return (
      <DefaultOverlay />
    )
  }
  if (!productData) {
    return (
      <div>Không tìm thấy dữ liệu!</div>
    )
  }

  const data = productData[0]!;
  const medias = data?.medias ?? [];

  return (
    <Flex
      gap={3}
      direction="column"
      className="mb-5"
    >
      <Card shadow="sm" padding="lg" radius="md" withBorder
        m={5}
      >
        <Grid
          columns={3}
          className="ml-3 mr-3 p-4 gap-3"
        >
          <Grid.Col span={1}>
            <Image
              loading="lazy"
              height={1000}
              src={medias.length > 0 ? medias[0]?.imageUrl : '/assets/test/Carrot-farming.jpg'}
              className=''
              alt='EcoFarm'
              width={500}
            />
          </Grid.Col>
          <Grid.Col span={2}>
            <Flex direction={'row'} justify={'space-between'}>
              <Text size='xl' fw='bold' c={'teal'}>
                {data?.name}
              </Text>
              <Button
                color="orange"

              >
                Cập nhật sản phẩm
              </Button>
            </Flex>
            <Table withRowBorders={false}>
              <Table.Tr>
                <Table.Td>Mã sản phẩm</Table.Td>
                <Table.Td>{data?.code}</Table.Td>
              </Table.Tr>
              <Table.Tr>
                <Table.Td>Tên sản phẩm</Table.Td>
                <Table.Td>{data?.name}</Table.Td>
              </Table.Tr>
              <Table.Tr>
                <Table.Td>Giá</Table.Td>
                <Table.Td className='text-primary-400'>
                  <NumberFormatter thousandSeparator value={data?.price} suffix=' VND' />
                </Table.Td>
              </Table.Tr>
              <Table.Tr>
                <Table.Td>Gói farming liên quan</Table.Td>
                <Table.Td className="text-primary-600 font-bold hover:underline">
                  <Link href={`/seller/packages/${data?.packageId}`}>{data?.packageName}</Link>
                </Table.Td>
              </Table.Tr>
              <Table.Tr>
                <Table.Td>Đã bán</Table.Td>
                <Table.Td>{data?.sold ?? 'Không có thông tin'}</Table.Td>
              </Table.Tr>
              <Table.Tr>
                <Table.Td>Còn lại</Table.Td>
                <Table.Td>{data?.quantityRemain ?? 'Không có thông tin'}</Table.Td>
              </Table.Tr>
            </Table>
          </Grid.Col>
        </Grid>
      </Card>
      {/* <Card shadow="sm" padding="lg" radius="md" withBorder
        m={5}
      >
        <Grid
          columns={4} m={3} p={4} className="gap-3"
        >
          <Grid.Col span={3}>
            Nhà cung cấp <span className='text-lg text-primary-600 font-bold'>
              {data?.sellerEnterpriseName} </span>
          </Grid.Col>
          <Grid.Col span={1}>
            <div className='flex justify-items-end gap-1'>
              <Link href={`/seller/${data?.sellerEnterpriseId}`}>
                <Button color='teal' size='lg'>Xem thông tin</Button>
              </Link>
              <Button color='gray' size='lg'
                onClick={() => {
                  void router.push('/chat')
                }}
              >Nhắn tin</Button>
            </div>
          </Grid.Col>
        </Grid>
      </Card> */}
      <Card shadow="sm" padding="lg" radius="md" withBorder
        m={5}>
        <Flex direction={'column'} gap={3}>
          <Text fw={'bold'} size="xl" c={'teal'}
            className="uppercase">Các đơn hàng</Text>
        </Flex>
      </Card>
    </Flex>
    // <div className="flex flex-col gap-3 mb-5">
    //   Product ID: {productId}
    //   <Card className=' grid grid-cols-3 gap-3 ml-3 mr-3 p-4'>
    //     <div>
    //       <Image
    //         height={1000}
    //         src='/assets/test/Carrot-farming.jpg'
    //         className=''
    //         alt='EcoFarm'
    //         width={500}
    //       />
    //       <Button color="primary" className="w-full mt-2">
    //         Đặt mua
    //       </Button>
    //     </div>
    //     <div className="col-span-2">
    //       <span className='text-primary-400 font-bold text-3xl'>{data?.name}</span>
    //       <Table hideHeader>
    //         <TableHeader>
    //           <TableColumn>NAME</TableColumn>
    //           <TableColumn>VALUE</TableColumn>
    //         </TableHeader>
    //         <TableBody>
    //           <TableRow>
    //             <TableCell>Mã sản phẩm</TableCell>
    //             <TableCell>{data?.code}</TableCell>
    //           </TableRow>
    //           <TableRow>
    //             <TableCell>Tên sản phẩm</TableCell>
    //             <TableCell>{data?.name}</TableCell>
    //           </TableRow>
    //           <TableRow>
    //             <TableCell>Giá</TableCell>
    //             <TableCell className='text-primary-400'>{splitDigits(data.price!)} VND</TableCell>
    //           </TableRow>
    //           <TableRow>
    //             <TableCell>Gói farming liên quan</TableCell>
    //             <TableCell>
    //               <Link href={`/packages/${data?.packageId}`}
    //                 className="focus:text-primary-400"
    //               >
    //                 {data?.packageName}
    //               </Link>
    //             </TableCell>
    //           </TableRow>
    //           <TableRow>
    //             <TableCell>Đã bán</TableCell>
    //             <TableCell>{data?.sold ?? 'Không có thông tin'}</TableCell>
    //           </TableRow>
    //           <TableRow>
    //             <TableCell>Còn lại</TableCell>
    //             <TableCell>{data?.quantityRemain ?? 'Không có thông tin'}</TableCell>
    //           </TableRow>
    //           {/* <TableRow>
    //             <TableCell>Test Datepicker</TableCell>
    //             <TableCell>
    //               <Datepicker />
    //             </TableCell>
    //           </TableRow> */}
    //           {/* <TableRow>
    //             <TableCell>Test Datepicker</TableCell>
    //             <TableCell>
    //               <DatePicker />
    //             </TableCell>
    //           </TableRow> */}
    //           <TableRow>
    //             <TableCell>Nhà cung cấp/ Chủ trang trại</TableCell>
    //             <TableCell>
    //               <Link href={`/erp/${data?.sellerEnterpriseId}`}
    //                 className="focus:text-primary-400"
    //               >
    //               </Link>
    //               {data?.sellerEnterpriseName ?? 'Không có thông tin'}
    //             </TableCell>
    //           </TableRow>
    //         </TableBody>
    //       </Table>
    //     </div>
    //   </Card>
    //   <Card className='grid grid-cols-4 ml-3 mr-3 p-4'>
    //     <div className='col-span-3'>
    //       Nhà cung cấp <span className='text-lg text-primary-600 font-bold'>
    //         {data?.sellerEnterpriseName}
    //       </span>
    //     </div>
    //     <div className='flex justify-items-end gap-3'>
    //       <Link href={`/seller/${data?.sellerEnterpriseId}`}>
    //         <Button color='primary' size='lg'>Xem thông tin</Button>
    //       </Link>
    //       <Button color='secondary' size='lg'>Nhắn tin</Button>
    //     </div>
    //   </Card>
    // </div>
  )
}

SellerProductDetailScreen.getLayout = function getLayout(page) {
  return (
    <SellerLayout>
      {page}
    </SellerLayout>
  )
}

export default SellerProductDetailScreen;