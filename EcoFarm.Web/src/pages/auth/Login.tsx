//import { Button } from '@nextui-org/react'
import { useState } from 'react'
import { ZodError, z } from 'zod'
import reactSvg from '../../assets/react.svg'
import {Button, Label, Spinner, TextInput } from 'flowbite-react'
import HttpUtils, { ApiUrls } from '../../helpers/Utils/HttpUtils'
//import BaseResultDTO from '../../helpers/DTOs/BaseResultDTO'
import LoginDTO from '../../helpers/DTOs/Auth/LoginDTO'

type LoginCommand = z.infer<typeof loginSchema>

interface iProps {
    prevUrl?: string;
}

const loginSchema = z.object({
    usernameOrEmail: z.string({
        required_error: 'Cần nhập thông tin tên đăng nhập/ email!',
        invalid_type_error: 'Tên đăng nhập/ email không hợp lệ!'
    }).min(1, "Cần nhập thông tin tên đăng nhập/ email!"),
    password: z.string({
        required_error: 'Cần nhập mật khẩu!',
        invalid_type_error: 'Mật khẩu không hợp lệ!'
    }).min(1, "Cần nhập mật khẩu!")
})

const Login = () => {

    const [errors, setErrors] = useState({});
    const [isLoading, setIsLoading] = useState(false);
    const handleSubmit = async () => {
        setIsLoading(true);
        const isValid = validateLogin(data);
        if (!isValid) {
            setIsLoading(false);
            return;
        }
        const result = await HttpUtils.post<LoginDTO>(ApiUrls.Login, data);
        setIsLoading(false);
        console.log(result)
        // if (result.isSuccess) {
        //     alert(result.value);
        // }
        // else {
        //     alert(result.status);
        // }
    }
    const validateLogin = (inputs: LoginCommand) => {
        try {
            loginSchema.parse(inputs);
            setErrors([]); // Clear errors if validation is successful
            return true;
        } catch (error) {
            if (error instanceof ZodError) {
                const fieldErrors = {};
        
                error.errors.forEach((validationError) => {
                    fieldErrors[validationError.path[0]] = validationError.message;
                });
        
                setErrors(fieldErrors);
                
            }
            return false;
        }
    }

    const [data, setData] = useState<LoginCommand>({
        usernameOrEmail: '',
        password: ''
    })

    return (
        <div className="w-full  min-h-screen bg-white px-5 py-5">
            <div className="grid grid-cols-2 xl:max-w-7xl bg-white drop-shadow-xl border border-black/20 w-full rounded-md justify-between items-stretch px-5 xl:px-5 py-5">
                <div className="sm:w-[60%] lg:w-[90%] bg-cover bg-center items-center justify-center hidden md:flex ">
                    <img
                        src={reactSvg}
                        alt="login"
                        className="w-screen h-[500px]"
                    />
                </div>         
        
                <form className='flex items-center max-w-md flex-col gap-4'>
                    <h1 className="text-center text-2xl sm:text-3xl font-semibold text-[#4A07DA]">
                        Đăng nhập
                    </h1> 
                    <div className=' w-full'>
                        <div className="mb-2 block">
                            <Label htmlFor="email1" value="Tên đăng nhập hoặc email" />
                        </div>
                        <TextInput id="email1" name='usernameOrEmail' type="email" placeholder="" required
                            onChange={(e) => {
                                setData({
                                    ...data,
                                    usernameOrEmail: e.target.value                                
                                })
                            }}
                            helperText={errors?.usernameOrEmail && <span className='text-red-500'>{errors.usernameOrEmail}</span>}
                        />
                    </div>
                    <div className='w-full'>
                        <div className="mb-2 block">
                            <Label htmlFor="password1" value="Mật khẩu" />
                        </div>
                        <TextInput id="password1" name='password' type="password" required 
                            onChange={(e) => {
                                setData({
                                    ...data,
                                    password: e.target.value
                                })
                            }}
                        />
                    </div>
                    <div className='w-full'>
                        <div className="mb-2 flex flex-row">
                            <Button color='success' className="w-1/2 bg-green-400 items-center mx-2"
                            onClick={handleSubmit}>
                                Đăng nhập
                            </Button>
                            <Button color='blue' className='text-white w-1/2 items-center mx-2'>
                                Đăng ký    
                            </Button>                                
                        </div>
                    </div>
                    <div className='w-full text-center'>
                        {
                            isLoading ? <Spinner /> : <></>
                        }
                    </div>
                </form>
            </div>
        </div>
    )
}

export default Login