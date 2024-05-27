using DB.Context;
using DB.Models;
using magnetron.Domain.Models;
using magnetron.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace magnetron.Infrastructure.Data
{
    public class InvoiceHeaderRepository : IInvoiceHeaderRepository
    {
        private readonly FacturacionContext _context;

        public InvoiceHeaderRepository(FacturacionContext context)
        {
            _context = context;
        }

        public IEnumerable<InvoiceHeaderDTO> GetAll()
        {
            return _context.InvoiceHeaders
                .Select(h => new InvoiceHeaderDTO
                {
                    InvoiceHeaderId = h.InvoiceHeaderId,
                    InvoiceNumber = h.InvoiceNumber,
                    InvoiceDate = h.InvoiceDate,
                    PersonId = h.PersonId
                }).ToList();
        }

        public InvoiceHeaderDTO GetById(int id)
        {
            var header = _context.InvoiceHeaders.Find(id);
            if (header == null) return null;
            return new InvoiceHeaderDTO
            {
                InvoiceHeaderId = header.InvoiceHeaderId,
                InvoiceNumber = header.InvoiceNumber,
                InvoiceDate = header.InvoiceDate,
                PersonId = header.PersonId
            };
        }

        public void Add(InvoiceHeaderDTO headerDto)
        {
            var header = new InvoiceHeader
            {
                InvoiceNumber = headerDto.InvoiceNumber,
                InvoiceDate = headerDto.InvoiceDate,
                PersonId = headerDto.PersonId
            };
            _context.InvoiceHeaders.Add(header);
            _context.SaveChanges();
        }

        public void Update(InvoiceHeaderDTO headerDto)
        {
            var header = _context.InvoiceHeaders.Find(headerDto.InvoiceHeaderId);
            if (header != null)
            {
                header.InvoiceNumber = headerDto.InvoiceNumber;
                header.InvoiceDate = headerDto.InvoiceDate;
                header.PersonId = headerDto.PersonId;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var header = _context.InvoiceHeaders.Find(id);
            if (header != null)
            {
                _context.InvoiceHeaders.Remove(header);
                _context.SaveChanges();
            }
        }
    }
}
