using magnetron.Application.Interfaces;
using magnetron.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;

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
            try
            {
                var details = _invoiceDetailService.GetAllInvoiceDetails();
                return Ok(details);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the invoice details.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetInvoiceDetailById(int id)
        {
            try
            {
                var detail = _invoiceDetailService.GetInvoiceDetailById(id);
                if (detail == null)
                {
                    return NotFound(new { message = "Invoice detail not found." });
                }
                return Ok(detail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the invoice detail.", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CreateInvoiceDetail([FromBody] InvoiceDetailDTO detail)
        {
            try
            {
                _invoiceDetailService.CreateInvoiceDetail(detail);
                return CreatedAtAction(nameof(GetInvoiceDetailById), new { id = detail.InvoiceDetailId }, detail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the invoice detail.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInvoiceDetail(int id, [FromBody] InvoiceDetailDTO detail)
        {
            try
            {
                if (id != detail.InvoiceDetailId)
                {
                    return BadRequest(new { message = "Invoice detail ID mismatch." });
                }
                _invoiceDetailService.UpdateInvoiceDetail(detail);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the invoice detail.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInvoiceDetail(int id)
        {
            try
            {
                _invoiceDetailService.DeleteInvoiceDetail(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the invoice detail.", error = ex.Message });
            }
        }
    }
}
