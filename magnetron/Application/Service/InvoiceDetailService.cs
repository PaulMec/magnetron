using magnetron.Application.Interfaces;
using magnetron.Domain.Models;
using magnetron.Infrastructure.Interfaces;

namespace magnetron.Application.Services
{
    public class InvoiceDetailService : IInvoiceDetailService
    {
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;

        public InvoiceDetailService(IInvoiceDetailRepository invoiceDetailRepository)
        {
            _invoiceDetailRepository = invoiceDetailRepository;
        }

        public IEnumerable<InvoiceDetailDTO> GetAllInvoiceDetails()
        {
            return _invoiceDetailRepository.GetAll();
        }

        public InvoiceDetailDTO GetInvoiceDetailById(int id)
        {
            return _invoiceDetailRepository.GetById(id);
        }

        public void CreateInvoiceDetail(InvoiceDetailDTO invoiceDetail)
        {
            _invoiceDetailRepository.Add(invoiceDetail);
        }

        public void UpdateInvoiceDetail(InvoiceDetailDTO invoiceDetail)
        {
            _invoiceDetailRepository.Update(invoiceDetail);
        }

        public void DeleteInvoiceDetail(int id)
        {
            _invoiceDetailRepository.Delete(id);
        }
    }
}
