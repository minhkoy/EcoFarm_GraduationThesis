import { type CreatePackageSchemaType } from "@/config/schema";
import useCreatePackage from "@/hooks/mutations/packages/useCreatePackage";
import SellerLayout from "@/layouts/seller/sellerLayout";
import { type NextPageWithLayout } from "@/pages/_app";
import { FileInput, Input, NumberInput, Textarea } from "@mantine/core";
import { DatePickerInput } from "@mantine/dates";
import { Button, Checkbox, Table, TableBody, TableCell, TableColumn, TableHeader, TableRow } from "@nextui-org/react";
import { Calendar, Upload } from "lucide-react";
import { useRouter } from "next/router";
import { useState } from "react";

const CreatePackageScreen: NextPageWithLayout = () => {
  const [packageRequest, setPackageRequest] = useState<CreatePackageSchemaType>({
    name: '',
    code: '',
    description: '',
    price: 0,
    quantity: 0,
    serviceType: 0,
    estimatedEndTime: null,
    estimatedStartTime: null,
    isAutoCloseRegister: false,
    avatar: ''
  })
  const [avatar, setAvatar] = useState<string>('');

  const router = useRouter();
  const { mutate: createPackageMutate, isPending } = useCreatePackage()
  const onSubmitCreate = () => createPackageMutate({
    ...packageRequest,
    avatar: avatar ?? ''
  });

  return (
    <div className="flex flex-col justify-between gap-2">
      <p className="text-primary-600 uppercase font-bold text-2xl">Thêm mới gói farming</p>
      <Table hideHeader className="w-1/2">
        <TableHeader>
          <TableColumn>NAME</TableColumn>
          <TableColumn>VALUE</TableColumn>
        </TableHeader>
        <TableBody>
          <TableRow>
            <TableCell>Mã gói farming</TableCell>
            <TableCell>
              <Input
                placeholder="Mã gói farming"
                onChange={(e) => {
                  setPackageRequest({
                    ...packageRequest,
                    code: e.target.value
                  })
                }}
              />
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell>Tên gói farming</TableCell>
            <TableCell>
              <Input
                placeholder="Tên gói farming"
                onChange={(e) => {
                  setPackageRequest({
                    ...packageRequest,
                    name: e.target.value
                  })
                }}
              />
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell>Mô tả</TableCell>
            <TableCell>
              <Textarea
                placeholder="Mô tả"
                onChange={(e) => {
                  setPackageRequest({
                    ...packageRequest,
                    description: e.target.value
                  })
                }}
              />
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell>Giá</TableCell>
            <TableCell>
              <NumberInput
                min={0}
                thousandSeparator
                placeholder="Giá"
                onChange={(e) => {
                  setPackageRequest({
                    ...packageRequest,
                    price: Number(e)
                  })
                }}
              />
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell>Số lượng</TableCell>
            <TableCell>
              <Input
                type="number"
                min={0}
                placeholder="Số lượng"
                onChange={(e) => {
                  if (Number(e.target.value) < 0) {
                    return;
                  }
                  setPackageRequest({
                    ...packageRequest,
                    quantity: Number(e.target.value)
                  })
                }}
              />
            </TableCell>
          </TableRow>
          {/* <TableRow>
                        <TableCell>Loại dịch vụ</TableCell>
                        <TableCell>
                            <Input
                                placeholder="Loại dịch vụ"
                                onChange={(e) => {
                                    setPackageRequest({
                                        ...packageRequest,
                                        serviceType: Number(e.target.value)
                                    })
                                }}
                            />
                        </TableCell>
                    </TableRow> */}
          <TableRow>
            <TableCell>Thời gian bắt đầu dự kiến</TableCell>
            <TableCell>
              <DatePickerInput
                radius='md'
                rightSection={<Calendar />}
                valueFormat="DD-MM-YYYY"
                onChange={(date) => {
                  setPackageRequest({
                    ...packageRequest,
                    estimatedStartTime: date
                  })
                }}
                clearable
                defaultValue={null}
              />
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell>Thời gian kết thúc dự kiến</TableCell>
            <TableCell>
              <DatePickerInput
                radius={'md'}
                rightSection={<Calendar />}
                valueFormat="DD-MM-YYYY"
                onChange={(date) => {
                  setPackageRequest({
                    ...packageRequest,
                    estimatedEndTime: date
                  })
                }}
                clearable
                defaultValue={null}
              />
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell>
              Tự động đóng đăng ký khi đến ngày dự kiến bắt đầu
            </TableCell>
            <TableCell>
              <Checkbox defaultChecked={false}
                onValueChange={(value) => {
                  setPackageRequest({
                    ...packageRequest,
                    isAutoCloseRegister: value
                  })
                }}
              />
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell>Ảnh đại diện</TableCell>
            <TableCell>
              <FileInput
                accept="image/png, image/jpeg"
                placeholder="Chọn ảnh đại diện"
                clearable
                rightSection={<Upload />}
                onChange={(file) => {
                  if (!file) {
                    return;
                  }
                  const reader = new FileReader();
                  reader.onload = () => {
                    setAvatar(reader.result as string)
                  }
                  reader.readAsDataURL(file)
                  // e?.text()
                  //   .then((text) => setPackageRequest({
                  //     ...packageRequest,
                  //     avatar: text

                  //   }))
                  //   .catch((err) => console.log(err))
                }}
              />
            </TableCell>
          </TableRow>
          {/* <TableRow>
            <TableCell>Các ảnh khác</TableCell>
            <TableCell>
              <FileInput
                accept="image/png, image/jpeg"
                placeholder="Chọn các ảnh khác"
                multiple
                clearable
                rightSection={<Upload />}
              />
            </TableCell>
          </TableRow> */}
        </TableBody>
      </Table>
      <div className="flex flex-row justify-center gap-3">
        <Button color='primary' isLoading={isPending}
          onClick={onSubmitCreate}>Tạo mới</Button>
        <Button color='default'
          onClick={() => {
            void router.push('/seller/packages')
          }}>Trở về</Button>
      </div>
    </div>
    // <div className="flex flex-col justify-center gap-3 w-1/2 self-center">
    //     <p className="text-primary-600 uppercase">Thêm mới gói farming</p>
    //     <div className="grid grid-cols-2 gap-4">
    //         <div className="col-span-1 m-3">
    //             Mã gói farming
    //         </div>
    //         <div className="col-span-1 m-3">
    //             <Input
    //                 placeholder="Mã gói farming"
    //                 onChange={(e) => {
    //                     setPackageRequest({
    //                         ...packageRequest,
    //                         code: e.target.value
    //                     })
    //                 }}
    //             />
    //         </div>
    //     </div>
    // </div>
  )
}

CreatePackageScreen.getLayout = function getLayout(page) {
  return (
    <SellerLayout>
      {page}
    </SellerLayout>
  )
}
export default CreatePackageScreen;