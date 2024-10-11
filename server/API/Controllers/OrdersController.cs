using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.TransferModels.Requests;
using DataAccess.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderService orderService, IOrderRepository orderRepository)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
        }
        
        [HttpPost]
        public IActionResult PlaceOrder(CreateOrderDto orderDto)
        {
            var order = new Order
            {
                OrderDate = orderDto.OrderDate,
                DeliveryDate = orderDto.DeliveryDate,
                Status = orderDto.Status,
                TotalAmount = orderDto.TotalAmount,
                CustomerId = orderDto.CustomerId,
                OrderEntries = orderDto.OrderEntries.Select(e => new OrderEntry
                {
                    Quantity = e.Quantity,
                    ProductId = e.ProductId
                }).ToList()
            };

            _orderService.PlaceOrder(order);
            return Ok(order);
        }
        
        [HttpGet("customer/{customerId}")]
        public IActionResult GetOrdersByCustomer(int customerId)
        {
            var orders = _orderService.GetOrdersByCustomerId(customerId);
            return Ok(orders);
        }
        
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }
        
        [HttpPut("{orderId}/status")]
        public IActionResult UpdateOrderStatus(int orderId, [FromBody] string status)
        {
            _orderService.UpdateOrderStatus(orderId, status);
            return NoContent();
        }
    }
}