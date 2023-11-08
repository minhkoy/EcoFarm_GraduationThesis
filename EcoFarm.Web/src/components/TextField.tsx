import { CSSProperties, useState } from "react"

interface iProps {
    style?: CSSProperties;
    placeholder?: string;
    icon?: React.FC;
    defaultValue?: string;
    onChange: (value: string) => void;
    type?: string
    helperText?: string;
    name?: string;
    title: string;
    //Validation props:
    required?: boolean;
    requiredMessage?: string;
    regex?: RegExp;
    regexMessage?: string;
}

const TextField = (props: iProps) => {
    let [errorMessage, setErrorMessage] = useState('')
    return (
        <>
        <label htmlFor={props.name} className="block mb-2 text-sm font-medium text-gray-900">{props.title}</label>
        <input type={props.type} id={props.name} name={props.name}
        style={props.style}
        className="shadow-sm bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
        placeholder={props.placeholder}
        value={props.defaultValue}
        onChange={(e) => {
            let value = e.target.value;
            if (props.required && !value) {
                setErrorMessage(props.requiredMessage || 'Vui lòng nhập trường thông tin này.')
            } else if (props.regex && !props.regex.test(value)) {
                setErrorMessage(props.regexMessage || 'Vui lòng nhập đúng định dạng.')
            } else {
                setErrorMessage('')
            }
            props.onChange(value)
        }}
        />
        {
            errorMessage ? 
            <p className="mt-2 text-sm text-red-600">{errorMessage}</p> : <></>
        }
        {
            props.helperText ? 
            <p className="mt-2 text-sm text-gray-500">{props.helperText}</p> : <></>
        }        
        </>
    )
}

export default TextField