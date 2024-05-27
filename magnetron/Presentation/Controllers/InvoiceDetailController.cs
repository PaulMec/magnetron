using magnetron.Application.Interfaces;
using magnetron.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace magnetron.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly IInvoiceDetailService _invoiceDetailService;

        public InvoiceDetailController(IInvoiceDetailService invoiceDetailService)
        {
            _invoiceDetailService = invoiceDetailService;
        }

        [HttpGet]
        public IActionResult GetAllInvoiceDetails()
        {
            var details = _invoiceDetailService.GetAllInvoiceDetails();
            return Ok(details);
        }

        [HttpGet("{id}")]
        public IActionResult GetInvoiceDetailById(int id)
        {
            var detail = _invoiceDetailService.GetInvoiceDetailById(id);
            if (detail == null)
            {
                return NotFound();
            }
            return Ok(detail);
        }

        [HttpPost]
        public IActionResult CreateInvoiceDetail([FromBody] InvoiceDetailDTO detail)
        {
            _invoiceDetailService.CreateInvoiceDetail(detail);
            return CreatedAtAction(nameof(GetInvoiceDetailById), new { id = detail.InvoiceDetailId }, detail);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInvoiceDetail(int id, [FromBody] InvoiceDetailDTO detail)
        {
            if (id != detail.InvoiceDetailId)
            {
                return BadRequest();
            }
            _invoiceDetailService.UpdateInvoiceDetail(detail);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInvoiceDetail(int id)
        {
            _invoiceDetailService.DeleteInvoiceDetail(id);
            return NoContent();
        }
    }
}
