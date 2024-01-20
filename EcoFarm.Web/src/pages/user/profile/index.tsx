import DefaultOverlay from "@/components/ui/overlay/DefaultOverlay";
import { UserInfo } from "@/components/ui/personalInfo/UserInfo";
import TextTitle from "@/components/ui/texts/TextTitle";
import { changePasswordSchema, type ChangePasswordSchemaType } from "@/config/schema/account";
import useAuth from "@/hooks/auth/useAuth";
import useChangePassword from "@/hooks/mutations/accounts/useChangePassword";
import useGetUserInfo from "@/hooks/queries/useGetUserInfo";
import MainLayout from "@/layouts/common/main";
import { EFX } from "@/utils/constants/constants";
import { dateFormat } from "@/utils/helpers/DateHelper";
import { Button, Card, Flex, Grid, Modal, PasswordInput, Table, Text, type TableData } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import { useDisclosure } from "@mantine/hooks";
import { KeyRoundIcon, SettingsIcon } from "lucide-react";
import { useParams } from "next/navigation";
import { useRouter } from "next/router";
import { useEffect, useState } from "react";
import { type NextPageWithLayout } from "../../_app";

const UserDetailScreen: NextPageWithLayout = () => {
  const router = useRouter();
  const params = useParams();
  //const userId = params.userId as string;

  const { accountInfo, isFetching } = useAuth();
  const { data, isLoading: isLoadingUserInfo, refetch: refetchUserInfo } = useGetUserInfo();
  const { rawData: changePasswordResult, isPending: isPendingChangePassword, mutate: changePasswordMutate, isError } = useChangePassword();
  const [isOpenChangingPasswordModal, { open, close }] = useDisclosure(false);
  const [confirmPassword, setConfirmPassword] = useState('');
  const changePasswordForm = useForm<ChangePasswordSchemaType>({
    initialValues: {
      oldPassword: '',
      newPassword: '',
      confirmPassword: ''
    },
    validate: zodResolver(changePasswordSchema()),
  })

  useEffect(() => {
    void refetchUserInfo();
  }, [refetchUserInfo])
  if (isFetching || isLoadingUserInfo) {
    return (
      <DefaultOverlay />
    )
  }
  // if (!accountInfo || !userData) {
  //   return (
  //     <>Có lỗi xảy ra. Vui lòng thử lại sau
  //     </>
  //   )
  // }
  if (isLoadingUserInfo) {
    return (
      <DefaultOverlay />
    )
  }

  const userData = data?.data.value;

  const changePasswordModal = (
    <Modal
      opened={isOpenChangingPasswordModal}
      onClose={close}
      title={<Text fw={'bold'} c={'teal'}>Đổi mật khẩu</Text>}
      size={'md'}
    >
      <Flex direction={'column'} gap={3}>
        <Text>Để đổi mật khẩu, vui lòng nhập mật khẩu cũ và mật khẩu mới</Text>
        <PasswordInput
          withAsterisk
          label={'Mật khẩu cũ'}
          type={'password'}
          placeholder={'Nhập mật khẩu cũ'}
          {...changePasswordForm.getInputProps('oldPassword')}
        />
        <PasswordInput
          withAsterisk
          label={'Mật khẩu mới'}
          type={'password'}
          placeholder={'Nhập mật khẩu mới'}
          {...changePasswordForm.getInputProps('newPassword')}
        />
        <PasswordInput
          withAsterisk
          label={'Nhập lại mật khẩu mới'}
          type={'password'}
          placeholder={'Nhập lại mật khẩu mới'}
          {...changePasswordForm.getInputProps('confirmPassword')}
        // errorProps={{ children: 'Mật khẩu nhập lại không khớp' }}
        // onChange={(e) => {
        //   setConfirmPassword(e.currentTarget.value)
        // }}
        // error={confirmPassword !== changePasswordForm.values.newPassword ? <Text c={'red'} size="sm">Mật khẩu nhập lại không khớp</Text> : null}
        />
        <Flex direction={'row'} justify={'end'} gap={3}>
          <Button
            color="gray"
            onClick={close}
          //className="hover:underline cursor-pointer"
          >
            Hủy
          </Button>
          <Button
            color="teal"
            loading={isPendingChangePassword}
            onClick={() => {
              const validationRs = changePasswordForm.validate();
              if (validationRs.hasErrors) {
                return;
              }
              changePasswordMutate(changePasswordForm.values)
                .then(() => {
                  close();
                })
                .catch((e: Error) => {
                  console.log(e);
                })
            }}
          //onClick={close}
          //className="hover:underline cursor-pointer"
          >
            Lưu thay đổi
          </Button>
        </Flex>
      </Flex>

    </Modal>
  )

  const tableData: TableData = {
    body: [
      [<Text fw={'bold'} c={'teal'}>Họ và tên</Text>, <Text>{userData?.fullName}</Text>],
      [<Text fw={'bold'} c={'teal'}>Email</Text>, <Text>{userData?.email}</Text>],
      [<Text fw={'bold'} c={'teal'}>Số điện thoại</Text>, <Text>{userData?.phoneNumber}</Text>],
      [<Text fw={'bold'} c={'teal'}>Ngày tạo tài khoản</Text>,
      <Text>{userData?.dateOfBirth ? dateFormat(new Date(userData?.dateOfBirth), EFX.DATETIME_FORMAT, 'vi') : 'Không có thông tin'}</Text>],
    ]
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
            <TextTitle>Thông tin người dùng</TextTitle>
          </Flex>
          <Card
            shadow="md"
            m={5}
            padding={5}
          >
            <Table withTableBorder={false} withRowBorders={false}
              data={tableData}
            >
            </Table>
          </Card>
          <Card
            shadow="md"
            m={5}
            padding={5}
          >
            <Text fw={'bold'} c={'teal'}
              component="a"
              onClick={open}
              className="hover:underline cursor-pointer"
            >
              <KeyRoundIcon className="inline mr-3" /> Đổi mật khẩu
            </Text>
            {changePasswordModal}
            <Text fw={'bold'} c={'indigo'}
              className="hover:underline cursor-pointer"
            >
              <SettingsIcon className="inline mr-3" /> Cập nhật thông tin tài khoản
            </Text>
          </Card>
        </Flex>
      </Grid.Col>
    </Grid>
  )
}

UserDetailScreen.getLayout = (page) => {
  return (
    <MainLayout>
      {page}
    </MainLayout>
  )
}

export default UserDetailScreen