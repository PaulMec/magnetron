using magnetron.Infrastructure.Interfaces;
using DB.Models.ViewModels;
using System.Collections.Generic;
using magnetron.Application.Interfaces;
using magnetron.Domain.Models;

namespace magnetron.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        public ProductDTO GetProductById(int id)
        {
            return _productRepository.GetById(id);
        }

        public void CreateProduct(ProductDTO product)
        {
            _productRepository.Add(product);
        }

        public void UpdateProduct(ProductDTO product)
        {
            _productRepository.Update(product);
        }

        public void DeleteProduct(int id)
        {
            _productRepository.Delete(id);
        }

        public IEnumerable<ProductSoldViewModel> GetProductsByQuantitySold()
        {
            return _productRepository.GetSoldProducts();
        }

        public IEnumerable<ProductProfitViewModel> GetProductsProfit()
        {
            return _productRepository.GetProductsProfit();
        }

        public IEnumerable<ProductProfitMarginViewModel> GetProductsProfitMargin()
        {
            return _productRepository.GetProductsProfitMargin();
        }
    }
}
