using magnetron.Application.Interfaces;
using magnetron.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;

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
            try
            {
                var products = _productService.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the products.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                var product = _productService.GetProductById(id);
                if (product == null)
                {
                    return NotFound(new { message = "Product not found." });
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the product.", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDTO product)
        {
            try
            {
                _productService.CreateProduct(product);
                return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the product.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDTO product)
        {
            try
            {
                if (id != product.ProductId)
                {
                    return BadRequest(new { message = "Product ID mismatch." });
                }
                _productService.UpdateProduct(product);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the product.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the product.", error = ex.Message });
            }
        }

        [HttpGet("sold")]
        public IActionResult GetProductsByQuantitySold()
        {
            try
            {
                var products = _productService.GetProductsByQuantitySold();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the products by quantity sold.", error = ex.Message });
            }
        }

        [HttpGet("profit")]
        public IActionResult GetProductsProfit()
        {
            try
            {
                var products = _productService.GetProductsProfit();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the products by profit.", error = ex.Message });
            }
        }

        [HttpGet("profit-margin")]
        public IActionResult GetProductsProfitMargin()
        {
            try
            {
                var products = _productService.GetProductsProfitMargin();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the products by profit margin.", error = ex.Message });
            }
        }
    }
}
