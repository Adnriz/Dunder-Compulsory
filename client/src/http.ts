import axios, { AxiosInstance, AxiosRequestConfig } from 'axios';
import {
    CreateOrderDto,
    CreatePaperDto
} from './myApi.ts';

interface ApiConfig {
    baseUrl: string;
}

class Api {
    private axiosInstance: AxiosInstance;

    constructor(config: ApiConfig) {
        this.axiosInstance = axios.create({
            baseURL: config.baseUrl,
            headers: {
                "Content-Type": "application/json",
            },
        });
    }

    public api = {
        orderAddOrder: (order: CreateOrderDto) => this.axiosInstance.post("/orders", order),
        paperUpdateStock: (stockUpdate: CreatePaperDto) => this.axiosInstance.put("/stock", stockUpdate),
    };
}

export const baseUrl = 'http://localhost:5000';

export const http = new Api({ baseUrl: baseUrl });