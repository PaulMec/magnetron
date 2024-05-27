using magnetron.Domain.Models;

namespace magnetron.Application.Interfaces
{
    public interface IInvoiceHeaderService
    {
        IEnumerable<InvoiceHeaderDTO> GetAllInvoiceHeaders();
        InvoiceHeaderDTO GetInvoiceHeaderById(int id);
        void CreateInvoiceHeader(InvoiceHeaderDTO invoiceHeader);
        void UpdateInvoiceHeader(InvoiceHeaderDTO invoiceHeader);
        void DeleteInvoiceHeader(int id);
    }
}
