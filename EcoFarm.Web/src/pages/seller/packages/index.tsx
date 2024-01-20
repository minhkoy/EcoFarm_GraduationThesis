import DefaultOverlay from "@/components/ui/overlay/DefaultOverlay";
import { setFilterParams } from "@/config/reducers/packages";
import useAuth from "@/hooks/auth/useAuth";
import useFetchPackage from "@/hooks/queries/useFetchPackage";
import { useAppDispatch } from "@/hooks/redux/useAppDispatch";
import SellerLayout from "@/layouts/seller/sellerLayout";
import { type QueryPackages } from "@/models/package.model";
import { type NextPageWithLayout } from "@/pages/_app";
import { Button, Card, Flex, Grid, Text, TextInput } from "@mantine/core";
import Link from "next/link";
import { useRouter } from "next/router";
import { useEffect, useState } from "react";
import { EFX } from "src/utils/constants/constants";

const SellerPackageListScreen: NextPageWithLayout = () => {
  const router = useRouter();
  const { accountInfo, isFetching } = useAuth();
  const [filterPackageParams, setFilterPackageParams] = useState<QueryPackages>({
    enterpriseId: accountInfo?.accountEntityId,
    keyword: '',
    limit: EFX.DEFAULT_LIMIT,
    page: EFX.DEFAULT_PAGE,
    priceFrom: 0,
    priceTo: 0,

  })
  const appDispatch = useAppDispatch();
  useEffect(() => {
    appDispatch(setFilterParams({
      ...filterPackageParams,
      enterpriseId: accountInfo?.accountEntityId
    }));
  }, [accountInfo?.accountEntityId, appDispatch, filterPackageParams])
  const { packageData, isLoading } = useFetchPackage();
  //appDispatch(setFilterParams(filterPackageParams));
  if (isFetching || isLoading) {
    return (
      <DefaultOverlay />
    )
  }

  const onSubmitSearch = () => {
    appDispatch(setFilterParams(filterPackageParams));
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
          <Text size="xl" fw={'bold'} c={'teal'} className="uppercase">Danh sách gói farming do {accountInfo?.fullName} quản lý</Text>
          <Button color={'teal'} onClick={() => {
            void router.push('/seller/packages/create');
          }}>Thêm gói farming mới</Button>
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
                placeholder={'Mã/ Tên gói farming ...'}
                onChange={(e) => {
                  setFilterPackageParams({
                    ...filterPackageParams,
                    keyword: e.currentTarget.value

                  })
                }}
              />
            </Grid.Col>
            {
              packageData?.map((item) => (
                <Grid.Col span={1}>
                  <Link href={`/seller/packages/${item.id}`}>
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
          <Button color="indigo" disabled={filterPackageParams.page! <= 1}
            onClick={() => {
              setFilterPackageParams({
                ...filterPackageParams,
                page: filterPackageParams.page! - 1
              })
            }}
          >
            {'<'}
          </Button>
          <TextInput
            min={1}
            //width={50}
            size="sm"
            value={filterPackageParams.page}
            onKeyDown={(e) => {
              if (e.key === 'Enter') {
                appDispatch(setFilterParams(filterPackageParams))
              }
            }}
            onChange={(e) => {
              setFilterPackageParams({
                ...filterPackageParams,
                page: Number(e.currentTarget.value)
              })
            }}
          />
          <Button color="indigo"
            disabled={(packageData?.length) ? (packageData.length >= filterPackageParams.limit! ? false : true) : true}
            onClick={() => {
              setFilterPackageParams({
                ...filterPackageParams,
                page: filterPackageParams.page! + 1
              })
            }}
          > {'>'} </Button>
        </Flex>
      </Flex>
    </Card>
  )
}

SellerPackageListScreen.getLayout = function getLayout(page) {
  return (
    <SellerLayout>
      {page}
    </SellerLayout>
  )
}

export default SellerPackageListScreen;