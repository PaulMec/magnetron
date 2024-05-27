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
    public class PersonControllerTests
    {
        private readonly Mock<IPersonService> _mockService;
        private readonly PersonController _controller;

        public PersonControllerTests()
        {
            _mockService = new Mock<IPersonService>();
            _controller = new PersonController(_mockService.Object);
        }

        [Fact]
        public void GetAllPersons_ReturnsOkResult_WithListOfPersons()
        {
            // Arrange
            var persons = new List<PersonDTO> { new PersonDTO { PersonId = 1, FirstName = "John", LastName = "Doe" } };
            _mockService.Setup(service => service.GetAllPersons()).Returns(persons);

            // Act
            var result = _controller.GetAllPersons();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<PersonDTO>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public void GetPersonById_ReturnsNotFound_WhenPersonDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetPersonById(It.IsAny<int>())).Returns((PersonDTO)null);

            // Act
            var result = _controller.GetPersonById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void CreatePerson_ReturnsCreatedAtAction_WithCreatedPerson()
        {
            // Arrange
            var person = new PersonDTO { PersonId = 1, FirstName = "John", LastName = "Doe" };

            // Act
            var result = _controller.CreatePerson(person);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetPersonById", createdAtActionResult.ActionName);
            Assert.Equal(person.PersonId, ((PersonDTO)createdAtActionResult.Value).PersonId);
        }

        [Fact]
        public void UpdatePerson_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var person = new PersonDTO { PersonId = 1, FirstName = "John", LastName = "Doe" };

            // Act
            var result = _controller.UpdatePerson(1, person);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeletePerson_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange

            // Act
            var result = _controller.DeletePerson(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void GetTotalBilledByPerson_ReturnsOkResult_WithListOfBilledPersons()
        {
            // Arrange
            var billedPersons = new List<PersonTotalBilledViewModel>
            {
                new PersonTotalBilledViewModel { PersonId = 1, FirstName = "John", LastName = "Doe", TotalBilled = 1000 },
                new PersonTotalBilledViewModel { PersonId = 2, FirstName = "Jane", LastName = "Smith", TotalBilled = 500 }
            };
            _mockService.Setup(service => service.GetTotalBilledByPerson()).Returns(billedPersons);

            // Act
            var result = _controller.GetTotalBilledByPerson();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnPersons = Assert.IsType<List<PersonTotalBilledViewModel>>(okResult.Value);
            Assert.Equal(2, returnPersons.Count);
        }

        [Fact]
        public void GetPersonWhoBoughtMostExpensiveProduct_ReturnsOkResult_WithPerson()
        {
            // Arrange
            var person = new PersonMostExpensiveProductViewModel { PersonId = 1, FirstName = "John", LastName = "Doe", Description = "Expensive Product", Price = 1000 };
            _mockService.Setup(service => service.GetPersonWhoBoughtMostExpensiveProduct()).Returns(person);

            // Act
            var result = _controller.GetPersonWhoBoughtMostExpensiveProduct();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PersonMostExpensiveProductViewModel>(okResult.Value);
            Assert.Equal(person.PersonId, returnValue.PersonId);
            Assert.Equal("Expensive Product", returnValue.Description);
            Assert.Equal(1000, returnValue.Price);
        }
    }
}
