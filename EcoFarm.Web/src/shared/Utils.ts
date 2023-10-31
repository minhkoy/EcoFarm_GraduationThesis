interface KeyValuePair<K, V> {
    key: K,
    value: V
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
export type {KeyValuePair}
export default Utils