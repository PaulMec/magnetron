using magnetron.Domain.Models;
using System.Collections.Generic;

namespace magnetron.Infrastructure.Interfaces
{
    public interface IInvoiceDetailRepository
    {
        IEnumerable<InvoiceDetailDTO> GetAll();
        InvoiceDetailDTO GetById(int id);
        void Add(InvoiceDetailDTO invoiceDetail);
        void Update(InvoiceDetailDTO invoiceDetail);
        void Delete(int id);
    }
}
