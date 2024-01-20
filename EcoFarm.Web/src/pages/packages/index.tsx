import DefaultOverlay from '@/components/ui/overlay/DefaultOverlay'
import { setFilterParams } from '@/config/reducers/packages'
import useFetchEnterprises from '@/hooks/queries/useFetchEnterprises'
import useFetchPackage from '@/hooks/queries/useFetchPackage'
import { useAppDispatch } from '@/hooks/redux/useAppDispatch'
import MainLayout from '@/layouts/common/main'
import { type QueryPackages } from '@/models/package.model'
import { Flex, Button as MantineButton, NumberFormatter, Select, Text, TextInput } from '@mantine/core'
import {
  Button,
  Card,
  CardBody,
  CardFooter,
  Checkbox,
  Image,
  Input
} from '@nextui-org/react'
import { capitalize, isEmpty, toNumber } from 'lodash-es'
import { StarIcon } from 'lucide-react'
import { type GetServerSideProps } from 'next'
import { useTranslation } from 'next-i18next'
import config from 'next-i18next.config.mjs'
import { serverSideTranslations } from 'next-i18next/serverSideTranslations'
import { useRouter } from 'next/navigation'
import { useEffect, useState } from 'react'
import { type NextPageWithLayout } from '../_app'
//import Image from 'next/image'

export const getServerSideProps: GetServerSideProps = async ({ locale }) => {
  return {
    props: {
      ...(await serverSideTranslations(
        locale ?? 'vi',
        ['common', 'farm-package'],
        config,
      )),
    },

  }
}

