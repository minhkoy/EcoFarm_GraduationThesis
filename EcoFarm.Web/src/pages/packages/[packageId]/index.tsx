import DefaultOverlay from '@/components/ui/overlay/DefaultOverlay'
import { setActivityFilterParams } from '@/config/reducers/activity'
import { setPackageId } from '@/config/reducers/package'
import { type CreatePackageReviewSchemaType } from '@/config/schema'
import useAuth from '@/hooks/auth/useAuth'
import useRegisterPackage from '@/hooks/mutations/packages/useRegisterPackage'
import useCreatePackageReview from '@/hooks/mutations/useCreatePackageReview'
import useFetchActivities from '@/hooks/queries/useFetchActivities'
import useFetchPackageReviews from '@/hooks/queries/useFetchPackageReviews'
import useFetchSinglePackage from '@/hooks/queries/useFetchSinglePackage'
import { useAppDispatch } from '@/hooks/redux/useAppDispatch'
import MainLayout from '@/layouts/common/main'
import { type ActivityModel } from '@/models/activity.model'
import { type NextPageWithLayout } from '@/pages/_app'
import { getQueryUrlValue } from '@/utils/helpers/CommonHelper'
import { dateFormat } from '@/utils/helpers/DateHelper'
import { Flex, Grid, Modal, NumberFormatter, Rating, Switch, Table, Text, Timeline } from '@mantine/core'
import { useDisclosure } from '@mantine/hooks'
import { Button, Card, Image, Table as NextUITable, TableBody, TableCell, TableColumn, TableHeader, TableRow, Textarea } from '@nextui-org/react'
import { ActivitySquareIcon, Check, Play, Star } from 'lucide-react'
import Link from 'next/link'
import { useRouter } from 'next/router'
import { useEffect, useMemo, useRef, useState } from 'react'


