using Microsoft.AspNetCore.Mvc;
using Moq;
using DataAccess;
using DataAccess.Models;
using API.Controllers;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.UnitTest
{
    public class CustomerControllerTests
    {
        private readonly Mock<WebShopContext> _mockContext;
        private readonly CustomerController _controller;

        public CustomerControllerTests()
        {
            var options = new DbContextOptionsBuilder<WebShopContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new WebShopContext(options);

            // Add mock DbSet
            var mockDbSet = new Mock<DbSet<Customer>>();
            context.Customers = mockDbSet.Object;

            _mockContext = new Mock<WebShopContext>(options);
            _mockContext.Setup(c => c.Customers).Returns(mockDbSet.Object); 
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            _controller = new CustomerController(context);
        }

        [Fact]
        public async Task CreateCustomer_ReturnsCreatedAtActionResult_WithCustomer()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "John Doe" };

            // Act
            var result = await _controller.CreateCustomer(customer);

            // Assert
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Customer>(createdAtResult.Value);
            Assert.Equal(customer.Name, returnValue.Name);
        }

        [Fact]
        public async Task GetCustomer_ReturnsOkResult_WithCustomer()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "John Doe" };
            _mockContext.Setup(c => c.Customers.FindAsync(It.IsAny<int>())).ReturnsAsync(customer);

            // Act
            var result = await _controller.GetCustomer(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal(customer.Name, returnValue.Name);
        }
    }
}