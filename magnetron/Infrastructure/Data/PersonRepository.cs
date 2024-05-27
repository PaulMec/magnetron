using DB.Context;
using DB.Models;
using DB.Models.ViewModels;
using magnetron.Domain.Models;
using magnetron.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace magnetron.Infrastructure.Data
{
    public class PersonRepository : IPersonRepository
    {
        private readonly FacturacionContext _context;

        public PersonRepository(FacturacionContext context)
        {
            _context = context;
        }

        public IEnumerable<PersonDTO> GetAll()
        {
            return _context.Persons
                .Select(p => new PersonDTO
                {
                    PersonId = p.PersonId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    DocumentType = p.DocumentType,
                    DocumentNumber = p.DocumentNumber
                }).ToList();
        }

        public PersonDTO GetById(int id)
        {
            var person = _context.Persons.Find(id);
            if (person == null) return null;
            return new PersonDTO
            {
                PersonId = person.PersonId,
                FirstName = person.FirstName,
                LastName = person.LastName,
                DocumentType = person.DocumentType,
                DocumentNumber = person.DocumentNumber
            };
        }

        public void Add(PersonDTO personDto)
        {
            var person = new Person
            {
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                DocumentType = personDto.DocumentType,
                DocumentNumber = personDto.DocumentNumber
            };
            _context.Persons.Add(person);
            _context.SaveChanges();
        }

        public void Update(PersonDTO personDto)
        {
            var person = _context.Persons.Find(personDto.PersonId);
            if (person != null)
            {
                person.FirstName = personDto.FirstName;
                person.LastName = personDto.LastName;
                person.DocumentType = personDto.DocumentType;
                person.DocumentNumber = personDto.DocumentNumber;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var person = _context.Persons.Find(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                _context.SaveChanges();
            }
        }

        // Métodos para obtener datos calculados desde vistas SQL
        public IEnumerable<PersonTotalBilledViewModel> GetTotalBilledByPerson()
        {
            return _context.PersonTotalBilledViewModels
                .FromSqlRaw("SELECT * FROM TotalFacturadoPorPersona")
                .ToList();
        }

        public PersonMostExpensiveProductViewModel GetPersonWhoBoughtMostExpensiveProduct()
        {
            return _context.PersonMostExpensiveProductViewModels
                .FromSqlRaw("SELECT TOP 1 * FROM PersonaProductoMasCaro")
                .FirstOrDefault();
        }
    }
}
