namespace magnetron.Domain.Models
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string UnitOfMeasure { get; set; }
    }
}
