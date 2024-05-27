using DB.Models.ViewModels;
using magnetron.Application.Interfaces;
using magnetron.Domain.Models;
using magnetron.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Magnetron.Tests.ControllerTest
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockService;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductController(_mockService.Object);
        }

        [Fact]
        public void GetAllProducts_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var products = new List<ProductDTO> { new ProductDTO { ProductId = 1, Description = "Test Product" } };
            _mockService.Setup(service => service.GetAllProducts()).Returns(products);

            // Act
            var result = _controller.GetAllProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ProductDTO>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public void GetProductById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetProductById(It.IsAny<int>())).Returns((ProductDTO)null);

            // Act
            var result = _controller.GetProductById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void CreateProduct_ReturnsCreatedAtAction_WithCreatedProduct()
        {
            // Arrange
            var product = new ProductDTO { ProductId = 1, Description = "Test Product" };

            // Act
            var result = _controller.CreateProduct(product);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetProductById", createdAtActionResult.ActionName);
            Assert.Equal(product.ProductId, ((ProductDTO)createdAtActionResult.Value).ProductId);
        }

        [Fact]
        public void UpdateProduct_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var product = new ProductDTO { ProductId = 1, Description = "Test Product" };

            // Act
            var result = _controller.UpdateProduct(1, product);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteProduct_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange

            // Act
            var result = _controller.DeleteProduct(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void GetProductsByQuantitySold_ReturnsOkResult_WithListOfSoldProducts()
        {
            // Arrange
            var soldProducts = new List<ProductSoldViewModel>
            {
                new ProductSoldViewModel { ProductId = 1, Description = "Product 1", QuantitySold = 100 },
                new ProductSoldViewModel { ProductId = 2, Description = "Product 2", QuantitySold = 50 }
            };
            _mockService.Setup(service => service.GetProductsByQuantitySold()).Returns(soldProducts);

            // Act
            var result = _controller.GetProductsByQuantitySold();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnProducts = Assert.IsType<List<ProductSoldViewModel>>(okResult.Value);
            Assert.Equal(2, returnProducts.Count);
        }

        [Fact]
        public void GetProductsProfit_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var products = new List<ProductProfitViewModel> { new ProductProfitViewModel { ProductId = 1, Description = "Test Product", Profit = 100 } };
            _mockService.Setup(service => service.GetProductsProfit()).Returns(products);

            // Act
            var result = _controller.GetProductsProfit();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ProductProfitViewModel>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public void GetProductsProfitMargin_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var products = new List<ProductProfitMarginViewModel> { new ProductProfitMarginViewModel { ProductId = 1, Description = "Test Product", ProfitMargin = 0.5M } };
            _mockService.Setup(service => service.GetProductsProfitMargin()).Returns(products);

            // Act
            var result = _controller.GetProductsProfitMargin();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ProductProfitMarginViewModel>>(okResult.Value);
            Assert.Single(returnValue);
        }
    }
}
