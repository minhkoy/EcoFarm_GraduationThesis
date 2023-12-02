import axios, { AxiosResponse } from 'axios';
import BaseResultDTO from '../DTOs/BaseResultDTO';

export default class HttpUtils {
    static API_BASE_URL = 'https://localhost:7019/api';
    private static accessToken = localStorage.getItem('ecofarm_accessToken') || '';

    static async get<T>(url: string, params: unknown): Promise<BaseResultDTO<T>> {
        const fullUrl = new URL(url, this.API_BASE_URL).toString();
        const response: AxiosResponse<BaseResultDTO<T>> = await axios.get(fullUrl, {
            headers: {
                'Authorization': `Bearer ${this.accessToken}`
            },
            params: params
        });
        return response.data;
    }

    static async post<T>(url: string, body: unknown): Promise<BaseResultDTO<T>> {
        const fullUrl = new URL(url, this.API_BASE_URL).toString();
        const response: AxiosResponse<BaseResultDTO<T>> = await axios.post(fullUrl, body, {
            headers: {
                'Authorization': `Bearer ${this.accessToken}`
            }
        });
        return response.data;
    }

    static async put<T>(url: string, body: unknown): Promise<BaseResultDTO<T>> {
        const fullUrl = new URL(url, this.API_BASE_URL).toString();
        const response: AxiosResponse<BaseResultDTO<T>> = await axios.put(fullUrl, body, {
            headers: {
                'Authorization': `Bearer ${this.accessToken}`
            }
        });
        return response.data;
    }

    static async delete<T>(url: string, params: unknown): Promise<BaseResultDTO<T>> {
        const fullUrl = new URL(url, this.API_BASE_URL).toString();
        const response: AxiosResponse<BaseResultDTO<T>> = await axios.delete(fullUrl, {
            headers: {
                'Authorization': `Bearer ${this.accessToken}`
            },
            params: params
        });
        return response.data;
    }
}

export class ApiUrls {
    static readonly Login = 'api/Authentication/Login';

    static readonly GetListPackage = 'api/FarmingPackage/GetList';
}

