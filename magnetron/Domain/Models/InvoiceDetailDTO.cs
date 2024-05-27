namespace magnetron.Domain.Models
{
    public class InvoiceDetailDTO
    {
        public int InvoiceDetailId { get; set; }
        public int LineNumber { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int InvoiceHeaderId { get; set; }
    }
}
