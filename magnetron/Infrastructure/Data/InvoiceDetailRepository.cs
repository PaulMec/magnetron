using DB.Context;
using DB.Models;
using magnetron.Domain.Models;
using magnetron.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace magnetron.Infrastructure.Data
{
    public class InvoiceDetailRepository : IInvoiceDetailRepository
    {
        private readonly FacturacionContext _context;

        public InvoiceDetailRepository(FacturacionContext context)
        {
            _context = context;
        }

        public IEnumerable<InvoiceDetailDTO> GetAll()
        {
            try
            {
                return _context.InvoiceDetails
                    .Select(d => new InvoiceDetailDTO
                    {
                        InvoiceDetailId = d.InvoiceDetailId,
                        LineNumber = d.LineNumber,
                        Quantity = d.Quantity,
                        ProductId = d.ProductId,
                        InvoiceHeaderId = d.InvoiceHeaderId
                    }).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Repository Layer: An error occurred while retrieving all invoice details.", ex);
            }
        }

        public InvoiceDetailDTO GetById(int id)
        {
            try
            {
                var detail = _context.InvoiceDetails.Find(id);
                if (detail == null) return null;
                return new InvoiceDetailDTO
                {
                    InvoiceDetailId = detail.InvoiceDetailId,
                    LineNumber = detail.LineNumber,
                    Quantity = detail.Quantity,
                    ProductId = detail.ProductId,
                    InvoiceHeaderId = detail.InvoiceHeaderId
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Repository Layer: An error occurred while retrieving the invoice detail with ID {id}.", ex);
            }
        }

        public void Add(InvoiceDetailDTO detailDto)
        {
            try
            {
                var detail = new InvoiceDetail
                {
                    LineNumber = detailDto.LineNumber,
                    Quantity = detailDto.Quantity,
                    ProductId = detailDto.ProductId,
                    InvoiceHeaderId = detailDto.InvoiceHeaderId
                };
                _context.InvoiceDetails.Add(detail);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Repository Layer: An error occurred while adding a new invoice detail.", ex);
            }
        }

        public void Update(InvoiceDetailDTO detailDto)
        {
            try
            {
                var detail = _context.InvoiceDetails.Find(detailDto.InvoiceDetailId);
                if (detail != null)
                {
                    detail.LineNumber = detailDto.LineNumber;
                    detail.Quantity = detailDto.Quantity;
                    detail.ProductId = detailDto.ProductId;
                    detail.InvoiceHeaderId = detailDto.InvoiceHeaderId;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Repository Layer: An error occurred while updating the invoice detail with ID {detailDto.InvoiceDetailId}.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var detail = _context.InvoiceDetails.Find(id);
                if (detail != null)
                {
                    _context.InvoiceDetails.Remove(detail);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Repository Layer: An error occurred while deleting the invoice detail with ID {id}.", ex);
            }
        }
    }
}
