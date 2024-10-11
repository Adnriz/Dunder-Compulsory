import React, { useEffect, useState } from 'react';
import { apiService } from '../apiService';
import { Order } from '../myApi';

interface Props {
    customerId: number;
}

const CustomerOrders: React.FC<Props> = ({ customerId }) => {
    const [orders, setOrders] = useState<Order[]>([]);

    useEffect(() => {
        async function fetchOrders() {
            const response = await apiService.getOrdersByCustomer(customerId);
            setOrders(response.data);
        }
        fetchOrders();
    }, [customerId]);

    return (
        <div>
            <h2>Order History</h2>
            <ul>
                {orders.map(order => (
                    <li key={order.id}>
                        Order ID: {order.id}, Status: {order.status}, Total: {order.totalAmount}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default CustomerOrders;