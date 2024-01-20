import { UserInfo } from "@/components/ui/personalInfo/UserInfo";
import TextTitle from "@/components/ui/texts/TextTitle";
import MainLayout from "@/layouts/common/main";
import { type NextPageWithLayout } from "@/pages/_app";
import { Card, Flex, Grid } from "@mantine/core";

const YourPackagesScreen: NextPageWithLayout = () => {
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
            <TextTitle>Các gói farming bạn đăng ký</TextTitle>
          </Flex>
        </Flex>
      </Grid.Col>
    </Grid>
  )
}

YourPackagesScreen.getLayout = function getLayout(page) {
  return (
    <MainLayout>
      {page}
    </MainLayout>
  )
}
export default YourPackagesScreen;