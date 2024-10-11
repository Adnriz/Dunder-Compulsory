import React from 'react';
import { Routes, Route, Link, useLocation, useParams } from 'react-router-dom';
import Home from './components/HomePage';
import OrderForm from './components/OrderForm';
import CustomerOrders from './components/CustomerOrders';
import ProductManagement from './components/ProductManagement';
import CreateCustomer from './components/CreateCustomer';

// Dummy customer ID for demonstration purposes
const demoCustomerId = 1;

const App: React.FC = () => {
    return <AppContent />;
};

const AppContent: React.FC = () => {
    const location = useLocation();

    return (
        <div>
            {location.pathname !== '/' && (
                <nav>
                    <ul>
                        <li><Link to="/">Home</Link></li>
                        <li><Link to="/create-customer">Create Customer</Link></li>
                        <li><Link to="/place-order">Place Order</Link></li>
                        <li><Link to={`/customer-orders/${demoCustomerId}`}>Order History</Link></li>
                        <li><Link to="/product-management">Product Management</Link></li>
                    </ul>
                </nav>
            )}
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/create-customer" element={<CreateCustomer />} />
                <Route path="/place-order" element={<OrderForm />} />
                <Route path="/customer-orders/:customerId" element={<CustomerOrdersWrapper />} />
                <Route path="/product-management" element={<ProductManagement />} />
            </Routes>
        </div>
    );
};

const CustomerOrdersWrapper: React.FC = () => {
    const { customerId } = useParams<{ customerId: string }>();
    return customerId ? <CustomerOrders customerId={Number(customerId)} /> : null;
};

export default App;