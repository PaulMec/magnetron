using DB.Models.ViewModels;
using magnetron.Domain.Models;
using System.Collections.Generic;

namespace magnetron.Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<ProductDTO> GetAll();
        ProductDTO GetById(int id);
        void Add(ProductDTO product);
        void Update(ProductDTO product);
        void Delete(int id);

        // Métodos para obtener datos calculados desde vistas SQL
        IEnumerable<ProductSoldViewModel> GetSoldProducts();
        IEnumerable<ProductProfitViewModel> GetProductsProfit();
        IEnumerable<ProductProfitMarginViewModel> GetProductsProfitMargin();
    }
}
