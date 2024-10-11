import React, { useState } from 'react';
import { apiService } from '../apiService';
import { CreateOrderDto, CreateOrderEntryDto } from '../myApi';

const OrderForm: React.FC = () => {
    const [customerId, setCustomerId] = useState<number | null>(null);
    const [orderEntries, setOrderEntries] = useState<CreateOrderEntryDto[]>([]);
    const [entry, setEntry] = useState<CreateOrderEntryDto>({ quantity: 1, productId: 0 });

    const handleAddEntry = () => {
        setOrderEntries([...orderEntries, { ...entry }]);
        setEntry({ quantity: 1, productId: 0 }); // Reset entry fields
    };

    const handleOrderSubmit = async (event: React.FormEvent) => {
        event.preventDefault();

        const order: CreateOrderDto = {
            orderDate: new Date().toISOString(),
            status: 'Pending',
            customerId: customerId!,
            totalAmount: 0,
            orderEntries: orderEntries,
        };

        await apiService.placeOrder(order);
        // Handle the response / success
    };

    return (
        <div>
            <h2>Place Order</h2>
            <form onSubmit={handleOrderSubmit}>
                <input
                    type="number"
                    placeholder="Customer ID"
                    value={customerId ?? ''}
                    onChange={(e) => setCustomerId(Number(e.target.value))}
                />
                <div>
                    <h3>Order Entries</h3>
                    <div>
                        <input
                            type="number"
                            placeholder="Product ID"
                            value={entry.productId}
                            onChange={(e) => setEntry({ ...entry, productId: Number(e.target.value) })}
                        />
                        <input
                            type="number"
                            placeholder="Quantity"
                            value={entry.quantity}
                            onChange={(e) => setEntry({ ...entry, quantity: Number(e.target.value) })}
                        />
                        <button type="button" onClick={handleAddEntry}>Add Entry</button>
                    </div>
                    <ul>
                        {orderEntries.map((entry, index) => (
                            <li key={index}>
                                Product ID: {entry.productId}, Quantity: {entry.quantity}
                            </li>
                        ))}
                    </ul>
                </div>
                <button type="submit">Place Order</button>
            </form>
        </div>
    );
};

export default OrderForm;