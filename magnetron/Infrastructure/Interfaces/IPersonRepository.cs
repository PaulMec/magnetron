using magnetron.Domain.Models;
using DB.Models.ViewModels;
using System.Collections.Generic;

namespace magnetron.Infrastructure.Interfaces
{
    public interface IPersonRepository
    {
        IEnumerable<PersonDTO> GetAll();
        PersonDTO GetById(int id);
        void Add(PersonDTO personDto);
        void Update(PersonDTO personDto);
        void Delete(int id);
        // Métodos para obtener datos calculados desde vistas SQL
        IEnumerable<PersonTotalBilledViewModel> GetTotalBilledByPerson();
        PersonMostExpensiveProductViewModel GetPersonWhoBoughtMostExpensiveProduct();
    }
}
