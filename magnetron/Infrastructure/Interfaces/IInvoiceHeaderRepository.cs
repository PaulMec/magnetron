using magnetron.Domain.Models;
using System.Collections.Generic;

namespace magnetron.Infrastructure.Interfaces
{
    public interface IInvoiceHeaderRepository
    {
        IEnumerable<InvoiceHeaderDTO> GetAll();
        InvoiceHeaderDTO GetById(int id);
        void Add(InvoiceHeaderDTO invoiceHeader);
        void Update(InvoiceHeaderDTO invoiceHeader);
        void Delete(int id);
    }
}
