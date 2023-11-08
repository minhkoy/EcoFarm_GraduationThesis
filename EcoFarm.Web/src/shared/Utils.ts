interface KeyValuePair<K, V> {
    key: K,
    value: V
}

interface ValidationError {
    propertyName?: string;
    errorMessage?: string;
    attemptedValue?: any;
    customState?: any;
    severity?: number;
    errorCode?: string;
    formattedMessagePlaceholderValues?: {
        PropertyName?: string;
        PropertyValue?: string;
        PropertyPath?: string;
    }
}

class Utils {
    static getFirstKeyFromValue<K, V>(value: V, arr: Array<KeyValuePair<K, V>>) {
        let result: K | undefined = undefined;
        arr.forEach((x) => {
            if (x.value === value) {
                result = x.key;
                return;
            }
        })
        return result;
    }
}
export type {KeyValuePair, ValidationError}
export default Utils