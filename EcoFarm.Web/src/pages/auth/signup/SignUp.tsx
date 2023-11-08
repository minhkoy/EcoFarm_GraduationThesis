import { useEffect, useState } from "react"
import { SignUpCommand } from "../../../DTOs/auth/SignUpDTO"
import { Link } from "react-router-dom"
import TextField from "../../../components/TextField"
import ComboBox from "../../../components/ComboBox"
import HttpUtils from "../../../shared/HttpUtils"
import RoleDTO from "../../../DTOs/auth/RoleDTO"
import { KeyValuePair } from "../../../shared/Utils"

const SignUp = () => {
    let [command, setCommand] = useState(new SignUpCommand())
    let [userType, setUserType] = useState(new Array<KeyValuePair<number, string>>())

    useEffect(() => {
        HttpUtils.get<RoleDTO, boolean>(HttpUtils.DEV_BASE_URL, HttpUtils.ApiUri.authLogin)
        .then((res) => {})
        .catch(err => {
            console.log(err)
        })
    })
    const handleSubmit = () => {
        
    }
    return (
        <>
            <div className="sm:mx-auto sm:w-full sm:max-w-sm">
                <img className='mx-auto h-10 w-auto' width={100} height={200} src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600" alt="Company logo"/>
                <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">Đăng ký</h2>
            </div>

            <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm shadow-lg shadow-green-500 p-5 bg-white">
                <form className="space-y-6"
                onSubmit={(e) => {
                    e.preventDefault()
                    handleSubmit()
                }}>
                    <div>
                        <TextField 
                            title="Tên đăng nhập"
                            placeholder="Nhập tên đăng nhập ..."
                            onChange={(value) => {
                                command.USERNAME = value;
                                setCommand(command)
                            }}
                        />
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
                    title="Đăng ký với vai trò: " 
                    addBlankItem 
                    blankItemText="-- Chọn vai trò --"
                    onChange={(value) => {
                        // command.UserType = value;
                        // setCommand(command)
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
        </>
    )
}

export default SignUp