import axios, { AxiosResponse } from 'axios';
import BaseResultDTO from '../DTOs/BaseResultDTO';

export default class HttpUtils {
    static API_BASE_URL = 'https://localhost:7019/api';

    static async get<T>(url: string): Promise<BaseResultDTO<T>> {
        const fullUrl = new URL(url, this.API_BASE_URL).toString();
        const response: AxiosResponse<BaseResultDTO<T>> = await axios.get(fullUrl);
        return response.data;
    }

    static async post<T>(url: string, body: unknown): Promise<BaseResultDTO<T>> {
        const fullUrl = new URL(url, this.API_BASE_URL).toString();
        const response: AxiosResponse<BaseResultDTO<T>> = await axios.post(fullUrl, body);
        return response.data;
    }

    static async put<T>(url: string, body: unknown): Promise<BaseResultDTO<T>> {
        const fullUrl = new URL(url, this.API_BASE_URL).toString();
        const response: AxiosResponse<BaseResultDTO<T>> = await axios.put(fullUrl, body);
        return response.data;
    }

    static async delete<T>(url: string): Promise<BaseResultDTO<T>> {
        const fullUrl = new URL(url, this.API_BASE_URL).toString();
        const response: AxiosResponse<BaseResultDTO<T>> = await axios.delete(fullUrl);
        return response.data;
    }
}

export class ApiUrls {
    static readonly Login = '/auth/login';
}

