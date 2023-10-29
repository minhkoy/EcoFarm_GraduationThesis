import { useEffect, useState } from "react";

interface iProps {
    value?: any;
    required?: boolean;
    maxLength?: number;
    minLength?: number;
    
}

const FieldValidator = (props: iProps) => {
    const [isValid, setIsValid] = useState(true);
    const [message, setMessage] = useState("");
    let value = props.value;
    
    useEffect(() => {
        if (props.required && !value) {
            setIsValid(false)
            setMessage('Không được để trống thông tin này')
        }
        //else if (props.minLength && value.toString() )
    }, [props.required, value])

    if (isValid) {
        return (<></>)
    }

    return (
        <>
        <div className="mt-2 text-sm text-red-600">
            {message}
        </div>
        </>
    )
}

export default FieldValidator