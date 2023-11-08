import Title from "antd/es/typography/Title"
import { useState } from "react";
import { ForgetPasswordCommand } from "../../../DTOs/auth/ForgetPasswordDTO";
import TextField from "../../../components/TextField";
import ComboBox from "../../../components/ComboBox";
import { KeyValuePair } from "../../../shared/Utils";
import { Link } from "react-router-dom";

type recoverType = 'username' | 'email';
const recoverOptions: Array<KeyValuePair<string, string>> = [
    {key: 'username', value: 'Khôi phục bằng tên đăng nhập'},
    {key: 'email', value: 'Khôi phục bằng email'}
]

const ForgetPassword = () => {
    let [command, setCommand] = useState(new ForgetPasswordCommand())
    let [rcvType, setRcvType] = useState('username' as recoverType)
    
    const renderInputField = () => {
        if (rcvType === 'username') {
            return (
                <div>
                    <TextField 
                        title="Tên đăng nhập"
                        type="text"
                        name="username"
                        onChange={(value) => {
                            command.Username = value;
                            setCommand(command)
                        }}
                        required
                        helperText="Nếu tồn tại tên đăng nhập, chúng tôi sẽ gửi email khôi phục mật khẩu cho bạn, theo mail bạn đã đăng ký."
                    />
                </div>
            )
        }
        if (rcvType === 'email') {
            return (
                <div>
                    <TextField 
                        title="Email khôi phục"
                        type="email"
                        name="email"
                        required
                        regex={/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/}
                        regexMessage="Vui lòng nhập đúng định dạng email."
                        onChange={(value) => {
                            command.Email = value;
                            setCommand(command)
                        }}
                        helperText="Nếu tồn tại email trong hệ thống, chúng tôi sẽ gửi email khôi phục mật khẩu cho bạn."
                    />
                </div>
            )
        }
    }
    return (
        <>
            <div className="sm:mx-auto sm:w-full sm:max-w-sm">
                <img className='mx-auto h-10 w-auto' width={100} height={200} src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600" alt="Company logo"/>
                <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">Quên mật khẩu</h2>
            </div>
            <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm shadow-lg shadow-green-500 p-5 bg-white">
                <form className="space-y-6">
                    <ComboBox 
                        title="Chọn hình thức khôi phục"
                        options={recoverOptions}
                        onChange={(value, key) => {
                            if (key === 'username' || key === 'email') {
                                setRcvType(key)
                            }
                            console.log(key)
                        }}
                    />
                    <div>
                        {renderInputField()}
                    </div>
                    <div>
                        <button type="reset" 
                        className="flex w-full justify-center rounded-md bg-green-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-green-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-green-900"
                        onClick={(e) => {
                            e.preventDefault();

                        }}
                        >
                            Hoàn tất
                        </button>
                        <Link to={'/auth/login'} 
                        className="flex w-full justify-center rounded-md mt-2 px-3 py-1.5 text-sm bg-indigo-600 font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                        >Trở về đăng nhập</Link>
                    </div>
                </form>            
            </div>        
        </>
    )
}

export default ForgetPassword