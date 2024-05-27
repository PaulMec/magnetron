using magnetron.Application.Interfaces;
using magnetron.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;

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
            try
            {
                var headers = _invoiceHeaderService.GetAllInvoiceHeaders();
                return Ok(headers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the invoice headers.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetInvoiceHeaderById(int id)
        {
            try
            {
                var header = _invoiceHeaderService.GetInvoiceHeaderById(id);
                if (header == null)
                {
                    return NotFound(new { message = "Invoice header not found." });
                }
                return Ok(header);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the invoice header.", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CreateInvoiceHeader([FromBody] InvoiceHeaderDTO header)
        {
            try
            {
                _invoiceHeaderService.CreateInvoiceHeader(header);
                return CreatedAtAction(nameof(GetInvoiceHeaderById), new { id = header.InvoiceHeaderId }, header);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the invoice header.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInvoiceHeader(int id, [FromBody] InvoiceHeaderDTO header)
        {
            try
            {
                if (id != header.InvoiceHeaderId)
                {
                    return BadRequest(new { message = "Invoice header ID mismatch." });
                }
                _invoiceHeaderService.UpdateInvoiceHeader(header);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the invoice header.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInvoiceHeader(int id)
        {
            try
            {
                _invoiceHeaderService.DeleteInvoiceHeader(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the invoice header.", error = ex.Message });
            }
        }
    }
}
