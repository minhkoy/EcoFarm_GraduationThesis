import Utils, { KeyValuePair } from "../shared/Utils"

interface iProps<K, V> {
    options?: Array<KeyValuePair<K, V>>
    title?: string
    addBlankItem?: boolean
    blankItemText?: string
    onChange: (value: string, key?: string) => void
}

const ComboBox = <K, V>(props: iProps<K, V>) => {
    return (
        <>
        <label htmlFor="combo" className="block mb-2 text-sm text-black font-medium">{props.title}</label>
        <select id="combo" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
        onChange={(e) => {
            let val = e.target.value;
            props.onChange(val, Utils.getFirstKeyFromValue(val as V, props.options!))
        }}
        >
            {
                props.addBlankItem ? (
                <option selected value={""}>{props.blankItemText}</option>
                ) : <></>
            }
            {
                props.options?.map((x, index) => 
                    (
                        <option key={x.key as string}>{x.value as string}</option>
                    )
                )
            }       
        </select>
        </>
    )
}

export default ComboBox