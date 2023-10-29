import Title from "antd/es/typography/Title"
import { useState } from "react";
import { ForgetPasswordCommand } from "./ForgetPasswordDTO";

const ForgetPassword = () => {
    let [command, setCommand] = useState(new ForgetPasswordCommand())
    return (
        <>
        <div className="flex min-h-full h-screen flex-col justify-center px-6 py-12 lg:px-8 bg-bg-login bg-cover bg-no-repeat"
        >
            <div className="sm:mx-auto sm:w-full sm:max-w-sm">
                <img className='mx-auto h-10 w-auto' width={100} height={200} src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600" alt="Company logo"/>
                <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">Quên mật khẩu</h2>
            </div>
            <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm shadow-lg shadow-green-500 p-5 bg-white">
                <form className="space-y-6">

                    <div>
                        <label htmlFor="username" className="block text-sm font-medium leading-6 text-gray-900">Tên đăng nhập</label>
                        <div className="mt-2">
                            <input id="username" name="username" type="username" 
                            autoComplete="username" //placeholder="" 
                            required className="block px-3 w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" 
                            onChange={(e) => {
                                command.Username = e.target.value;
                                setCommand(command)
                            }}
                            />
                        </div>
                    </div>

                    <div>
                        <label htmlFor="mail" className="block text-sm font-medium leading-6 text-gray-900">Email</label>
                        <div className="mt-2">
                            <input id="mail" name="mail" type="email" 
                            autoComplete="email" //placeholder="Type username or email here ..." 
                            required className="block px-3 w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" 
                            onChange={(e) => {
                                command.Email = e.target.value;
                                setCommand(command)
                            }}
                            />
                        </div>
                    </div>
                </form>            
            </div>        
        </div>
        </>
    )
}

export default ForgetPassword