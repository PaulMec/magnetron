using magnetron.Application.Interfaces;
using magnetron.Domain.Models;
using magnetron.Infrastructure.Interfaces;

namespace magnetron.Application.Service
{
    public class InvoiceHeaderService : IInvoiceHeaderService
    {
        private readonly IInvoiceHeaderRepository _invoiceHeaderRepository;

        public InvoiceHeaderService(IInvoiceHeaderRepository invoiceHeaderRepository)
        {
            _invoiceHeaderRepository = invoiceHeaderRepository;
        }

        public IEnumerable<InvoiceHeaderDTO> GetAllInvoiceHeaders()
        {
            return _invoiceHeaderRepository.GetAll();
        }

        public InvoiceHeaderDTO GetInvoiceHeaderById(int id)
        {
            return _invoiceHeaderRepository.GetById(id);
        }

        public void CreateInvoiceHeader(InvoiceHeaderDTO invoiceHeader)
        {
            _invoiceHeaderRepository.Add(invoiceHeader);
        }

        public void UpdateInvoiceHeader(InvoiceHeaderDTO invoiceHeader)
        {
            _invoiceHeaderRepository.Update(invoiceHeader);
        }

        public void DeleteInvoiceHeader(int id)
        {
            _invoiceHeaderRepository.Delete(id);
        }
    }
}
