import DefaultOverlay from "@/components/ui/overlay/DefaultOverlay";
import TextTitle from "@/components/ui/texts/TextTitle";
import { setActivityFilterParams } from "@/config/reducers/activity";
import { setPackageId } from "@/config/reducers/package";
import { createNewActivitySchema, type CreateActivitySchemaType } from "@/config/schema";
import useAuth from "@/hooks/auth/useAuth";
import useCreateActivity from "@/hooks/mutations/activities/useCreateActivity";
import useCloseRegisterPackage from "@/hooks/mutations/packages/useCloseRegisterPackage";
import useEndPackage from "@/hooks/mutations/packages/useEndPackage";
import useStartPackage from "@/hooks/mutations/packages/useStartPackage";
import useFetchActivities from "@/hooks/queries/useFetchActivities";
import useFetchSinglePackage from "@/hooks/queries/useFetchSinglePackage";
import { useAppDispatch } from "@/hooks/redux/useAppDispatch";
import SellerLayout from "@/layouts/seller/sellerLayout";
import { type ActivityModel } from "@/models/activity.model";
import { type NextPageWithLayout } from "@/pages/_app";
import { PACKAGE_STATUS } from "@/utils/constants/enums";
import { splitDigits } from "@/utils/helpers/CommonHelper";
import { dateFormat } from "@/utils/helpers/DateHelper";
import { Button, Card, FileInput, Flex, Grid, Group, Image, Modal, Rating, Table, Text, TextInput, Textarea, Timeline, type TableData } from "@mantine/core";
import { useForm, zodResolver } from '@mantine/form';
import { useDisclosure } from "@mantine/hooks";
import { ActivitySquareIcon, Check, Edit, Play, TrashIcon, Upload } from "lucide-react";
import { type GetServerSideProps } from "next";
import { useRouter } from "next/router";
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

export const getServerSideProps: GetServerSideProps = async ({ locale, query }) => {
  const packageId = query.packageId as string;
  return {
    // props: {
    //   ...(await serverSideTranslations(
    //     locale ?? 'vi',
    //     ['common', 'farm-package'],
    //     config,
    //   )),
    // },
    props: {
      packageId: packageId
    }

  }
}

