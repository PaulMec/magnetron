using magnetron.Application.Interfaces;
using magnetron.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace magnetron.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceHeaderController : ControllerBase
    {
        private readonly IInvoiceHeaderService _invoiceHeaderService;

        public InvoiceHeaderController(IInvoiceHeaderService invoiceHeaderService)
        {
            _invoiceHeaderService = invoiceHeaderService;
        }

        [HttpGet]
        public IActionResult GetAllInvoiceHeaders()
        {
            var headers = _invoiceHeaderService.GetAllInvoiceHeaders();
            return Ok(headers);
        }

        [HttpGet("{id}")]
        public IActionResult GetInvoiceHeaderById(int id)
        {
            var header = _invoiceHeaderService.GetInvoiceHeaderById(id);
            if (header == null)
            {
                return NotFound();
            }
            return Ok(header);
        }

        [HttpPost]
        public IActionResult CreateInvoiceHeader([FromBody] InvoiceHeaderDTO header)
        {
            _invoiceHeaderService.CreateInvoiceHeader(header);
            return CreatedAtAction(nameof(GetInvoiceHeaderById), new { id = header.InvoiceHeaderId }, header);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInvoiceHeader(int id, [FromBody] InvoiceHeaderDTO header)
        {
            if (id != header.InvoiceHeaderId)
            {
                return BadRequest();
            }
            _invoiceHeaderService.UpdateInvoiceHeader(header);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInvoiceHeader(int id)
        {
            _invoiceHeaderService.DeleteInvoiceHeader(id);
            return NoContent();
        }
    }
}
