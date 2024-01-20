import DefaultOverlay from "@/components/ui/overlay/DefaultOverlay";
import useAuth from "@/hooks/auth/useAuth";
import { ACCOUNT_TYPE } from "@/utils/constants/enums";
import { Button, Container, Text, Title } from "@mantine/core";
import { useRouter } from "next/router";
import { useTranslation } from "react-i18next";

const ForbiddenScreen = () => {
  const { t } = useTranslation();
  const { accountInfo, isFetching } = useAuth();
  const router = useRouter();
  if (isFetching) {
    return (
      <DefaultOverlay />
    )
  }
  return (
    <Container size="sm" style={{ textAlign: 'center' }}>
      <Title order={1} m={2}>
        EcoFarm rất tiếc 🚫
      </Title>
      <Text style={{ marginBottom: 20 }}>
        {/* {t('access-denied.description', { ns: 'error' })} */}
        Bạn không có quyền truy cập vào trang này
      </Text>
      <Button fullWidth
        onClick={() => {
          if (accountInfo && accountInfo.accountType === ACCOUNT_TYPE.SELLER) {
            void router.push('/seller/homepage');
          }
          if (accountInfo && accountInfo.accountType === ACCOUNT_TYPE.CUSTOMER) {
            void router.push('/');
          }
        }}
      >
        Trở về trang chủ
      </Button>
    </Container>
  )
}

export default ForbiddenScreen;