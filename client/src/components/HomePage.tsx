import React from 'react';
import { Link } from 'react-router-dom';
import MCParkourImage from '../pictures/MCParkour.jpg';

const Home: React.FC = () => {
    return (
        <div className="home-container">
            <h2>Welcome to Dunder Mifflin...</h2>
            <img src={MCParkourImage} alt="MC Parkour" />
            <div className="home-buttons">
                <Link to="/place-order">
                    <button>Place Order</button>
                </Link>
                <Link to="/customer-orders/1"> {/* Example customer ID */}
                    <button>Order History</button>
                </Link>
                <Link to="/product-management">
                    <button>Product Management</button>
                </Link>
            </div>
        </div>
    );
};

export default Home;