const PackageDetailScreen: NextPageWithLayout = () => {
  const router = useRouter()
  const newReviewRef = useRef<HTMLParagraphElement>(null)
  const packageId = useMemo(() => getQueryUrlValue(router.query, 0), [router.query])
  const [isAddingNewReview, setIsAddingNewReview] = useState(false);
  const [isRatingPoints, setIsRatingPoints] = useState(false);
  const [activityDetailWatching, setActivityDetailWatching] = useState<ActivityModel | null>(null)
  const [newReview, setNewReview] = useState<CreatePackageReviewSchemaType>({
    rating: 0,
    content: '',
    packageId: packageId!,
  });
  const [isViewingActivityModal, { open: openViewingActivityModal, close: closeViewingActivityModal }] = useDisclosure(false)
  //const packageId = useMemo(() => getQueryUrlValue(query, 0), [query])
  const appDispatch = useAppDispatch()
  useEffect(() => {
    if (!router.isReady) {
      return;
    }
    if (packageId) {
      appDispatch(setPackageId(packageId))
      appDispatch(setActivityFilterParams({
        packageId: packageId
      }))
    }
  }, [appDispatch, packageId, router.isReady])

  const { packageData, isLoading } = useFetchSinglePackage()
  const { activityData, isLoading: isLoadingActivity } = useFetchActivities()
  const { mutate: registerMutate, isPending: isPendingRegister } = useRegisterPackage()
  const { accountInfo } = useAuth()

  const onRegister = () => registerMutate(packageId);

  // appDispatch(setFilterParams({
  //   packageId: packageId!
  // }))
  const { packageReviewsData } = useFetchPackageReviews(packageId)

  const loadRegisterButton = () => {
    if (packageData?.isRegisteredByCurrentUser) {
      return (
        <Button color='secondary' className='w-full mt-2' isDisabled>Đã đăng ký</Button>
      )
    }
    if (packageData?.closeRegisterTime) {
      return (
        <Button color='default' className='w-full mt-2' isDisabled>Đã đóng đăng ký</Button>
      )
    }
    return (
      <Button color='primary' className='w-full mt-2'
        isLoading={isPendingRegister} onClick={onRegister}>Đăng ký</Button>
    )
  }

  const { mutate: createPackageReviewMutate, isPending } = useCreatePackageReview()
  const addingNewReview = () => createPackageReviewMutate(newReview)

  if (isLoading) {
    return (
      <DefaultOverlay />
    )
  }
  if (!packageData?.id) {
    return (
      <div>Không tìm thấy dữ liệu. Vui lòng thử lại sau</div>
    )
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
          <Table>
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
                    src={media.imageUrl}
                    className='rounded-md'
                    alt={activity.title}
                  />
                </Grid.Col>
              ))
            }
          </Grid>
        </Flex>
      </Modal>
    )
  }
  return (
    <div className='flex flex-col gap-3 mb-5'>
      <Card className=' grid grid-cols-3 gap-3 ml-3 mr-3 p-4'>
        <div className=''>
          <Image
            height={1000}
            src={packageData.avatarUrl ?? '/assets/brands/logo.png'}
            className=''
            alt='EcoFarm'
            width={500}
          />
          {
            loadRegisterButton()
          }
        </div>
        <div className='col-span-2'>
          <span className='text-primary-400 font-bold text-3xl'>{packageData?.name}</span>
          <NextUITable hideHeader>
            <TableHeader>
              <TableColumn>NAME</TableColumn>
              <TableColumn>VALUE</TableColumn>
            </TableHeader>
            <TableBody>
              <TableRow key={1}>
                <TableCell>Mã gói farming</TableCell>
                <TableCell>{packageData?.code}</TableCell>
              </TableRow>
              <TableRow key={2}>
                <TableCell>Giá gói farming</TableCell>
                <TableCell>
                  <NumberFormatter className='text-primary-400' thousandSeparator value={packageData?.price} suffix=' VND' />
                </TableCell>
              </TableRow>
              <TableRow key={3}>
                <TableCell>Đã đăng ký</TableCell>
                <TableCell>{packageData?.quantityRegistered}</TableCell>
              </TableRow>
              <TableRow key={4}>
                <TableCell>Số suất còn lại</TableCell>
                <TableCell>{packageData?.quantityRemain}</TableCell>
              </TableRow>
              <TableRow key={5}>
                <TableCell>Thời gian đóng đăng ký</TableCell>
                <TableCell>{dateFormat(new Date(packageData.closeRegisterTime), 'P', 'vi')}</TableCell>
              </TableRow>
              {/* {
                packageData?.closeRegisterTime ? (
                  <TableRow key={5}>
                    <TableCell>Thời gian đóng đăng ký</TableCell>
                    <TableCell>{dateFormat(new Date(packageData.closeRegisterTime), 'P', 'vi')}</TableCell>
                  </TableRow>

                ) : (
                  <></>
                )
              } */}
              <TableRow key={6}>
                <TableCell>Dự kiến bắt đầu</TableCell>
                <TableCell>{packageData?.estimatedStartTime ? dateFormat(new Date(packageData.estimatedStartTime), 'P', 'vi') : 'Không xác định'}</TableCell>
              </TableRow>
              <TableRow key={7}>
                <TableCell>Dự kiến kết thúc</TableCell>
                <TableCell>{packageData?.estimatedEndTime ? dateFormat(new Date(packageData.estimatedEndTime), 'P', 'vi') : 'Không xác định'}</TableCell>
              </TableRow>
              <TableRow key={8}>
                <TableCell>Thời gian bắt đầu</TableCell>
                <TableCell>{packageData?.startTime ? dateFormat(new Date(packageData.startTime), 'P', 'vi') : 'Chưa bắt đầu'}</TableCell>
              </TableRow>
              <TableRow key={9}>
                <TableCell>Thời gian kết thúc</TableCell>
                <TableCell>{packageData?.endTime ? dateFormat(new Date(packageData.endTime), 'P', 'vi') : 'Chưa kết thúc'}</TableCell>
              </TableRow>
              <TableRow key={10}>
                <TableCell>Đánh giá</TableCell>
                <TableCell>
                  <span className='inline font-semibold'>
                    {packageData?.averageRating}
                  </span> <Star className='inline h-1/2' color='yellow' />
                  {` (${packageData?.numbersOfRating} đánh giá)`}
                </TableCell>
              </TableRow>
            </TableBody>
          </NextUITable>
        </div>
      </Card>
      <Card className='grid grid-cols-4 ml-3 mr-3 p-4'>
        <div className='col-span-3'>
          Nhà cung cấp <span className='text-lg text-primary-600 font-bold'>
            {packageData?.enterprise?.fullName}
          </span>
        </div>
        <div className='flex justify-items-end gap-3'>
          <Button color='primary' size='lg'>Xem thông tin</Button>
          <Button color='secondary' size='lg'
            onClick={() => {
              void router.push('/chat');
            }}>Nhắn tin</Button>
        </div>
      </Card>
      <Card className='ml-3 mr-3 p-4'>
        <div>
          <p className='text-primary-400 font-bold text-xl'>Mô tả</p>
          <textarea className='w-full h-16 bg-inherit' disabled value={packageData?.description} />
        </div>
      </Card>
      <Card className='ml-3 mr-3 p-4'>
        <div className='flex flex-col'>
          <p className='text-primary-400 font-bold text-xl'>Hoạt động</p>
          {
            packageData.isRegisteredByCurrentUser === true ?
              <Timeline active={1} bulletSize={24} lineWidth={2}>
                {
                  packageData.endTime && (
                    <Timeline.Item bullet={<Check size={12} />} title={'Kết thúc'}
                      p={3}>
                      <Text c="dimmed" size="sm">Kết thúc gói farming</Text>
                      <Text size="xs" mt={4}>{dateFormat(new Date(packageData.endTime), 'dd/MM/yyyy HH:mm:ss', 'vi')}</Text>
                    </Timeline.Item>
                  )
                }
                {
                  activityData?.map((item: ActivityModel, index) => (
                    <Timeline.Item bullet={<ActivitySquareIcon size={12} />} title={item.title}
                      onClick={() => {
                        setActivityDetailWatching(item);
                        openViewingActivityModal();
                      }}
                      p={3}
                      className='hover:bg-orange-300'
                    >
                      <Text c="dimmed" size="sm">{item.shortDescription}</Text>
                      <Text size="xs" mt={4}>{dateFormat(new Date(item.createdTime), 'dd/MM/yyyy HH:mm:ss', 'vi')}</Text>
                    </Timeline.Item>
                  ))
                }
                {
                  ShowActivityDetail(activityDetailWatching)
                }
                {
                  packageData.startTime && (
                    <Timeline.Item bullet={<Play size={12} />} title='Bắt đầu' p={3}>
                      <Text c="dimmed" size="sm">Bắt đầu gói farming</Text>
                      <Text size="xs" mt={4}>{dateFormat(new Date(packageData.startTime), 'dd/MM/yyyy HH:mm:ss', 'vi')}</Text>
                    </Timeline.Item>
                  )
                }
              </Timeline>
              : <Text>Bạn phải đăng ký gói để xem hoạt động của gói này.</Text>
          }
        </div>
      </Card>
      <Card className='ml-3 mr-3 p-4'>
        <div className='flex flex-col '>
          <div className='flex justify-between'>
            <p className='flex-1 mr-auto text-primary-400 font-bold text-xl'>Đánh giá ({packageReviewsData?.length})</p>
            <Button className='flex-1 w-1/4' fullWidth={false} color='primary'
              isDisabled={!(packageData?.isRegisteredByCurrentUser)}
              onClick={() => {
                //scrollTo({ top: newReviewRef.current?.offsetHeight, behavior: 'smooth' })
                newReviewRef.current?.focus();
                setIsAddingNewReview(true)
              }}>Thêm đánh giá</Button>
          </div>
        </div>
        <div className='flex flex-col gap-3 mt-3'>
          {
            packageReviewsData?.map((review) => (
              <Card key={review.reviewId} className='flex flex-col gap-3 p-3'>
                <div className='flex gap-3'>
                  <div className='flex flex-col'>
                    <Link className='text-primary-400 font-bold text-lg'
                      href={`/user/${review.userId}`}>{review.userFullname}</Link>
                    <span className='text-gray-400 text-sm'>{dateFormat(new Date(review.createdAt!), 'P', 'vi')}</span>
                  </div>
                  <div className='flex-1'>
                    <Rating readOnly value={review.rating} fractions={1} />
                    {/* <span className='inline font-semibold'>
                      {review.rating}
                    </span> <Star className='inline h-3/4' color='yellow' /> */}
                  </div>
                </div>
                <div>
                  <Textarea className='w-full text-xl bg-inherit' disabled value={review.content} />
                </div>
              </Card>
            ))
          }
          {
            isAddingNewReview && (
              <>
                <Card className='gap-3 p-3'>
                  <div>
                    <div className=''>
                      <Text className='inline' fw='bold' ref={newReviewRef}>Đánh giá</Text>
                      <Rating
                        defaultValue={5}
                        readOnly={!isRatingPoints}
                        value={newReview.rating}
                        onChange={(value) => {
                          setNewReview({
                            ...newReview,
                            rating: value
                          })
                        }}
                        fractions={1}
                        size='lg'
                      />
                      <Switch
                        m={2}
                        defaultChecked={false}
                        label='Không đánh giá điểm'
                        onChange={(e) => {
                          if (e.target.checked) {
                            setNewReview({
                              ...newReview,
                              rating: 0
                            })
                            setIsRatingPoints(false)
                          }
                          else {
                            setNewReview({
                              ...newReview,
                              rating: 5
                            })
                            setIsRatingPoints(true)
                          }
                        }}
                      />
                      {/* <Select className='inline w-1/2 font-sans' fullWidth={false}
                        onChange={(e) => {
                          setNewReview({
                            ...newReview,
                            rating: Number(e.target.value)
                          })
                        }}>
                        <SelectItem key={0} value={0}>Không đánh giá</SelectItem>
                        <SelectItem key={1} value={1}>1</SelectItem>
                        <SelectItem key={2} value={2}>2</SelectItem>
                        <SelectItem key={3} value={3}>3</SelectItem>
                        <SelectItem key={4} value={4}>4</SelectItem>
                        <SelectItem key={5} value={5}>5</SelectItem>
                      </Select> */}
                    </div>
                    <Textarea
                      placeholder={'Nhập đánh giá của bạn (không bắt buộc)'}
                      className='w-full text-xl bg-inherit'
                      onChange={(e) => {
                        setNewReview({
                          ...newReview,
                          content: e.target.value
                        })
                      }}
                    />
                  </div>
                </Card>
                <div className='mr-3 flex flex-row gap-3'>
                  <Button color='primary' onClick={() => {
                    addingNewReview();
                    if (!isPending) {
                      setIsAddingNewReview(false)
                    }
                  }}>Đăng đánh giá</Button>
                  <Button color='default' onClick={() => {
                    setIsAddingNewReview(false)
                  }}>Trở về</Button>
                </div>
              </>
            )
          }
        </div>
      </Card>
    </div>
  )
}

PackageDetailScreen.getLayout = (page) => {
  return <MainLayout>{page}</MainLayout>
}
export default PackageDetailScreen
