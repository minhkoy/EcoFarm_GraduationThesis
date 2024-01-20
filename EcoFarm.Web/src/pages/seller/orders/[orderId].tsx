import SellerLayout from "@/layouts/seller/sellerLayout";
import { type NextPageWithLayout } from "@/pages/_app";
import { useRouter } from "next/router";

const SellerOrderDetailScreen: NextPageWithLayout = () => {
  const router = useRouter();
  return (
    <>
      Id: {router.query.orderId}
    </>
  )
}

SellerOrderDetailScreen.getLayout = function getLayout(page) {
  return (
    <SellerLayout>
      {page}
    </SellerLayout>
  )
}

export default SellerOrderDetailScreen;