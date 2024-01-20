import DefaultOverlay from "@/components/ui/overlay/DefaultOverlay"
import { setAddressFilterParams } from "@/config/reducers/address"
import { setFilterParams } from "@/config/reducers/products"
import { createOrderSchema, type CreateOrderSchemaType } from "@/config/schema/order"
import useAddProductToCart from "@/hooks/mutations/cart/useAddNewProduct"
import useCreateOrder from "@/hooks/mutations/orders/useCreateOrder"
import useFetchAddresses from "@/hooks/queries/useFetchAddresses"
import useFetchProducts from "@/hooks/queries/useFetchProducts"
import { useAppDispatch } from "@/hooks/redux/useAppDispatch"
import MainLayout from "@/layouts/common/main"
import { type NextPageWithLayout } from "@/pages/_app"
import { PAYMENT_METHOD } from "@/utils/constants/enums"
import { Button, Card, Flex, Grid, Image, Modal, NumberFormatter, NumberInput, Select, Table, Text, Textarea, useCombobox } from "@mantine/core"
import { Form, useForm, zodResolver } from "@mantine/form"
import { useDisclosure } from "@mantine/hooks"
import { ShoppingCartIcon, TruckIcon } from "lucide-react"
//import { Button, Card, Image, Table, TableBody, TableCell, TableColumn, TableHeader, TableRow } from "@nextui-org/react"
import Link from "next/link"
import { useParams } from "next/navigation"
import { useRouter } from "next/router"

const ProductDetailScreen: NextPageWithLayout = () => {
  //---- FORM ---- //
  const params = useParams()
  const router = useRouter()
  const productId = router.query.productId as string;
  const { addressData, isLoading: isLoadingAddress } = useFetchAddresses();
  const newOrderForm = useForm<CreateOrderSchemaType>({
    initialValues: {
      productId: productId,
      quantity: 1,
      note: '',
      addressId: (addressData && addressData.length > 0) ? addressData[0]?.id : '',
      paymentMethod: null,
      cartProducts: []
    },
    validate: zodResolver(createOrderSchema()),
  })
  const combobox = useCombobox({
    onDropdownClose: () => combobox.resetSelectedOption()
  })
  //---- FORM END ---- //

  const [isOpenOrderModal, { open: openOrderModal, close: closeOrderModal }] = useDisclosure(false);
  // const productId = useMemo(() => {
  //   if (isArray(query.productId)) {
  //     return isUndefined(query.productId[0])
  //       ? ''
  //       : query.productId[0]
  //   } else {
  //     return isUndefined(query.productId) ? '' : query.productId
  //   }
  // }, [query.productId])
  const appDispatch = useAppDispatch()
  if (productId) {
    appDispatch(setFilterParams({
      id: productId
    }))
  }
  const { productData, isLoading } = useFetchProducts();
  const { mutate: createOrderMutate, isPending: pendingCreateOrder, data: newOrderData } = useCreateOrder();
  const { mutate: addProductToCartMutate, isPending: pendingAddToCart } = useAddProductToCart();
  //const { accountInfo, isFetching} = useAuth();

  const onAddToCart = () => {
    if (productId) {
      addProductToCartMutate(productId);
    }
  }

  if (isLoading) {
    return (
      <DefaultOverlay />
    )
  }
  if (!productData) {
    return (
      <div>Không tìm thấy dữ liệu</div>
    )
  }

  const data = productData[0]!;
  const medias = data.medias ?? [];

  const newOrderModal = (
    <Modal opened={isOpenOrderModal} onClose={closeOrderModal}
      title={<Text fw={'bold'}>Đặt mua sản phẩm</Text>} size="md"
    >
      <Form form={newOrderForm}>
        <Flex justify={'center'} direction={'column'} gap={3}>
          <NumberInput label="Số lượng"
            {...newOrderForm.getInputProps('quantity')}
          />
          <Textarea label='Ghi chú'
            {...newOrderForm.getInputProps('note')}
          />
          {
            isLoadingAddress
              ? <></> //<Select label='Địa chỉ...' placeholder="Đang tìm địa chỉ..." />
              :
              <Select
                data={addressData?.map((address) => ({
                  value: address.id,
                  label: `${address.receiverName} - ${address.addressPhone} - ${address.addressDescription}`
                  //label: address.addressDescription
                }))}
                checkIconPosition="right"
                label="Địa chỉ"
                searchable
                clearable
                nothingFoundMessage='Không tìm thấy địa chỉ nào'
                onSearchChange={(value) => {
                  appDispatch(setAddressFilterParams({
                    keyword: value
                  }))
                }}

                {...newOrderForm.getInputProps('addressId')}
              />
          }
          <Select
            label='Phương thức thanh toán'
            placeholder='Chọn phương thức thanh toán'
            data={PAYMENT_METHOD.map((method) => ({
              value: method[0]!.toString(),
              label: method[1]!
            }))}
            onChange={(val) => {
              newOrderForm.setFieldValue('paymentMethod', Number(val))
            }}
          //{...newOrderForm.getInputProps('paymentMethod')}
          />
          <Button color="teal" type="submit"
            loading={pendingCreateOrder}
            onClick={() => {
              const validateResult = newOrderForm.validate();
              if (validateResult.hasErrors) return;
              createOrderMutate(newOrderForm.values);
              if (!pendingCreateOrder) {
                newOrderForm.reset();
                closeOrderModal();
              }
            }}
          >
            Đặt hàng
          </Button>
        </Flex>
      </Form>
    </Modal>
  );

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
              height={1000}
              src={medias.length > 0 ? medias[0]?.imageUrl : '/assets/test/Carrot-farming.jpg'}
              className=''
              alt='EcoFarm'
              width={500}
            />
            <Button color="teal" fullWidth mt={3} mb={3}
              onClick={openOrderModal}>
              Đặt mua <TruckIcon className="ml-3" />
            </Button>
            <Button color="orange" fullWidth mt={3} mb={3}
              onClick={onAddToCart}
              loading={pendingAddToCart}
            >
              Thêm vào giỏ hàng <ShoppingCartIcon className="ml-3" />
            </Button>
            {
              newOrderModal
            }
          </Grid.Col>
          <Grid.Col span={2}>
            <Text size='xl' fw='bold' c={'teal'}>
              {data?.name}
            </Text>
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
                  <NumberFormatter thousandSeparator value={(data?.isUserRegisteredPackage && data?.priceForRegister) ? (data.priceForRegister ?? 0) : data?.price} suffix=' VND' />
                </Table.Td>
              </Table.Tr>
              <Table.Tr>
                <Table.Td>Gói farming liên quan</Table.Td>
                <Table.Td className="text-primary-600 font-bold hover:underline">
                  <Link href={`/packages/${data?.packageId}`}>{data?.packageName}</Link>
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
      <Card shadow="sm" padding="lg" radius="md" withBorder
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
      </Card>
      <Card shadow="sm" padding="lg" radius="md" withBorder
        m={5}>
        <Text size="xl" c={'teal'} fw={'bold'}>Mô tả</Text>
        <Text
        >
          {data?.description}
        </Text>
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

ProductDetailScreen.getLayout = function getLayout(page) {
  return (
    <MainLayout>
      {page}
    </MainLayout>
  )
}
export default ProductDetailScreen;