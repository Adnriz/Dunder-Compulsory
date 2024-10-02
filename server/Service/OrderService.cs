using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void PlaceOrder(Order order)
        {
            order.OrderDate = DateTime.UtcNow;
            _orderRepository.AddOrder(order);
        }

        public Order GetOrderById(int orderId)
        {
            return _orderRepository.GetOrder(orderId);
        }

        public Order[] GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }
    }
}