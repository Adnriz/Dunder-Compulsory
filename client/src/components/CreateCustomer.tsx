import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

const CreateCustomer: React.FC = () => {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [address, setAddress] = useState('');
    const [phone, setPhone] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            await axios.post('/api/customers', { name, email, address, phone });
            navigate('/'); 
        } catch (error) {
            console.error("There was an error creating the customer!", error);
        }
    };

    return (
        <div className="create-customer-container">
            <h2>Create Customer</h2>
            <form onSubmit={handleSubmit}>
                <label>
                    Name:
                    <input type="text" value={name} onChange={(e) => setName(e.target.value)} required />
                </label>
                <label>
                    Email:
                    <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} required />
                </label>
                <label>
                    Address:
                    <input type="text" value={address} onChange={(e) => setAddress(e.target.value)} required />
                </label>
                <label>
                    Phone:
                    <input type="text" value={phone} onChange={(e) => setPhone(e.target.value)} required />
                </label>
                <button type="submit">Create Customer</button>
            </form>
        </div>
    );
};

export default CreateCustomer;