import DefaultOverlay from "@/components/ui/overlay/DefaultOverlay";
import TextTitle from "@/components/ui/texts/TextTitle";
import { setPackageId } from "@/config/reducers/package";
import { updatePackageSchema, type UpdatePackageSchemaType } from "@/config/schema";
import useUpdatePackage from "@/hooks/mutations/packages/useUpdatePackage";
import useFetchSinglePackage from "@/hooks/queries/useFetchSinglePackage";
import { useAppDispatch } from "@/hooks/redux/useAppDispatch";
import SellerLayout from "@/layouts/seller/sellerLayout";
import { type NextPageWithLayout } from "@/pages/_app";
import { Button, Checkbox, Flex, Grid, Input, NumberInput, Table, Textarea } from "@mantine/core";
import { DateTimePicker } from "@mantine/dates";
import { useForm, zodResolver } from "@mantine/form";
import { CalendarIcon, SaveIcon, UndoIcon } from "lucide-react";
//import { useParams } from "next/navigation";
import { useRouter } from "next/router";
import { useEffect } from "react";

const UpdatePackageScreen: NextPageWithLayout = () => {
  //const params = useParams();
  const router = useRouter();
  const packageId = router.query.packageId as string;

  const appDispatch = useAppDispatch();
  const { packageData, isLoading } = useFetchSinglePackage()
  const { mutate: updatePackageMutate, isPending: pendingUpdatePackage } = useUpdatePackage();
  const form = useForm<UpdatePackageSchemaType>({
    initialValues: {
      id: packageId,
      code: packageData?.code ?? '',
      name: packageData?.name ?? '',
      description: packageData?.description ?? '',
      price: packageData?.price ?? 0,
      quantityRemain: packageData?.quantityRemain ?? 0,
      estimatedStartTime: packageData?.estimatedStartTime ? new Date(packageData.estimatedStartTime) : null,
      estimatedEndTime: packageData?.estimatedEndTime ? new Date(packageData.estimatedEndTime) : null,
      avatar: '',
      isAutoCloseRegister: false,

    },
    validate: zodResolver(updatePackageSchema())
  })
  useEffect(() => {
    if (packageId) {
      appDispatch(setPackageId(packageId))
    }
  }, [appDispatch, packageId])

  if (isLoading) {
    return (
      <DefaultOverlay />
    )
  }

  return (
    <Flex direction={'column'} gap={3} m={5}>
      <Flex direction={'row'} justify={'center'}>
        <TextTitle>Cập nhật thông tin gói farming </TextTitle>
      </Flex>
      <Grid columns={4}>
        <Grid.Col span={1}>

        </Grid.Col>
        <Grid.Col span={2}>
          <Table withTableBorder={false} withRowBorders={false}>
            <Table.Tr>
              <Table.Td fw={'bold'}>
                Mã gói farming
              </Table.Td>
              <Table.Td>
                <Input
                  defaultValue={packageData?.code}
                  {...form.getInputProps('code')}
                />
              </Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>
                Tên gói farming
              </Table.Td>
              <Table.Td>
                <Input
                  defaultValue={packageData?.name}
                  {...form.getInputProps('name')}
                />
              </Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>
                Mô tả
              </Table.Td>
              <Table.Td>
                <Textarea
                  rows={5}
                  //defaultValue={packageData?.description}
                  {...form.getInputProps('description')}
                />
              </Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>
                Giá (VND)
              </Table.Td>
              <Table.Td>
                <NumberInput
                  thousandSeparator
                  defaultValue={packageData?.price}
                  {...form.getInputProps('price')}
                />
              </Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>
                Số lượng còn lại
              </Table.Td>
              <Table.Td>
                <NumberInput

                  defaultValue={packageData?.quantityRemain}
                  {...form.getInputProps('quantity')}
                />
              </Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>
                Thời gian dự kiến bắt đầu
              </Table.Td>
              <Table.Td>
                <DateTimePicker
                  radius='md'
                  rightSection={<CalendarIcon />}
                  valueFormat="DD-MM-YYYY HH:mm"
                  clearable
                  defaultValue={form.values.estimatedStartTime ? new Date(form.values.estimatedStartTime) : undefined}
                  //value={packageData?.estimatedStartTime ? new Date(packageData.estimatedStartTime) : undefined}
                  onChange={(value) => {
                    form.setValues({
                      ...form.values,
                      estimatedStartTime: value
                    })
                  }}
                //{...form.getInputProps('estimatedStartTime')}
                />
              </Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>
                Thời gian dự kiến kết thúc
              </Table.Td>
              <Table.Td>
                <DateTimePicker
                  radius='md'
                  rightSection={<CalendarIcon />}
                  valueFormat="DD-MM-YYYY HH:mm"
                  clearable
                  defaultValue={packageData?.estimatedEndTime ? new Date(packageData.estimatedEndTime) : undefined}
                  onChange={(value) => {
                    form.setValues({
                      ...form.values,
                      estimatedEndTime: value
                    })
                  }}
                //{...form.getInputProps('estimatedStartTime')}
                />
              </Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>
                Tự động đóng đăng ký khi đến ngày dự kiến bắt đầu
              </Table.Td>
              <Table.Td>
                <Checkbox
                  {...form.getInputProps('isAutoCloseRegister')}
                />
              </Table.Td>
            </Table.Tr>
          </Table>
        </Grid.Col>
      </Grid>
      <Flex direction={'row'} justify={'center'} gap={3}>
        <Button color="teal"
          loading={pendingUpdatePackage}
          leftSection={<SaveIcon />}
          onClick={() => {
            console.log(form.values)
            const validRs = form.validate();
            if (validRs.hasErrors) {
              console.log(validRs.errors)
              return;
            }
            updatePackageMutate({
              ...form.values,
              id: packageId
            })
          }}
        >
          Cập nhật
        </Button>
        <Button color="gray"
          leftSection={<UndoIcon />}
          onClick={() => {
            router.back()
          }}
        >
          Trở về
        </Button>
      </Flex>
    </Flex>
  )
}

UpdatePackageScreen.getLayout = function getLayout(page) {
  return (
    <SellerLayout>
      {page}
    </SellerLayout>
  )
}
export default UpdatePackageScreen;