using magnetron.Application.Interfaces;
using magnetron.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace magnetron.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public IActionResult GetAllPersons()
        {
            try
            {
                var persons = _personService.GetAllPersons();
                return Ok(persons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the persons.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPersonById(int id)
        {
            try
            {
                var person = _personService.GetPersonById(id);
                if (person == null)
                {
                    return NotFound(new { message = "Person not found." });
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the person.", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CreatePerson([FromBody] PersonDTO person)
        {
            try
            {
                _personService.CreatePerson(person);
                return CreatedAtAction(nameof(GetPersonById), new { id = person.PersonId }, person);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the person.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePerson(int id, [FromBody] PersonDTO person)
        {
            try
            {
                if (id != person.PersonId)
                {
                    return BadRequest(new { message = "Person ID mismatch." });
                }
                _personService.UpdatePerson(person);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the person.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            try
            {
                _personService.DeletePerson(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the person.", error = ex.Message });
            }
        }

        [HttpGet("total-billed")]
        public IActionResult GetTotalBilledByPerson()
        {
            try
            {
                var persons = _personService.GetTotalBilledByPerson();
                return Ok(persons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the total billed by person.", error = ex.Message });
            }
        }

        [HttpGet("most-expensive-product")]
        public IActionResult GetPersonWhoBoughtMostExpensiveProduct()
        {
            try
            {
                var person = _personService.GetPersonWhoBoughtMostExpensiveProduct();
                return Ok(person);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the person who bought the most expensive product.", error = ex.Message });
            }
        }
    }
}