const PackagesScreen: NextPageWithLayout = () => {
  const [filters, setFilters] = useState<QueryPackages>({
    limit: 10,
    page: 1,
    keyword: '',
    priceFrom: undefined,
    priceTo: undefined,
    enterpriseId: '',
    isStarted: undefined,
    isEnded: undefined,
  })
  const appDispatch = useAppDispatch()
  const { t } = useTranslation()
  const router = useRouter()
  const { packageData, isLoading } = useFetchPackage()
  useEffect(() => {
    appDispatch(setFilterParams(filters));
  }, [appDispatch, filters])
  const { enterpriseData, isLoading: isLoadingEnterprises } = useFetchEnterprises();
  //const packageData = useMemo(() => queryResult?.data.value, [queryResult])
  // const onChange = useCallback(
  //   (e: ChangeEvent<HTMLSelectElement>) => {
  //   setFilters(prev => ({
  //     ...prev,
  //     limit: toNumber(e.target.value)
  //   }))
  //   },
  //   [],
  // )

  const onSubmitSearch = () => setFilters(filters)

  if (isLoading) {
    return (
      <DefaultOverlay />
    )
  }
  return (
    <div className='grid grid-cols-3 gap-4'>
      <form className='col-span-3 flex flex-col bg-primary p-4 sm:col-span-1'
        onKeyDown={(e) => {
          if (e.key === 'Enter') {
            onSubmitSearch();
          }
        }}
      >
        <div className='mb-4 flex flex-col gap-3'>
          <TextInput
            label='Tên gói farming'
            type='text'
            placeholder='Tên gói farming...'
            //placeholder={capitalize(t("info.name", { ns: "farm-package" }))}
            onChange={(e) => {
              appDispatch(setFilterParams({
                keyword: e.target.value
              }));
            }}
          />
          <Select
            label='Nhà cung cấp/ Chủ trang trại'
            placeholder='Nhà cung cấp/ Chủ trang trại'
            data={enterpriseData?.map((enterprise) => ({
              value: enterprise.enterpriseId,
              label: enterprise.fullName,
            })) ?? []}
            onChange={(value) => {
              // appDispatch(setFilterParams({
              //   enterpriseId: value ?? ''
              // }))
              setFilters({
                ...filters,
                enterpriseId: value ?? ''
              })
            }}
          />
        </div>
        <div className='mb-4'>
          <p>{capitalize(t('query-param.range-price', { ns: 'farm-package' }))}</p>
          <div className=' flex flex-row justify-center gap-3'>
            <Input
              type='number'
              pattern='[0-9]'
              min={0}
              placeholder={capitalize(t('query-param.from-price', { ns: 'farm-package' }))}
              onKeyDown={(e) => {
                if (e.key === '.') {
                  e.preventDefault();
                }
              }}
              onChange={(e) => {
                setFilters({
                  ...filters,
                  priceFrom: toNumber(e.target.value),
                })
              }}
            />
            <Input type='number'
              placeholder={capitalize(t('query-param.to-price', { ns: 'farm-package' }))}
              onChange={(e) => {
                setFilters({
                  ...filters,
                  priceTo: toNumber(e.target.value),
                })
              }}
            />
          </div>
        </div>
        <div className='mb-4'>
          <Checkbox
            isSelected={filters.isStarted}
            onValueChange={(e) => {
              setFilters({
                ...filters,
                isStarted: e,
              })
            }}
          >
            Đã bắt đầu
            {/* {capitalize(t('query-param.is-started', { ns: 'farm-package' }))} */}
          </Checkbox>
        </div>
        <div className='mb-4'>
          <Checkbox
            isSelected={filters.isEnded}
            onValueChange={(e) => {
              setFilters({
                ...filters,
                isEnded: e,
              })
            }}
          >
            Đã kết thúc
            {/* {capitalize(t('query-param.is-ended', { ns: 'farm-package' }))} */}
          </Checkbox>
        </div>
        <div className='mb-4 object-center'>
          <Button
            color='primary'
            onClick={onSubmitSearch}
          >
            {t('search', { ns: 'common' })}
          </Button>
        </div>
      </form>

      <div className='col-span-3 bg-primary p-4 sm:col-span-2'>
        {/* <Select
          items={SELECT_LIMIT}
          label='Limit'
          placeholder='Select limit'
          className='max-w-xs'
          isLoading={isLoading}
          onChange={onChange}
          //defaultSelectedKeys={[toString(currentLimit)]}
        >
          {(items) => (
            <SelectItem key={items.value} color='primary'>
              {items.label}
            </SelectItem>
          )}
        </Select> */}
        <div className=''>
          {!packageData || (isEmpty(packageData) && filters.page === 1) ? (
            <Text c={'red'} fw={'bold'}>{capitalize('Không tìm thấy gói')}</Text>
          ) : (
            <>
              <p className='text-)primary-600 uppercase font-bold'>
                {capitalize(
                  t('info.farm-package', {
                    ns: 'farm-package',
                    total: packageData.length,
                  }),
                )}
              </p>
              <div className='grid grid-cols-3 justify-start gap-2'>
                {packageData?.map((_package, index) => (
                  <Card
                    className='m-2 w-56'
                    shadow='md'
                    key={index}
                    isPressable
                    onPress={() => {
                      router.push(`./packages/${_package.id}`)
                    }}
                  >
                    <CardBody className='overflow-visible p-0'>
                      <Image
                        src={_package.avatarUrl ?? '/assets/brands/logo.png'}
                        alt='Logo'
                        shadow='sm'
                        radius='lg'
                        width='100%'
                        className='h-[140px] w-full object-cover text-center'
                      />
                      {/* {cn(
                          t('farming-package', { ns: 'common' }), ':',
                          t('has-price', { ns: 'farm-package', name: _package.name, price: _package.price })
                        )} */}
                    </CardBody>
                    <CardFooter className='m-2 justify-between text-small'>
                      <div className='m-2'>
                        <b>{_package.name}</b>
                        <p className='text-default-500'>
                          <NumberFormatter thousandSeparator value={_package.price} suffix=' VND' />
                        </p>
                      </div>
                      <div className='m-2'>
                        <span className='font-bold'>
                          {_package.averageRating}
                          <StarIcon className='h-4 w-4 text-yellow-500' />
                        </span>
                      </div>
                    </CardFooter>
                  </Card>
                ))}
              </div>
              <Flex direction={'row'} justify={'center'} gap={3}>
                <MantineButton color="indigo" disabled={filters.page! <= 1}
                  onClick={() => {
                    setFilters({
                      ...filters,
                      page: filters.page! - 1
                    })
                  }}
                >
                  {'<'}
                </MantineButton>
                <TextInput
                  min={1}
                  //width={50}
                  size="sm"
                  value={filters.page}
                  onKeyDown={(e) => {
                    if (e.key === 'Enter') {
                      appDispatch(setFilterParams(filters))
                    }
                  }}
                  onChange={(e) => {
                    setFilters({
                      ...filters,
                      page: Number(e.currentTarget.value)
                    })
                  }}
                />
                <MantineButton color="indigo"
                  disabled={(packageData?.length) ? (packageData.length >= filters.limit! ? false : true) : true}
                  onClick={() => {
                    setFilters({
                      ...filters,
                      page: filters.page! + 1
                    })
                  }}
                > {'>'}
                </MantineButton>
              </Flex>

            </>
          )}
        </div>
      </div>
    </div>
  )
}

PackagesScreen.getLayout = function getLayout(page) {
  return <MainLayout>{page}</MainLayout>
}

export default PackagesScreen
