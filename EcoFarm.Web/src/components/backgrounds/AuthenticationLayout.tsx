import { Outlet } from "react-router-dom"

const AuthenticationLayout = () => {
    return (
        <div className="flex min-h-full flex-col justify-center px-6 py-12 lg:px-8 bg-bg-auth bg-cover bg-no-repeat h-screen">
            <Outlet />
        </div>
    )
}

export default AuthenticationLayout