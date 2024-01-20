import TextTitle from "@/components/ui/texts/TextTitle";
import { setFilterParams } from "@/config/reducers/packages";
import { createProductSchema, type createProductSchemaType } from "@/config/schema/product";
import useAuth from "@/hooks/auth/useAuth";
import useCreateProduct from "@/hooks/mutations/products/useCreateProduct";
import useFetchPackage from "@/hooks/queries/useFetchPackage";
import { useAppDispatch } from "@/hooks/redux/useAppDispatch";
import SellerLayout from "@/layouts/seller/sellerLayout";
import { type NextPageWithLayout } from "@/pages/_app";
import { Button, FileInput, Flex, Input, NumberInput, Select, Table, Textarea } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import { Upload } from "lucide-react";
import { useRouter } from "next/router";
import { useEffect } from "react";

const CreateProductScreen: NextPageWithLayout = () => {
  const router = useRouter();
  //const [avatar, setAvatar] = useState('')
  const { accountInfo } = useAuth();
  const addProductForm = useForm<createProductSchemaType>({
    initialValues: {
      code: '',
      name: '',
      description: '',
      price: 0,
      quantity: 0,
      packageId: '',
      priceForRegistered: 0,
      weight: 0,
      avatar: '',
    },
    validate: zodResolver(createProductSchema()),
  })

  const { packageData } = useFetchPackage();
  const { mutate, isPending } = useCreateProduct();
  const appDispatch = useAppDispatch();
  useEffect(() => {
    if (accountInfo?.accountEntityId) {
      appDispatch(setFilterParams({
        enterpriseId: accountInfo?.accountEntityId
      }))
    }
  })
  return (
    <>
      <Flex direction={'column'} gap={5}>
        <Flex direction={'row'} justify={'center'}>
          <TextTitle>Thêm mới sản phẩm</TextTitle>
        </Flex>
        <Table withTableBorder={false} withRowBorders={false}>
          <Table.Tr>
            <Table.Td fw={'bold'}>Tên sản phẩm</Table.Td>
            <Table.Td>
              <Input
                placeholder={'Tên sản phẩm'}
                {...addProductForm.getInputProps('name')}
              >
              </Input>
            </Table.Td>
          </Table.Tr>
          <Table.Tr>
            <Table.Td fw={'bold'}>Mã sản phẩm</Table.Td>
            <Table.Td>
              <Input
                placeholder={'Mã sản phẩm'}
                {...addProductForm.getInputProps('code')}
              >
              </Input>
            </Table.Td>
          </Table.Tr>
          <Table.Tr>
            <Table.Td fw={'bold'}>Mô tả</Table.Td>
            <Table.Td>
              <Textarea
                placeholder={'Mô tả'}
                minRows={5}
                autosize
                {...addProductForm.getInputProps('description')}
              >
              </Textarea>
            </Table.Td>
          </Table.Tr>
          <Table.Tr>
            <Table.Td fw={'bold'}>Giá</Table.Td>
            <Table.Td>
              <NumberInput
                placeholder={'Giá'}
                {...addProductForm.getInputProps('price')}
              >
              </NumberInput>
            </Table.Td>
          </Table.Tr>
          <Table.Tr>
            <Table.Td fw={'bold'}>Số lượng để bán</Table.Td>
            <Table.Td>
              <NumberInput
                placeholder={'Số lượng'}
                {...addProductForm.getInputProps('quantity')}
              >
              </NumberInput>
            </Table.Td>
          </Table.Tr>
          <Table.Tr>
            <Table.Td fw={'bold'}>Gói</Table.Td>
            <Table.Td>
              <Select
                placeholder={'Gói farming liên quan'}

                {...addProductForm.getInputProps('packageId')}
                data={packageData?.map((item) => ({ value: item.id, label: item.name }))}
              />
            </Table.Td>
          </Table.Tr>
          <Table.Tr>
            <Table.Td fw={'bold'}>Giá cho người đăng ký</Table.Td>
            <Table.Td>
              <NumberInput
                disabled={!addProductForm.values.packageId}
                placeholder={'Giá cho người đăng ký'}
                {...addProductForm.getInputProps('priceForRegistered')}
              >
              </NumberInput>
            </Table.Td>
          </Table.Tr>
          <Table.Tr>
            <Table.Td fw={'bold'}>Khối lượng sản phẩm (kg)</Table.Td>
            <Table.Td>
              <NumberInput
                placeholder={'Khối lượng'}
                {...addProductForm.getInputProps('weight')}
              >
              </NumberInput>
            </Table.Td>
          </Table.Tr>
          <Table.Tr>
            <Table.Td fw={'bold'}>Ảnh sản phẩm</Table.Td>
            <Table.Td>
              <FileInput
                {...addProductForm.getInputProps('avatar')}
                accept="image/png, image/jpeg"
                placeholder="Chọn ảnh..."
                clearable
                rightSection={<Upload />}
                onChange={(file) => {
                  if (!file) {
                    return;
                  }
                  const reader = new FileReader();
                  reader.onload = () => {
                    addProductForm.setFieldValue('avatar', reader.result as string);
                  }
                  reader.readAsDataURL(file)
                  // e?.text()
                  //   .then((text) => setPackageRequest({
                  //     ...packageRequest,
                  //     avatar: text

                  //   }))
                  //   .catch((err) => console.log(err))
                }}
              >
              </FileInput>
            </Table.Td>
          </Table.Tr>
        </Table>
        <Flex direction={'row'} justify={'center'} gap={3}>
          <Button
            color="teal"
            variant="light"
            loading={isPending}
            onClick={() => {
              const validateResult = addProductForm.validate();
              if (validateResult.hasErrors) {
                return;
              }
              mutate(addProductForm.values);
            }}
          >
            Tạo sản phẩm
          </Button>
          <Button
            color="gray"
            variant="light"
            onClick={() => {
              router.back();
            }}
          >
            Trở về
          </Button>
        </Flex>
      </Flex>
    </>
  )
}

CreateProductScreen.getLayout = function getLayout(page) {
  return (
    <SellerLayout>
      {page}
    </SellerLayout>
  )
}

export default CreateProductScreen;