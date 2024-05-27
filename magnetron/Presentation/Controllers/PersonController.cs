using magnetron.Application.Interfaces;
using magnetron.Domain.Models;
using DB.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            var persons = _personService.GetAllPersons();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public IActionResult GetPersonById(int id)
        {
            var person = _personService.GetPersonById(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public IActionResult CreatePerson([FromBody] PersonDTO person)
        {
            _personService.CreatePerson(person);
            return CreatedAtAction(nameof(GetPersonById), new { id = person.PersonId }, person);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePerson(int id, [FromBody] PersonDTO person)
        {
            if (id != person.PersonId)
            {
                return BadRequest();
            }
            _personService.UpdatePerson(person);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            _personService.DeletePerson(id);
            return NoContent();
        }

        [HttpGet("total-billed")]
        public IActionResult GetTotalBilledByPerson()
        {
            var persons = _personService.GetTotalBilledByPerson();
            return Ok(persons);
        }

        [HttpGet("most-expensive-product")]
        public IActionResult GetPersonWhoBoughtMostExpensiveProduct()
        {
            var person = _personService.GetPersonWhoBoughtMostExpensiveProduct();
            return Ok(person);
        }
    }
}
