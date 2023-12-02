import { useEffect } from "react";
import HttpUtils, { ApiUrls } from "../../helpers/Utils/HttpUtils";
import FarmingPackageDTO from "../../helpers/DTOs/FarmingPackageDTO";

const FarmingPackageDetail = () => {
    useEffect(() => {
        HttpUtils.get<FarmingPackageDTO>(ApiUrls.GetListPackage)
        .then((res) => {console.log(res)})
    })
    return (
        <>
        Test
        </>
    )
}

export default FarmingPackageDetail;