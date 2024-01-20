// import useAuth from "@/hooks/auth/useAuth";
// import useFetchOrders from "@/hooks/queries/useFetchOrders";
// import SellerLayout from "@/layouts/seller/sellerLayout";
// import { type QueryOrders } from "@/models/order.model";
// import { type NextPageWithLayout } from "@/pages/_app";
// import { EFX } from "@/utils/constants/constants";
// import { useRouter } from "next/router";
// import { useState } from "react";

// const SellerProductScreen: NextPageWithLayout = () => {
//   const router = useRouter();
//   const { accountInfo, isFetching } = useAuth();
//   const [filterOrderParams, setFilterOrderParams] = useState<QueryOrders>({
//     createdFrom: undefined,
//     createdTo: undefined,
//     keyword: '',
//     limit: EFX.DEFAULT_LIMIT,
//     page: EFX.DEFAULT_PAGE,
//     status: undefined,
//   })

//   const { orderData, isLoading } = useFetchOrders();
//   return (
//     <></>
//   )
// }

// SellerProductScreen.getLayout = function getLayout(page) {
//   return (
//     <SellerLayout>
//       {page}
//     </SellerLayout>
//   )
// }

// export default SellerProductScreen;
import DefaultOverlay from "@/components/ui/overlay/DefaultOverlay";
import { setFilterParams } from "@/config/reducers/products";
import useAuth from "@/hooks/auth/useAuth";
import useFetchProducts from "@/hooks/queries/useFetchProducts";
import { useAppDispatch } from "@/hooks/redux/useAppDispatch";
import SellerLayout from "@/layouts/seller/sellerLayout";
import { type QueryProducts } from "@/models/product.model";
import { type NextPageWithLayout } from "@/pages/_app";
import { Button, Card, Flex, Grid, Text, TextInput } from "@mantine/core";
import Link from "next/link";
import { useRouter } from "next/router";
import { useEffect, useState } from "react";
import { EFX } from "src/utils/constants/constants";

const SellerProductListScreen: NextPageWithLayout = () => {
  const router = useRouter();
  const { accountInfo, isFetching } = useAuth();
  const [filterProductParams, setFilterProductParams] = useState<QueryProducts>({
    enterpriseId: accountInfo?.accountEntityId,
    keyword: '',
    limit: EFX.DEFAULT_LIMIT,
    page: EFX.DEFAULT_PAGE,
    code: '',
    id: '',
    isActive: undefined,
    maximumPrice: undefined,
    minimumPrice: undefined,
    maximumQuantity: undefined,
    minimumQuantity: undefined,
    packageId: ''
  })
  const appDispatch = useAppDispatch();
  useEffect(() => {
    if (accountInfo && !isFetching) {
      appDispatch(setFilterParams({
        ...filterProductParams,
        enterpriseId: accountInfo?.accountEntityId
      }));
    }
  }, [accountInfo, accountInfo?.accountEntityId, appDispatch, filterProductParams, isFetching]
  )
  const { productData, isLoading } = useFetchProducts();
  //appDispatch(setFilterParams(filterProductParams));
  if (isFetching || isLoading) {
    return (
      <DefaultOverlay />
    )
  }

  const onSubmitSearch = () => {
    appDispatch(setFilterParams(filterProductParams));
  }
  // if (!accountInfo || accountInfo.accountType !== ACCOUNT_TYPE.SELLER) {
  //   return (
  //     <ForbiddenScreen />
  //   )
  // }

  return (
    <Card m={5} padding={3}
      shadow="md" radius={'md'} withBorder>
      <Flex direction={'column'} justify={'center'} gap={3}>
        <Flex direction={'row'} justify={'space-between'}>
          <Text size="xl" fw={'bold'} c={'teal'} className="uppercase">Danh sách sản phẩm do {accountInfo?.fullName} quản lý</Text>
          <Button color={'teal'} onClick={() => {
            void router.push('/seller/products/create');
          }}>Thêm sản phẩm mới</Button>
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
                placeholder={'Mã/ Tên sản phẩm ...'}
                onChange={(e) => {
                  setFilterProductParams({
                    ...filterProductParams,
                    keyword: e.currentTarget.value

                  })
                }}
              />
            </Grid.Col>
            {
              productData?.map((item) => (
                <Grid.Col span={1}>
                  <Link href={`/seller/products/${item.id}`}>
                    <Card shadow="sm" padding="lg" radius="md" withBorder
                      m={5}
                    >
                      <Flex direction={'row'} justify={'space-between'}>
                        <Text c={'teal'} fw={'bold'}>{item.name}</Text>
                        <Text>{item.code}</Text>
                      </Flex>
                    </Card>
                  </Link>
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
          <Button color="indigo" disabled={filterProductParams.page! <= 1}
            onClick={() => {
              setFilterProductParams({
                ...filterProductParams,
                page: filterProductParams.page! - 1
              })
            }}
          >
            {'<'}
          </Button>
          <TextInput
            min={1}
            //width={50}
            size="sm"
            value={filterProductParams.page}
            onKeyDown={(e) => {
              if (e.key === 'Enter') {
                appDispatch(setFilterParams(filterProductParams))
              }
            }}
            onChange={(e) => {
              setFilterProductParams({
                ...filterProductParams,
                page: Number(e.currentTarget.value)
              })
            }}
          />
          <Button color="indigo"
            disabled={(productData?.length) ? (productData.length >= filterProductParams.limit! ? false : true) : true}
            onClick={() => {
              setFilterProductParams({
                ...filterProductParams,
                page: filterProductParams.page! + 1
              })
            }}
          > {'>'} </Button>
        </Flex>
      </Flex>
    </Card>
  )
}

SellerProductListScreen.getLayout = function getLayout(page) {
  return (
    <SellerLayout>
      {page}
    </SellerLayout>
  )
}

export default SellerProductListScreen;