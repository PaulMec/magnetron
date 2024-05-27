using magnetron.Application.Interfaces;
using magnetron.Domain.Models;
using DB.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace magnetron.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDTO product)
        {
            _productService.CreateProduct(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDTO product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            _productService.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return NoContent();
        }

        [HttpGet("sold")]
        public IActionResult GetProductsByQuantitySold()
        {
            var products = _productService.GetProductsByQuantitySold();
            return Ok(products);
        }

        [HttpGet("profit")]
        public IActionResult GetProductsProfit()
        {
            var products = _productService.GetProductsProfit();
            return Ok(products);
        }

        [HttpGet("profit-margin")]
        public IActionResult GetProductsProfitMargin()
        {
            var products = _productService.GetProductsProfitMargin();
            return Ok(products);
        }
    }
}
