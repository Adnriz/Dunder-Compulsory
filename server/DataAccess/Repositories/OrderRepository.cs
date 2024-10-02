using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WebShopContext _context;

        public OrderRepository(WebShopContext context)
        {
            _context = context;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public Order GetOrder(int orderId)
        {
            return _context.Orders.Include(o => o.OrderEntries).FirstOrDefault(o => o.Id == orderId);
        }

        public Order[] GetAllOrders()
        {
            return _context.Orders.Include(o => o.OrderEntries).ToArray();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Order> GetOrdersByCustomerId(int customerId)
        {
            return _context.Orders.Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderEntries)
                .ToList();
        }

        public void UpdateOrderStatus(int orderId, string status)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.Status = status;
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
        }
    }
}