using DataAccess.Interfaces;
using DataAccess.Models;
using System.Collections.Generic;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }
        
        public void PlaceOrder(Order order)
        {
            _orderRepository.AddOrder(order);
        }

        public Order GetOrderById(int orderId)
        {
            return _orderRepository.GetOrder(orderId);
        }

        public void UpdateOrderStatus(int orderId, string status)
        {
            var order = _orderRepository.GetOrder(orderId);
            if (order != null)
            {
                order.Status = status;
                _orderRepository.UpdateOrder(order);
            }
        }

        public IEnumerable<Order> GetOrdersByCustomerId(int customerId)
        {
            return _orderRepository.GetOrdersByCustomerId(customerId);
        }
    }
}