const SellerPackageDetailScreen: NextPageWithLayout = (props) => {
  const packageId = (props?.packageId as string) || '';
  //const { query } = useRouter();
  const router = useRouter();
  const appDispatch = useAppDispatch()
  const { t } = useTranslation();
  const [imageList, setImageList] = useState<Array<File>>([]);
  const [activityDetailWatching, setActivityDetailWatching] = useState<ActivityModel | null>(null);
  //const packageId = router.query.packageId as string;

  useEffect(() => {
    if (!router.isReady) {
      return;
    }
    if (packageId) {
      appDispatch(setActivityFilterParams({
        packageId: packageId,
      }))
      appDispatch(setPackageId(packageId))
    }
  }, [appDispatch, packageId, router.isReady])//, packageId, newActivityForm])
  //debugger;
  const newActivityForm = useForm<CreateActivitySchemaType>({
    initialValues: {
      code: '',
      content: '',
      medias: [],
      title: '',
      packageId: packageId ?? '',
      shortDescription: '',
      images: [],
      mainImage: '',
    },
    validate: zodResolver(createNewActivitySchema()),
  })
  const { accountInfo, isFetching } = useAuth()
  const { packageData, isLoading } = useFetchSinglePackage()
  const { activityData, isLoading: isLoadingActivity } = useFetchActivities()
  //console.log(packageData)
  //debugger;
  const { mutate: createActivityMutate, data: newActivity, isPending: isPendingCreateActivity } = useCreateActivity()
  const { mutate: closeRegisterMutate, isPending: isPendingCloseRegister } = useCloseRegisterPackage()
  const { mutate: startPackageMutate, isPending: isPendingStartPackage } = useStartPackage()
  const { mutate: endPackageMutate, isPending: isPendingEndPackage } = useEndPackage()

  // const packageData = useMemo(() => testPackageData, [testPackageData]);
  // const activityData = useMemo(() => testActivityData, [testActivityData]);

  const [isCreatingNewActivity, { open, close }] = useDisclosure(false)
  const [isViewingActivityModal, { open: openViewingActivityModal, close: closeViewingActivityModal }] = useDisclosure(false)


  if (isFetching || isLoading) { //|| isPendingCloseRegister || isPendingStartPackage || isPendingEndPackage) {
    return (
      <DefaultOverlay
      />
    )
  }

  if (!(packageData?.id)) {
    return (
      <div className="flex justify-center items-center">
        <p className="text-2xl font-bold">Không tìm thấy dữ liệu!</p>
      </div>
    )
  }

  // if (accountInfo?.accountType !== ACCOUNT_TYPE.SELLER || accountInfo?.accountId !== packageData?.enterprise?.accountId) {
  //   return (
  //     <ForbiddenScreen />
  //   )
  // }

  if (!packageData) {
    return (
      <div className="flex justify-center items-center">
        <p className="text-2xl font-bold">Không tìm thấy dữ liệu!</p>
      </div>
    )
  }

  const tableData: TableData = {
    body: [
      ['Mã gói farming', packageData.code],
      ['Giá gói farming', `${splitDigits(packageData.price)} VND`],
      ['Số suất đăng ký', packageData.quantityStart],
      ['Đã đăng ký', packageData.quantityRegistered],
      ['Số suất còn lại', packageData.quantityStart - packageData.quantityRegistered],
      ['Thời gian đóng đăng ký', packageData.closeRegisterTime ? dateFormat(new Date(packageData.closeRegisterTime), 'dd/MM/yyyy HH:mm:ss', 'vi') : 'Không có thông tin'],
      ['Thời gian bắt đầu', packageData.startTime ? dateFormat(new Date(packageData.startTime), 'dd/MM/yyyy HH:mm:ss', 'vi') : 'Không có thông tin'],
      ['Thời gian kết thúc', packageData.endTime ? dateFormat(new Date(packageData.endTime), 'dd/MM/yyyy HH:mm:ss', 'vi') : 'Không có thông tin'],
      // ['Trạng thái duyệt', packageData.servicePackageApprovalStatusName],
      // ['Trạng thái gói', packageData.packageStatusName],
      //['Ngày tạo', dateFormat(new Date(packageData.createAt), 'dd/MM/yyyy HH:mm:ss', 'vi')],
      //['Ngày cập nhật', dateFormat(new Date(packageData.), 'dd/MM/yyyy HH:mm:ss', 'vi')],
    ]
  }

  const onCloseRegister = () => closeRegisterMutate(packageId)
  const onStartPackage = () => startPackageMutate(packageId)
  const onEndPackage = () => endPackageMutate(packageId)
  const ButtonRegister = () => {
    if (!packageData.closeRegisterTime) {
      return (
        <Button color="indigo" onClick={onCloseRegister}>Đóng đăng ký</Button>
      )
    }
    return (
      <Button color="indigo" disabled>Đã đóng đăng ký</Button>
    )
  }
  const ButtonStart = () => {
    switch (packageData.packageStatus) {
      case PACKAGE_STATUS.NotStarted:
        return (
          <Button color="teal" onClick={onStartPackage}>Bắt đầu</Button>
        )
      case PACKAGE_STATUS.Started:
        return (
          <Button color="teal" onClick={onEndPackage}>Kết thúc</Button>
        )
      case PACKAGE_STATUS.Ended:
        return (
          <Button color="teal" disabled>Đã kết thúc</Button>
        )
    }
  }

  const ShowActivityDetail = (activity: ActivityModel | null) => {
    if (!activity) {
      return <></>;
    }
    return (
      <Modal opened={isViewingActivityModal}
        onClose={closeViewingActivityModal}
        title={<Text c={'teal'} fw={'bold'}>Chi tiết hoạt động</Text>}
      >
        <Flex direction={'column'} gap={3}>
          {/* <Flex direction={'row'} gap={5}>
            <Text ></Text>
          </Flex>
          <Text title="Hoạt động">{activity.title}</Text>
          <Text>{activity.shortDescription}</Text>
          <Text>{activity.description}</Text> */}
          <Table >
            <Table.Tr>
              <Table.Td fw={'bold'}>Tiêu đề</Table.Td>
              <Table.Td>{activity.title}</Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>Tóm tắt</Table.Td>
              <Table.Td>{activity.shortDescription}</Table.Td>
            </Table.Tr>
            <Table.Tr>
              <Table.Td fw={'bold'}>Nội dung</Table.Td>
              <Table.Td>{activity.description}</Table.Td>
            </Table.Tr>
          </Table>
          <Grid columns={2} justify="center">
            {
              activity.medias?.map((media) => (
                <Grid.Col span={1}>
                  <Image
                    src={media.imageUrl ?? '/assets/brands/logo.png'}
                    className='rounded-md'
                    width={'300'}
                    height={'200'}
                    alt='EcoFarm'
                  />
                </Grid.Col>
              ))
            }
          </Grid>
          <Flex direction={'row'} gap={5} justify={'center'}>
            <Button color="orange">
              <Edit /> Cập nhật
            </Button>
            <Button color="red">
              <TrashIcon size={20} /> Xóa
            </Button>
          </Flex>
        </Flex>
      </Modal>
    )
  }
  const CreateActivityModal = () => {
    return (
      <Modal opened={isCreatingNewActivity}
        onClose={close}
        draggable={true}
        size={'auto'}
        //withCloseButton={false}
        title={
          <Text fw={'bold'} c={'teal'}>Thêm hoạt động mới cho gói farming </Text>
        }
        centered
        // overlayProps={{
        //   backgroundOpacity: 0.55,
        //   blur: 3,
        // }}
        padding={5}
      >
        <Flex direction={'column'} justify={'left'} gap={3} w={'400'}>
          <Flex direction={'row'} justify={'space-between'} gap={2}>
            <TextInput
              withAsterisk
              label="Mã hoạt động"
              placeholder="Nhập mã hoạt động..."
              {...newActivityForm.getInputProps('code')}
            />
            <TextInput
              withAsterisk
              maxLength={40}
              label="Tiêu đề hoạt động"
              placeholder="Nhập tiêu đề..."
              {...newActivityForm.getInputProps('title')}
            />
          </Flex>
          <TextInput
            label="Tóm tắt hoạt động"
            placeholder="Nhập nội dung..."
            {...newActivityForm.getInputProps('shortDescription')}
          />
          <Textarea
            rows={3}
            withAsterisk
            label="Nội dung hoạt động"
            placeholder="Nhập nội dung..."
            {...newActivityForm.getInputProps('content')}
          />
          <FileInput
            accept="image/png, image/jpeg"
            placeholder="Chọn ảnh"
            label="Ảnh hoạt động"
            clearable
            //multiple
            leftSection={<Upload />}
            onChange={(file) => {
              if (!file) {
                newActivityForm.setFieldValue('mainImage', '')
                return;
              }
              const reader = new FileReader();
              reader.onload = () => {
                newActivityForm.setFieldValue('mainImage', reader.result as string)
              }
              reader.readAsDataURL(file)
              //newActivityForm.setFieldValue('medias', file);
            }}

          />
          <Group justify={'center'}>
            <Button color="teal" onClick={() => {
              console.log(newActivityForm.values)
              const validRs = newActivityForm.validate();
              if (!validRs.hasErrors) {
                createActivityMutate(newActivityForm.values)
                console.log(newActivity);
                if (newActivity) {
                  activityData?.unshift(newActivity);
                }
                newActivityForm.reset()
                close()
              }
            }}>Thêm hoạt động</Button>
          </Group>
          {/* <p>{JSON.stringify(imageList)}</p>
          <p>{JSON.stringify(newActivityForm.values)}</p> */}

        </Flex>
      </Modal>
    )
  }

  return (
    <>
      <Flex direction={'column'} gap={3} mb={5}>
        <Card shadow="sm" padding="md" radius="md" withBorder
          m={5}>
          <Grid columns={3} m={3} p={4} align="stretch">
            <Grid.Col span={1}>
              <Flex direction={'column'} gap={5} justify={'center'} >
                <Image
                  height={1000}
                  src={packageData.avatarUrl ?? '/assets/brands/logo.png'}
                  className=''
                  alt='EcoFarm'
                  width={500}
                />
                {
                  ButtonRegister()
                }
                {
                  ButtonStart()
                }
                <Card shadow="sm" padding={'md'} radius={'md'} withBorder>
                  <Table withRowBorders={false} >
                    <Table.Tr>
                      <Table.Td>Trạng thái duyệt</Table.Td>
                      <Table.Td fw={'bold'}>{packageData.servicePackageApprovalStatusName}</Table.Td>
                    </Table.Tr>
                    <Table.Tr>
                      <Table.Td>Trạng thái gói</Table.Td>
                      <Table.Td fw={'bold'}>{packageData.packageStatusName}</Table.Td>
                    </Table.Tr>
                    <Table.Tr>
                      <Table.Td>Trạng thái đăng ký</Table.Td>
                      <Table.Td fw={'bold'}>{packageData.packageRegisterStatusName}</Table.Td>
                    </Table.Tr>
                  </Table>
                </Card>
              </Flex>
            </Grid.Col>
            <Grid.Col span={2} h={'full'}>
              <Flex direction={'row'} justify={'space-between'} m={2}>
                <Text size="xl" c='teal' fw='bold' className="uppercase">{packageData.name}</Text>
                <Button color="orange"
                  disabled={packageData.packageStatus !== PACKAGE_STATUS.NotStarted}
                  onClick={() => {
                    void router.push(`/seller/packages/${packageId}/update`);
                  }}
                >
                  Cập nhật thông tin
                </Button>
              </Flex>
              <Card shadow="sm" padding="md" radius="md" withBorder
                mb={5}>
                <Table withRowBorders={false} data={tableData}>
                </Table>
                {/* <Table withRowBorders={false} >
                  <Table.Tr>
                    <Table.Td>Trạng thái duyệt</Table.Td>
                    <Table.Td fw={'bold'}>{packageData.servicePackageApprovalStatusName}</Table.Td>
                  </Table.Tr>
                  <Table.Tr>
                    <Table.Td>Trạng thái gói</Table.Td>
                    <Table.Td fw={'bold'}>{packageData.packageStatusName}</Table.Td>
                  </Table.Tr>
                </Table> */}
              </Card>
            </Grid.Col>
          </Grid>
        </Card>
        <Card shadow="sm" padding="md" radius="md" withBorder
          m={5}>
          <Flex direction={'column'} gap={3}>
            <Flex justify={'space-between'}>
              <Text size="xl" c='teal' fw='bold' className="uppercase">Hoạt động</Text>
              <Button color='teal'
                disabled={packageData.packageStatus !== PACKAGE_STATUS.Started}
                onClick={open}
              >
                Thêm hoạt động
              </Button>
              {
                CreateActivityModal()
              }
            </Flex>
            <Timeline active={1} bulletSize={24} lineWidth={2}>
              {
                packageData.endTime && (
                  <Timeline.Item bullet={<Check size={12} />} title={'Kết thúc'} p={3}>
                    <Text c="dimmed" size="sm">Kết thúc gói farming</Text>
                    <Text size="xs" mt={4}>{dateFormat(new Date(packageData.endTime), 'dd/MM/yyyy HH:mm:ss', 'vi')}</Text>
                  </Timeline.Item>
                )
              }
              {
                activityData?.map((item: ActivityModel, index) => (
                  <>
                    <Timeline.Item bullet={<ActivitySquareIcon size={12} />} title={item.title}
                      p={3}
                      className="hover:bg-orange-300"
                      onClick={() => {
                        setActivityDetailWatching(item)
                        openViewingActivityModal()
                      }}
                    >
                      <Text c="dimmed" size="sm">{item.shortDescription}</Text>
                      <Text size="xs" mt={4}>{dateFormat(new Date(item.createdTime), 'dd/MM/yyyy HH:mm:ss', 'vi')}</Text>
                    </Timeline.Item>
                  </>
                ))
              }
              {ShowActivityDetail(activityDetailWatching)}
              {
                packageData.startTime && (
                  <Timeline.Item bullet={<Play size={12} />} title='Bắt đầu' p={3}>
                    <Text c="dimmed" size="sm">Bắt đầu gói farming</Text>
                    <Text size="xs" mt={4}>{dateFormat(new Date(packageData.startTime), 'dd/MM/yyyy HH:mm:ss', 'vi')}</Text>
                  </Timeline.Item>
                )
              }
            </Timeline>
          </Flex>
        </Card>
        <Card shadow="sm" padding={'md'} radius={'md'} withBorder>
          <Flex direction={'row'} justify={'start'} gap={2}>
            <TextTitle>Đánh giá</TextTitle>
            <Text fw={'bold'}>
              <>
                <Rating fractions={10} defaultValue={packageData.averageRating} readOnly /> ({packageData.averageRating} sao/ {packageData.numbersOfRating} lượt đánh giá)
              </>
            </Text>
          </Flex>
          {
            packageData.reviews?.map((review) => (
              <Card shadow="sm" padding={'md'} radius={'md'} withBorder
                className="hover:bg-orange-300">
                <Flex direction={'row'} justify={'start'} gap={2}>
                  <Text fw={'bold'}>{review.userFullname}</Text>
                  <Text>
                    <Rating fractions={10} defaultValue={review.rating} readOnly /> ({review.rating}/5)
                  </Text>
                </Flex>
                <Text>{review.content}</Text>
              </Card>
            ))
          }
        </Card>
      </Flex >
    </>
  )
}

SellerPackageDetailScreen.getLayout = function getLayout(page) {
  return (
    <SellerLayout>
      {page}
    </SellerLayout>
  )
}

export default SellerPackageDetailScreen;