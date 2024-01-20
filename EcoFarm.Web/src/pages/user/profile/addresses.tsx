import DefaultOverlay from "@/components/ui/overlay/DefaultOverlay"
import { UserInfo } from "@/components/ui/personalInfo/UserInfo"
import TextTitle from "@/components/ui/texts/TextTitle"
import { setAddressFilterParams } from "@/config/reducers/address"
import { createAddressSchema, type CreateAddressSchemaType } from "@/config/schema/address"
import useCreateAddress from "@/hooks/mutations/address/useCreateAddress"
import useSetMainAddress from "@/hooks/mutations/address/useSetMainAddress"
import useFetchAddresses from "@/hooks/queries/useFetchAddresses"
import { useAppDispatch } from "@/hooks/redux/useAppDispatch"
import MainLayout from "@/layouts/common/main"
import { type QueryAddresses } from "@/models/address.model"
import { type NextPageWithLayout } from "@/pages/_app"
import { EFX } from "@/utils/constants/constants"
import { Button, Card, Flex, Grid, Text, TextInput } from "@mantine/core"
import { useForm, zodResolver } from "@mantine/form"
import { useQueryClient } from "@tanstack/react-query"
import { EditIcon, SettingsIcon, TrashIcon } from "lucide-react"
import { useEffect, useState } from "react"

const UserAddressScreen: NextPageWithLayout = () => {
  const appDispatch = useAppDispatch();
  const { addressData, isLoading, refetch: refetchAddresses } = useFetchAddresses()
  const queryClient = useQueryClient();
  const { mutate: createAddressMutate, data: newAddressData, isPending } = useCreateAddress();
  const { mutate: setMainAddressMutate, isPending: isPendingSetMain } = useSetMainAddress();
  const newAddressForm = useForm<CreateAddressSchemaType>({
    initialValues: {
      addressDescription: '',
      addressPhone: '',
      receiverName: '',
      isMain: false,
    },
    validate: zodResolver(createAddressSchema()),
  })

  const [filterAddressParams, setFilterAddressParams] = useState<QueryAddresses>({
    page: EFX.DEFAULT_PAGE,
    keyword: '',
    limit: EFX.DEFAULT_LIMIT,
  })

  useEffect(() => {
    appDispatch(setAddressFilterParams(filterAddressParams))
  }, [appDispatch, filterAddressParams])

  if (isLoading) {
    return (
      <DefaultOverlay />
    )
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
            <TextTitle>Địa chỉ của bạn</TextTitle>
          </Flex>
          <Flex direction={'row'} justify={'center'} gap={3}>
            <Button color="indigo" disabled={filterAddressParams.page! <= 1}
              onClick={() => {
                setFilterAddressParams({
                  ...filterAddressParams,
                  page: filterAddressParams.page! - 1
                })
              }}
            >
              {'<'}
            </Button>
            <TextInput
              min={1}
              //width={50}
              size="sm"
              value={filterAddressParams.page}
              onKeyDown={(e) => {
                if (e.key === 'Enter') {
                  appDispatch(setAddressFilterParams(filterAddressParams))
                }
              }}
              onChange={(e) => {
                setFilterAddressParams({
                  ...filterAddressParams,
                  page: Number(e.currentTarget.value)
                })
              }}
            />
            <Button color="indigo"
              disabled={(addressData?.length) ? (addressData.length >= filterAddressParams.limit! ? false : true) : true}
              onClick={() => {
                setFilterAddressParams({
                  ...filterAddressParams,
                  page: filterAddressParams.page! + 1
                })
              }}
            > {'>'} </Button>
          </Flex>
          <TextInput
            placeholder="Tìm kiếm địa chỉ..."
            onChange={(e) => {
              setFilterAddressParams({
                ...filterAddressParams,
                keyword: e.currentTarget.value
              })
              void refetchAddresses();
            }}
          />
          <Flex direction={'column'} gap={5}>
            {
              addressData?.map((address) => (
                <Card shadow="md">
                  <Flex direction={'row'} justify={'space-between'}>
                    <Flex direction={'column'} gap={3}>
                      <Text
                        c={'teal'}
                        fw={'bold'}
                        size={'lg'}
                      >
                        {`${address.receiverName} ${address.isPrimary ? '(Mặc định)' : ''}`}
                      </Text>
                      <p>{address.addressDescription}</p>
                      <p>{address.addressPhone}</p>
                      <p>{address.isPrimary}</p>
                    </Flex>
                    <Flex direction={'row'} gap={3}>
                      <Button
                        color="teal"
                        loading={isPendingSetMain}
                        leftSection={<SettingsIcon />}
                        onClick={() => {
                          setMainAddressMutate(address.id)
                        }}
                      >
                        Đặt làm mặc định
                      </Button>
                      <Button
                        color={'orange'}
                        leftSection={<EditIcon />}
                      >
                        Sửa
                      </Button>
                      <Button color={'red'}
                        leftSection={<TrashIcon />}
                      >
                        Xóa
                      </Button>
                    </Flex>
                  </Flex>
                </Card>
              ))
            }
            <Card shadow="md">
              <Flex direction={'column'} gap={3}>
                <TextInput
                  color="teal"
                  label="Tên người nhận"
                  placeholder="Nhập tên người nhận"
                  required
                  {...newAddressForm.getInputProps('receiverName')}
                />
                <TextInput
                  color="teal"
                  label="Số điện thoại"
                  placeholder="Nhập số điện thoại"
                  required
                  {...newAddressForm.getInputProps('addressPhone')}
                />
                <TextInput
                  color="teal"
                  label="Địa chỉ"
                  placeholder="Nhập địa chỉ"
                  required
                  {...newAddressForm.getInputProps('addressDescription')}
                />
                <Button
                  color="teal"
                  onClick={() => {
                    const rs = newAddressForm.validate();
                    if (rs.hasErrors) {
                      return;
                    }
                    createAddressMutate(newAddressForm.values)
                    void refetchAddresses();
                    newAddressForm.reset();
                  }}
                >
                  Thêm địa chỉ
                </Button>
              </Flex>
            </Card>
          </Flex>
        </Flex>
      </Grid.Col>
    </Grid>
  )
}

UserAddressScreen.getLayout = function getLayout(page) {
  return (
    <MainLayout>
      {page}
    </MainLayout>
  )
}
export default UserAddressScreen