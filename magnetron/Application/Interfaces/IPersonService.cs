using magnetron.Domain.Models;
using DB.Models.ViewModels;
using System.Collections.Generic;

namespace magnetron.Application.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<PersonDTO> GetAllPersons();
        PersonDTO GetPersonById(int id);
        void CreatePerson(PersonDTO person);
        void UpdatePerson(PersonDTO person);
        void DeletePerson(int id);
        IEnumerable<PersonTotalBilledViewModel> GetTotalBilledByPerson();
        PersonMostExpensiveProductViewModel GetPersonWhoBoughtMostExpensiveProduct();
    }
}
