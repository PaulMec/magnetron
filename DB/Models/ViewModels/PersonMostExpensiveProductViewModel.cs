namespace DB.Models.ViewModels
{
    public class PersonMostExpensiveProductViewModel
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
