import React, { useState } from 'react';
import { apiService } from '../apiService';
import { CreatePaperDto } from '../myApi';

const ProductManagement: React.FC = () => {
    const [product, setProduct] = useState<CreatePaperDto>({
        name: '',
        discontinued: false,
        stock: 0,
        price: 0,
    });
    const [restockProductId, setRestockProductId] = useState<number | null>(null);
    const [restockQuantity, setRestockQuantity] = useState<number>(0);
    const [discontinueProductId, setDiscontinueProductId] = useState<number | null>(null);

    const handleProductCreate = async (event: React.FormEvent) => {
        event.preventDefault();
        await apiService.createProduct(product);
        
    };

    const handleRestock = async (event: React.FormEvent) => {
        event.preventDefault();
        if (restockProductId !== null) {
            await apiService.restockProduct(restockProductId, restockQuantity);
            
        }
    };

    const handleDiscontinue = async (event: React.FormEvent) => {
        event.preventDefault();
        if (discontinueProductId !== null) {
            await apiService.discontinueProduct(discontinueProductId);
            
        }
    };

    return (
        <div>
            <h2>Product Management</h2>
            <form onSubmit={handleProductCreate}>
                <input
                    type="text"
                    placeholder="Product Name"
                    value={product.name || ''}
                    onChange={(e) => setProduct({ ...product, name: e.target.value })}
                />
                <input
                    type="number"
                    placeholder="Stock"
                    value={product.stock || 0}
                    onChange={(e) => setProduct({ ...product, stock: Number(e.target.value) })}
                />
                <input
                    type="number"
                    placeholder="Price"
                    value={product.price || 0}
                    onChange={(e) => setProduct({ ...product, price: Number(e.target.value) })}
                />
                <button type="submit">Create Product</button>
            </form>

            <div>
                <h3>Restock Product</h3>
                <form onSubmit={handleRestock}>
                    <input
                        type="number"
                        placeholder="Product ID"
                        value={restockProductId ?? ''}
                        onChange={(e) => setRestockProductId(Number(e.target.value))}
                    />
                    <input
                        type="number"
                        placeholder="Quantity"
                        value={restockQuantity}
                        onChange={(e) => setRestockQuantity(Number(e.target.value))}
                    />
                    <button type="submit">Restock</button>
                </form>
            </div>

            <div>
                <h3>Discontinue Product</h3>
                <form onSubmit={handleDiscontinue}>
                    <input
                        type="number"
                        placeholder="Product ID"
                        value={discontinueProductId ?? ''}
                        onChange={(e) => setDiscontinueProductId(Number(e.target.value))}
                    />
                    <button type="submit">Discontinue</button>
                </form>
            </div>
        </div>
    );
};

export default ProductManagement;