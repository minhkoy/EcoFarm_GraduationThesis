import { useState } from "react";
import { LoginCommand } from "./LoginDTO";
import ComboBox from "../../../components/ComboBox";
import { KeyValuePair } from "../../../shared/Utils";
import { Link } from "react-router-dom";
import SignUp from "../signup/SignUp";

//type Page = () => React.FC;
interface iProps {
    prevUrl?: string;
}

interface iState {
    request?: LoginCommand;
}

const Login = (props: iProps) => {
    const [command, setCommand] = useState(new LoginCommand())
    const userType: Array<KeyValuePair<string, string>> = [
        {key: "user", value: "Người tìm dịch vụ"},
        {key: "erp", value: "Tổ chức/ cá nhân cung cấp dịch vụ"},
        {key: "adm", value: "Quản trị hệ thống"}
    ];

    const handleSubmit = (val?: any) => {
        console.log(command);
    }

    return (
        <>
        <div className="flex min-h-full flex-col justify-center px-6 py-12 lg:px-8 bg-bg-login bg-cover bg-no-repeat"
        >
            <div className="sm:mx-auto sm:w-full sm:max-w-sm">
                <img className='mx-auto h-10 w-auto' width={100} height={200} src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600" alt="Company logo"/>
                <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">Đăng nhập vào hệ thống</h2>
            </div>

            <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm shadow-lg shadow-green-500 p-5 bg-white">
                <form className="space-y-6"
                onSubmit={(e) => {
                    e.preventDefault()
                    handleSubmit()
                }}>
                    <div>
                        <label htmlFor="username" className="block text-sm font-medium leading-6 text-gray-900">Tên đăng nhập hoặc email</label>
                        <div className="mt-2">
                            <input id="username" name="username" type="username" 
                            autoComplete="usernameOrEmail" placeholder="Type username or email here ..." 
                            required className="block px-3 w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" 
                            onChange={(e) => {
                                command.UsernameOrEmail = e.target.value;
                                setCommand(command)
                            }}
                            />
                        </div>
                    </div>

                    <div>
                        <div className="flex items-center justify-between">
                            <label htmlFor="password" className="block text-sm font-medium leading-6 text-gray-900">Mật khẩu</label>
                            <div className="text-sm">
                            <Link to='/auth/forget-password' className="font-semibold text-indigo-600 hover:text-indigo-500">Quên mật khẩu</Link>
                            </div>
                        </div>
                        <div className="mt-2">
                            <input id="password" name="password" 
                            type="password" autoComplete="current-password" 
                            required 
                            className="block px-3 w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" 
                            onChange={(e) => {
                                command.Password = e.target.value
                                setCommand(command)
                            }}
                            />
                        </div>
                    </div>
                    <ComboBox 
                    options={userType} 
                    title="Đăng nhập với vai trò: " 
                    addBlankItem 
                    blankItemText="-- Chọn vai trò --"
                    onChange={(value) => {
                        command.UserType = value;
                        setCommand(command)
                    }}
                    />
                    <div>
                        <button type="submit" className="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600">Đăng nhập</button>
                    </div>
                </form>

                <p className="mt-10 text-center text-sm text-gray-500">
                    Chưa có tài khoản?
                    <Link to='/auth/signup' className="font-semibold leading-6 text-indigo-600 hover:text-indigo-500"> Đăng ký tại đây...</Link>
                </p>
            </div>
        </div>
        </>
        
    )
}

export default Login