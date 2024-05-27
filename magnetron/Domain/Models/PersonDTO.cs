namespace magnetron.Domain.Models
{
    public class PersonDTO
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }

        // Propiedades calculadas
        public decimal TotalBilled { get; set; }
    }
}
