import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from "./auth/Login";
import FarmingPackageAddNew from "./farmingPackages/AddNew";
import Layout from "../helpers/components/Layouts/Layout";
import FarmingPackageDetail from "./farmingPackages/Detail";

const RouteManager = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout />}>
                    {/* <Route index /> */}
                    <Route path="farming-package">
                        <Route path="add-new" element={<FarmingPackageAddNew />} />
                        <Route path="detail" element={<FarmingPackageDetail />} />
                    </Route>
                </Route>
                <Route path="auth">
                    <Route path="login" element={<Login />} />
                </Route>
            </Routes>
        </BrowserRouter>
    );
}

export default RouteManager;