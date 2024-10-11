using Microsoft.AspNetCore.Mvc;
using Moq;
using Service.Interfaces;
using Service.TransferModels.Requests;
using API.Controllers;
using DataAccess.Models;
using Xunit;
using System.Threading.Tasks;

namespace API.UnitTest
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductsController(_mockProductService.Object);
        }

        [Fact]
        public void CreateProduct_ReturnsOkResult_WithCreatedProduct()
        {
            // Arrange
            var productDto = new CreatePaperDto { Name = "Paper1", Discontinued = false, Stock = 100, Price = 10.0 };

            // Act
            var result = _controller.CreateProduct(productDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Paper>(okResult.Value);
            Assert.Equal(productDto.Name, returnValue.Name);
        }

        [Fact]
        public void RestockProduct_ReturnsNoContentResult()
        {
            // Arrange
            int productId = 1;
            int quantity = 50;

            // Act
            var result = _controller.RestockProduct(productId, quantity);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DiscontinueProduct_ReturnsNoContentResult()
        {
            // Arrange
            int productId = 1;

            // Act
            var result = _controller.DiscontinueProduct(productId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}