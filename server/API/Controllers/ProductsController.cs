using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.TransferModels.Requests;
using DataAccess.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        
        [HttpPost]
        public IActionResult CreateProduct(CreatePaperDto paperDto)
        {
            var paper = new Paper
            {
                Name = paperDto.Name,
                Discontinued = paperDto.Discontinued,
                Stock = paperDto.Stock,
                Price = paperDto.Price
            };

            _productService.AddProduct(paper);
            return Ok(paper);
        }

        
        [HttpPut("{productId}")]
        public IActionResult RestockProduct(int productId, [FromBody] int quantity)
        {
            _productService.RestockProduct(productId, quantity);
            return NoContent();
        }

        
        [HttpDelete("{productId}")]
        public IActionResult DiscontinueProduct(int productId)
        {
            _productService.DiscontinueProduct(productId);
            return NoContent();
        }
    }
}