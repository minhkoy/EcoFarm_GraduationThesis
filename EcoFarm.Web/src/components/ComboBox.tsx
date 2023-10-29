import { KeyValuePair } from "../shared/Utils"

interface iProps {
    options?: Array<KeyValuePair<string, string>>
    title?: string
    addBlankItem?: boolean
    blankItemText?: string
    onChange: (value: string) => void
}

const ComboBox = (props: iProps) => {
    return (
        <>
        <label htmlFor="combo" className="block mb-2 text-sm text-black font-medium">{props.title}</label>
        <select id="combo" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
        onChange={(e) => props.onChange(e.target.value)}
        >
            {
                props.addBlankItem ? (
                <option selected value={""}>{props.blankItemText}</option>
                ) : <></>
            }
            {
                props.options?.map((x, index) => 
                    (
                        <option key={x.key}>{x.value}</option>
                    )
                )
            }       
        </select>
        </>
    )
}

export default ComboBox