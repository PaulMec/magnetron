using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DB.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string DocumentType { get; set; }

        [Required]
        [StringLength(20)]
        public string DocumentNumber { get; set; }

        public virtual ICollection<InvoiceHeader> Invoices { get; set; } = new List<InvoiceHeader>();
    }
}
