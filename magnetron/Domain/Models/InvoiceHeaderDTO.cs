namespace magnetron.Domain.Models
{
    public class InvoiceHeaderDTO
    {
        public int InvoiceHeaderId { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int PersonId { get; set; }
    }
}
