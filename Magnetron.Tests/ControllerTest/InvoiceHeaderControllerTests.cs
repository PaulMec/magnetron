using magnetron.Application.Interfaces;
using magnetron.Domain.Models;
using magnetron.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Magnetron.Tests.ControllerTest
{
    public class InvoiceHeaderControllerTests
    {
        private readonly Mock<IInvoiceHeaderService> _mockService;
        private readonly InvoiceHeaderController _controller;

        public InvoiceHeaderControllerTests()
        {
            _mockService = new Mock<IInvoiceHeaderService>();
            _controller = new InvoiceHeaderController(_mockService.Object);
        }

        [Fact]
        public void GetAllInvoiceHeaders_ReturnsOkResult_WithListOfInvoiceHeaders()
        {
            // Arrange
            var invoiceHeaders = new List<InvoiceHeaderDTO> { new InvoiceHeaderDTO { InvoiceHeaderId = 1, InvoiceNumber = 1 } };
            _mockService.Setup(service => service.GetAllInvoiceHeaders()).Returns(invoiceHeaders);

            // Act
            var result = _controller.GetAllInvoiceHeaders();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<InvoiceHeaderDTO>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public void GetInvoiceHeaderById_ReturnsNotFound_WhenInvoiceHeaderDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetInvoiceHeaderById(It.IsAny<int>())).Returns((InvoiceHeaderDTO)null);

            // Act
            var result = _controller.GetInvoiceHeaderById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void CreateInvoiceHeader_ReturnsCreatedAtAction_WithCreatedInvoiceHeader()
        {
            // Arrange
            var invoiceHeader = new InvoiceHeaderDTO { InvoiceHeaderId = 1, InvoiceNumber = 1 };

            // Act
            var result = _controller.CreateInvoiceHeader(invoiceHeader);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetInvoiceHeaderById", createdAtActionResult.ActionName);
            Assert.Equal(invoiceHeader.InvoiceHeaderId, ((InvoiceHeaderDTO)createdAtActionResult.Value).InvoiceHeaderId);
        }

        [Fact]
        public void UpdateInvoiceHeader_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var invoiceHeader = new InvoiceHeaderDTO { InvoiceHeaderId = 1, InvoiceNumber = 1 };

            // Act
            var result = _controller.UpdateInvoiceHeader(1, invoiceHeader);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteInvoiceHeader_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange

            // Act
            var result = _controller.DeleteInvoiceHeader(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
