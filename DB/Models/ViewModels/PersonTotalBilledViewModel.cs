namespace DB.Models.ViewModels
{
    public class PersonTotalBilledViewModel
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal TotalBilled { get; set; }
    }
}
