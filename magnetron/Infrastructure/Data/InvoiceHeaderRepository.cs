using DB.Context;
using DB.Models;
using magnetron.Domain.Models;
using magnetron.Infrastructure.Interfaces;
using System;
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
            try
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
            catch (Exception ex)
            {
                throw new ApplicationException("Repository Layer: An error occurred while retrieving all invoice headers.", ex);
            }
        }

        public InvoiceHeaderDTO GetById(int id)
        {
            try
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
            catch (Exception ex)
            {
                throw new ApplicationException($"Repository Layer: An error occurred while retrieving the invoice header with ID {id}.", ex);
            }
        }

        public void Add(InvoiceHeaderDTO headerDto)
        {
            try
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
            catch (Exception ex)
            {
                throw new ApplicationException("Repository Layer: An error occurred while adding a new invoice header.", ex);
            }
        }

        public void Update(InvoiceHeaderDTO headerDto)
        {
            try
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
            catch (Exception ex)
            {
                throw new ApplicationException($"Repository Layer: An error occurred while updating the invoice header with ID {headerDto.InvoiceHeaderId}.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var header = _context.InvoiceHeaders.Find(id);
                if (header != null)
                {
                    _context.InvoiceHeaders.Remove(header);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Repository Layer: An error occurred while deleting the invoice header with ID {id}.", ex);
            }
        }
    }
}
