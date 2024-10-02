using Microsoft.AspNetCore.Mvc;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            var orders = _orderRepository.GetAllOrders();
            return Ok(orders);
        }

        
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _orderRepository.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        
        [HttpPost]
        public ActionResult<Order> CreateOrder(Order order)
        {
            _orderRepository.AddOrder(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            var existingOrder = _orderRepository.GetOrder(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            _orderRepository.UpdateOrder(order);
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var existingOrder = _orderRepository.GetOrder(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            _orderRepository.DeleteOrder(id);
            return NoContent();
        }

        
        [HttpGet("customer/{customerId}")]
        public ActionResult<IEnumerable<Order>> GetOrdersByCustomerId(int customerId)
        {
            var orders = _orderRepository.GetOrdersByCustomerId(customerId);
            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        
        [HttpPatch("{id}/status")]
        public IActionResult UpdateOrderStatus(int id, [FromBody] string status)
        {
            var existingOrder = _orderRepository.GetOrder(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            _orderRepository.UpdateOrderStatus(id, status);
            return NoContent();
        }
    }
}