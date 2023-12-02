import { Outlet } from "react-router-dom";

const AuthenticationLayout = () => {
    return (
        <>
            <main className="w-screen bg-bg-login">
                <Outlet />
            </main>
        </>
    )
}

export default AuthenticationLayout;