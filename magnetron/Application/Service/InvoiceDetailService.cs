using magnetron.Application.Interfaces;
using magnetron.Domain.Models;
using magnetron.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;

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
            try
            {
                return _invoiceDetailRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service Layer: An error occurred while retrieving all invoice details.", ex);
            }
        }

        public InvoiceDetailDTO GetInvoiceDetailById(int id)
        {
            try
            {
                return _invoiceDetailRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Service Layer: An error occurred while retrieving the invoice detail with ID {id}.", ex);
            }
        }

        public void CreateInvoiceDetail(InvoiceDetailDTO invoiceDetail)
        {
            try
            {
                _invoiceDetailRepository.Add(invoiceDetail);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service Layer: An error occurred while creating the invoice detail.", ex);
            }
        }

        public void UpdateInvoiceDetail(InvoiceDetailDTO invoiceDetail)
        {
            try
            {
                _invoiceDetailRepository.Update(invoiceDetail);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Service Layer: An error occurred while updating the invoice detail with ID {invoiceDetail.InvoiceDetailId}.", ex);
            }
        }

        public void DeleteInvoiceDetail(int id)
        {
            try
            {
                _invoiceDetailRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Service Layer: An error occurred while deleting the invoice detail with ID {id}.", ex);
            }
        }
    }
}
