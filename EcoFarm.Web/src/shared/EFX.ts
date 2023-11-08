import { KeyValuePair } from "./Utils";

export default class EFX {
    static ResultTypes: Array<KeyValuePair<number, string>> = [
        {key: 0, value: "Ok"},
        {key: 1, value: "Redirect"},
        {key: 2, value: "BadRequest"},
        {key: 3, value: "Unexpected"},
        {key: 4, value: "Unauthorized"},
        {key: 5, value: "Forbidden"},
        {key: 6, value: "NotFound"},
        {key: 7, value: "InternalServerError"},
    ]
    
    static UserTypes: Array<KeyValuePair<number, string>> = [
        {key: 1, value: "Admin"},
    ]
}