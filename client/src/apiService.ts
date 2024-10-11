import axios from 'axios';
import {
    CreateOrderDto,
    CreatePaperDto
} from './myApi';

const API_URL = 'http://localhost:5000/api';

const instance = axios.create({
    baseURL: API_URL,
    headers: {
        "Content-Type": "application/json",
    },
});

export const apiService = {
    placeOrder: (order: CreateOrderDto) => instance.post('/orders', order),
    getOrdersByCustomer: (customerId: number) => instance.get(`/orders/customer/${customerId}`),
    getAllOrders: () => instance.get('/orders'),
    updateOrderStatus: (orderId: number, status: string) => instance.put(`/orders/${orderId}/status`, status),

    createProduct: (product: CreatePaperDto) => instance.post('/products', product),
    restockProduct: (productId: number, quantity: number) => instance.put(`/products/${productId}`, quantity),
    discontinueProduct: (productId: number) => instance.delete(`/products/${productId}`),
};