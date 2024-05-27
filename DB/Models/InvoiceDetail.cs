using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Models
{
    public class InvoiceDetail
    {
        [Key]
        public int InvoiceDetailId { get; set; }

        [Required]
        public int LineNumber { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Required]
        public int InvoiceHeaderId { get; set; }

        [ForeignKey("InvoiceHeaderId")]
        public virtual InvoiceHeader InvoiceHeader { get; set; }
    }
}
