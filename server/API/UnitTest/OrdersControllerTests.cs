using Microsoft.AspNetCore.Mvc;
using Moq;
using Service.Interfaces;
using Service.TransferModels.Requests;
using API.Controllers;
using DataAccess.Models;
using Xunit;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace API.UnitTest
{
    public class OrdersControllerTests
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly OrdersController _controller;

        public OrdersControllerTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            _controller = new OrdersController(_mockOrderService.Object, null);
        }

        [Fact]
        public void PlaceOrder_ReturnsOkResult_WithOrder()
        {
            // Arrange
            var orderDto = new CreateOrderDto
            {
                OrderDate = System.DateTime.Now,
                DeliveryDate = System.DateTime.Now.AddDays(5),
                Status = "Pending",
                TotalAmount = 100.0, // As double
                CustomerId = 1,
                OrderEntries = new List<CreateOrderEntryDto>
                {
                    new CreateOrderEntryDto { Quantity = 2, ProductId = 1 }
                }
            };

            var order = new Order
            {
                OrderDate = orderDto.OrderDate,
                DeliveryDate = orderDto.DeliveryDate,
                Status = orderDto.Status,
                TotalAmount = orderDto.TotalAmount, // As double
                CustomerId = orderDto.CustomerId,
                OrderEntries = new List<OrderEntry>
                {
                    new OrderEntry { Quantity = 2, ProductId = 1 }
                }
            };

            _mockOrderService
                .Setup(s => s.PlaceOrder(It.IsAny<Order>()))
                .Callback<Order>(o => // Simulate the behavior of PlaceOrder method
                {
                    o.OrderDate = order.OrderDate;
                    o.DeliveryDate = order.DeliveryDate;
                    o.Status = order.Status;
                    o.TotalAmount = order.TotalAmount;
                    o.CustomerId = order.CustomerId;
                    o.OrderEntries = order.OrderEntries;
                });

            // Act
            var result = _controller.PlaceOrder(orderDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Order>(okResult.Value);
            Assert.Equal(orderDto.TotalAmount, returnValue.TotalAmount);
        }

        [Fact]
        public void GetOrdersByCustomer_ReturnsOkResult_WithOrders()
        {
            // Arrange
            int customerId = 1;
            var orders = new List<Order>
            {
                new Order { Id = 1, CustomerId = customerId, TotalAmount = 100.0 } // As double
            };

            _mockOrderService.Setup(s => s.GetOrdersByCustomerId(customerId)).Returns(orders);

            // Act
            var result = _controller.GetOrdersByCustomer(customerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Order>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public void UpdateOrderStatus_ReturnsNoContentResult()
        {
            // Arrange
            int orderId = 1;
            string status = "Shipped";

            // Act
            var result = _controller.UpdateOrderStatus(orderId, status);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}