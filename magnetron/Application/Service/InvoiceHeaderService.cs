using magnetron.Application.Interfaces;
using magnetron.Domain.Models;
using magnetron.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;

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
            try
            {
                return _invoiceHeaderRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service Layer: An error occurred while retrieving all invoice headers.", ex);
            }
        }

        public InvoiceHeaderDTO GetInvoiceHeaderById(int id)
        {
            try
            {
                return _invoiceHeaderRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Service Layer: An error occurred while retrieving the invoice header with ID {id}.", ex);
            }
        }

        public void CreateInvoiceHeader(InvoiceHeaderDTO invoiceHeader)
        {
            try
            {
                _invoiceHeaderRepository.Add(invoiceHeader);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service Layer: An error occurred while creating the invoice header.", ex);
            }
        }

        public void UpdateInvoiceHeader(InvoiceHeaderDTO invoiceHeader)
        {
            try
            {
                _invoiceHeaderRepository.Update(invoiceHeader);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Service Layer: An error occurred while updating the invoice header with ID {invoiceHeader.InvoiceHeaderId}.", ex);
            }
        }

        public void DeleteInvoiceHeader(int id)
        {
            try
            {
                _invoiceHeaderRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Service Layer: An error occurred while deleting the invoice header with ID {id}.", ex);
            }
        }
    }
}
