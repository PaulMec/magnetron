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
    public class InvoiceDetailControllerTests
    {
        private readonly Mock<IInvoiceDetailService> _mockService;
        private readonly InvoiceDetailController _controller;

        public InvoiceDetailControllerTests()
        {
            _mockService = new Mock<IInvoiceDetailService>();
            _controller = new InvoiceDetailController(_mockService.Object);
        }

        [Fact]
        public void GetAllInvoiceDetails_ReturnsOkResult_WithListOfInvoiceDetails()
        {
            // Arrange
            var invoiceDetails = new List<InvoiceDetailDTO> { new InvoiceDetailDTO { InvoiceDetailId = 1, LineNumber = 1 } };
            _mockService.Setup(service => service.GetAllInvoiceDetails()).Returns(invoiceDetails);

            // Act
            var result = _controller.GetAllInvoiceDetails();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<InvoiceDetailDTO>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public void GetInvoiceDetailById_ReturnsNotFound_WhenInvoiceDetailDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetInvoiceDetailById(It.IsAny<int>())).Returns((InvoiceDetailDTO)null);

            // Act
            var result = _controller.GetInvoiceDetailById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void CreateInvoiceDetail_ReturnsCreatedAtAction_WithCreatedInvoiceDetail()
        {
            // Arrange
            var invoiceDetail = new InvoiceDetailDTO { InvoiceDetailId = 1, LineNumber = 1 };

            // Act
            var result = _controller.CreateInvoiceDetail(invoiceDetail);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetInvoiceDetailById", createdAtActionResult.ActionName);
            Assert.Equal(invoiceDetail.InvoiceDetailId, ((InvoiceDetailDTO)createdAtActionResult.Value).InvoiceDetailId);
        }

        [Fact]
        public void UpdateInvoiceDetail_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var invoiceDetail = new InvoiceDetailDTO { InvoiceDetailId = 1, LineNumber = 1 };

            // Act
            var result = _controller.UpdateInvoiceDetail(1, invoiceDetail);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteInvoiceDetail_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange

            // Act
            var result = _controller.DeleteInvoiceDetail(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
