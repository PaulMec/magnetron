using DB.Context;
using DB.Models;
using magnetron.Domain.Models;
using magnetron.Infrastructure.Interfaces;
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

        public InvoiceDetailDTO GetById(int id)
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

        public void Add(InvoiceDetailDTO detailDto)
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

        public void Update(InvoiceDetailDTO detailDto)
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

        public void Delete(int id)
        {
            var detail = _context.InvoiceDetails.Find(id);
            if (detail != null)
            {
                _context.InvoiceDetails.Remove(detail);
                _context.SaveChanges();
            }
        }
    }
}
