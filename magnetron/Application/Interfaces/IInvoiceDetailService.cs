using magnetron.Domain.Models;

namespace magnetron.Application.Interfaces
{
    public interface IInvoiceDetailService
    {
        IEnumerable<InvoiceDetailDTO> GetAllInvoiceDetails();
        InvoiceDetailDTO GetInvoiceDetailById(int id);
        void CreateInvoiceDetail(InvoiceDetailDTO invoiceDetail);
        void UpdateInvoiceDetail(InvoiceDetailDTO invoiceDetail);
        void DeleteInvoiceDetail(int id);
    }
}
