import { CSSProperties } from "react"

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
}

const TextField = (props: iProps) => {
    return (
        <>
        <label htmlFor={props.name} className="block mb-2 text-sm font-medium text-gray-900">{props.title}</label>
        <input type={props.type} id={props.name} name={props.name}
        style={props.style}
        className="shadow-sm bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
        placeholder={props.placeholder}
        value={props.defaultValue}
        onChange={(e) => {
            props.onChange(e.target.value)
        }}
        />
        {
            props.helperText ? 
            <p className="mt-2 text-sm text-gray-500 dark:text-gray-400">{props.helperText}</p> : <></>
        }        
        </>
    )
}

export default TextField