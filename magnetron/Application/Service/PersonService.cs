using magnetron.Infrastructure.Interfaces;
using DB.Models.ViewModels;
using System.Collections.Generic;
using magnetron.Application.Interfaces;
using magnetron.Domain.Models;
using System;

namespace magnetron.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IEnumerable<PersonDTO> GetAllPersons()
        {
            try
            {
                return _personRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service Layer: An error occurred while retrieving all persons.", ex);
            }
        }

        public PersonDTO GetPersonById(int id)
        {
            try
            {
                return _personRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Service Layer: An error occurred while retrieving the person with ID {id}.", ex);
            }
        }

        public void CreatePerson(PersonDTO person)
        {
            try
            {
                _personRepository.Add(person);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service Layer: An error occurred while creating the person.", ex);
            }
        }

        public void UpdatePerson(PersonDTO person)
        {
            try
            {
                _personRepository.Update(person);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Service Layer: An error occurred while updating the person with ID {person.PersonId}.", ex);
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                _personRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Service Layer: An error occurred while deleting the person with ID {id}.", ex);
            }
        }

        public IEnumerable<PersonTotalBilledViewModel> GetTotalBilledByPerson()
        {
            try
            {
                return _personRepository.GetTotalBilledByPerson();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service Layer: An error occurred while retrieving the total billed by person.", ex);
            }
        }

        public PersonMostExpensiveProductViewModel GetPersonWhoBoughtMostExpensiveProduct()
        {
            try
            {
                return _personRepository.GetPersonWhoBoughtMostExpensiveProduct();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service Layer: An error occurred while retrieving the person who bought the most expensive product.", ex);
            }
        }
    }
}
