import { Layout } from "antd"
import { BrowserRouter, Routes, Route } from "react-router-dom"
import Home from "./Home"
import Login from "./auth/login/Login"
import Error404Page from "./Error404Page"
import SignUp from "./auth/signup/SignUp"
import ForgetPassword from "./auth/forgetPassword/ForgetPassword"
import AuthenticationLayout from "../components/backgrounds/AuthenticationLayout"

const RouteManager = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path='/' errorElement={<Error404Page />}>
                    <Route index element={<Home />} />
                    <Route path='auth' element={<AuthenticationLayout />}>
                        <Route path="login" element={<Login />}/>
                        <Route path="signup" element={<SignUp />} />
                        <Route path="forget-password" element={<ForgetPassword />} />
                    </Route>
                </Route>
                <Route path="*" element={<Error404Page />} />
            </Routes>
        </BrowserRouter>
    )
}

export default RouteManager