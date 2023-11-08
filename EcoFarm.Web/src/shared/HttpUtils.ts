import axios, { Axios, AxiosError, AxiosResponse } from "axios"
import { KeyValuePair, ValidationError } from "./Utils";
import EFX from "./EFX";

class Result<T> {
    Data?: T;
    ResultType?: number; //Consider string
    Errors?: Array<ValidationError>;
    Message?: string;
};

export default class HttpUtils {
    static DEV_BASE_URL = "https://localhost:5001/api/";
    static ApiUri = {
        "authLogin": "auth/login",
        "authRole": "auth/get-list-roles",
    };

    static get = async <TRequest, TResponse>(baseUrl: string, uri?: string, params?: TRequest, headers?: Array<KeyValuePair<string, string>>) => {
        let apiUrl = new URL(uri ?? '/', baseUrl);
        let token = localStorage.getItem('ecofarm_token');
        if (token) {
            headers?.push({
                key: 'Authorization',
                value: 'Bearer ' + token
            });
        }
        let result = await axios.get<Result<TResponse>>(apiUrl.toString(), {
            params: params,
            headers: {
                'Content-Type': 'application/json',
                //"Authorization": "Bearer " + localStorage.getItem("ecofarm_token")
            }
        })
        return result;
    }

    static post = async <TRequest, TResponse>(baseUrl: string, uri?: string, body?: TRequest, headers?: Array<KeyValuePair<string, string>>) => {
        let apiUrl = new URL(uri ?? '/', baseUrl);
        let token = localStorage.getItem('ecofarm_token');
        if (token) {
            headers?.push({
                key: 'Authorization',
                value: 'Bearer ' + token
            });
        }
        let result = await axios.post<TRequest, Result<TResponse>>(apiUrl.toString(), {
            body: body,
            Headers: {
                ...headers,
                'Content-Type': 'application/json',
                //'Authorization': 'Bearer ' + localStorage.getItem('ecofarm_token')
            }
        });
        return result;
    }
}