using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IOrderService
    {
        void PlaceOrder(Order order);
        Order GetOrderById(int orderId);
        Order[] GetAllOrders();
    }
}