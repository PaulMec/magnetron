namespace DB.Models.ViewModels
{
    public class ProductProfitMarginViewModel
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public decimal ProfitMargin { get; set; }
    }
}
