using DB.Context;
using DB.Models;
using DB.Models.ViewModels;
using magnetron.Domain.Models;
using magnetron.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
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
            try
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
            catch (Exception ex)
            {
                throw new ApplicationException("Repository Layer: An error occurred while retrieving all persons.", ex);
            }
        }

        public PersonDTO GetById(int id)
        {
            try
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
            catch (Exception ex)
            {
                throw new ApplicationException($"Repository Layer: An error occurred while retrieving the person with ID {id}.", ex);
            }
        }

        public void Add(PersonDTO personDto)
        {
            try
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
            catch (Exception ex)
            {
                throw new ApplicationException("Repository Layer: An error occurred while adding a new person.", ex);
            }
        }

        public void Update(PersonDTO personDto)
        {
            try
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
            catch (Exception ex)
            {
                throw new ApplicationException($"Repository Layer: An error occurred while updating the person with ID {personDto.PersonId}.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var person = _context.Persons.Find(id);
                if (person != null)
                {
                    _context.Persons.Remove(person);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Repository Layer: An error occurred while deleting the person with ID {id}.", ex);
            }
        }

        // Métodos para obtener datos calculados desde vistas SQL
        public IEnumerable<PersonTotalBilledViewModel> GetTotalBilledByPerson()
        {
            try
            {
                return _context.PersonTotalBilledViewModels
                    .FromSqlRaw("SELECT * FROM TotalFacturadoPorPersona")
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Repository Layer: An error occurred while retrieving total billed by person.", ex);
            }
        }

        public PersonMostExpensiveProductViewModel GetPersonWhoBoughtMostExpensiveProduct()
        {
            try
            {
                return _context.PersonMostExpensiveProductViewModels
                    .FromSqlRaw("SELECT TOP 1 * FROM PersonaProductoMasCaro")
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Repository Layer: An error occurred while retrieving the person who bought the most expensive product.", ex);
            }
        }
    }
}
