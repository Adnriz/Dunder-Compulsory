using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        
        Order GetOrder(int orderId);
        
        Order[] GetAllOrders();
        
        void UpdateOrder(Order order);
        
        void DeleteOrder(int orderId);
        
        IEnumerable<Order> GetOrdersByCustomerId(int customerId);
        
        void UpdateOrderStatus(int orderId, string status);
    }
}