using magnetron.Domain.Models;
using DB.Models.ViewModels;
using System.Collections.Generic;

namespace magnetron.Application.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetAllProducts();
        ProductDTO GetProductById(int id);
        void CreateProduct(ProductDTO product);
        void UpdateProduct(ProductDTO product);
        void DeleteProduct(int id);
        IEnumerable<ProductSoldViewModel> GetProductsByQuantitySold();
        IEnumerable<ProductProfitViewModel> GetProductsProfit();
        IEnumerable<ProductProfitMarginViewModel> GetProductsProfitMargin();
    }
}
