namespace DB.Models.ViewModels
{
    public class ProductSoldViewModel
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public decimal QuantitySold { get; set; }
    }
}
