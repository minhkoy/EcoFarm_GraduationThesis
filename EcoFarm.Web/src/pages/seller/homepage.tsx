import SellerLayout from "@/layouts/seller/sellerLayout";
import { type NextPageWithLayout } from "../_app";

const SellerHomepage: NextPageWithLayout = () => {
  return (
    <div>
      <h1>Seller Homepage</h1>
    </div>
  );
}

SellerHomepage.getLayout = function getLayout(page) {
  return (
    <SellerLayout>
      {page}
    </SellerLayout>
  );
}

export default SellerHomepage;