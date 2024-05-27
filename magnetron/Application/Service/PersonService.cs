using magnetron.Infrastructure.Interfaces;
using DB.Models.ViewModels;
using System.Collections.Generic;
using magnetron.Application.Interfaces;
using magnetron.Domain.Models;

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
            return _personRepository.GetAll();
        }

        public PersonDTO GetPersonById(int id)
        {
            return _personRepository.GetById(id);
        }

        public void CreatePerson(PersonDTO person)
        {
            _personRepository.Add(person);
        }

        public void UpdatePerson(PersonDTO person)
        {
            _personRepository.Update(person);
        }

        public void DeletePerson(int id)
        {
            _personRepository.Delete(id);
        }

        public IEnumerable<PersonTotalBilledViewModel> GetTotalBilledByPerson()
        {
            return _personRepository.GetTotalBilledByPerson();
        }

        public PersonMostExpensiveProductViewModel GetPersonWhoBoughtMostExpensiveProduct()
        {
            return _personRepository.GetPersonWhoBoughtMostExpensiveProduct();
        }
    }
}
