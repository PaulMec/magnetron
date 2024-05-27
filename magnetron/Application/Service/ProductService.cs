using magnetron.Infrastructure.Interfaces;
using DB.Models.ViewModels;
using System.Collections.Generic;
using magnetron.Application.Interfaces;
using magnetron.Domain.Models;
using System;

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
            try
            {
                return _productRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service Layer: An error occurred while retrieving all products.", ex);
            }
        }

        public ProductDTO GetProductById(int id)
        {
            try
            {
                return _productRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Service Layer: An error occurred while retrieving the product with ID {id}.", ex);
            }
        }

        public void CreateProduct(ProductDTO product)
        {
            try
            {
                _productRepository.Add(product);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service Layer: An error occurred while creating the product.", ex);
            }
        }

        public void UpdateProduct(ProductDTO product)
        {
            try
            {
                _productRepository.Update(product);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Service Layer: An error occurred while updating the product with ID {product.ProductId}.", ex);
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                _productRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Service Layer: An error occurred while deleting the product with ID {id}.", ex);
            }
        }

        public IEnumerable<ProductSoldViewModel> GetProductsByQuantitySold()
        {
            try
            {
                return _productRepository.GetSoldProducts();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service Layer: An error occurred while retrieving the products by quantity sold.", ex);
            }
        }

        public IEnumerable<ProductProfitViewModel> GetProductsProfit()
        {
            try
            {
                return _productRepository.GetProductsProfit();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service Layer: An error occurred while retrieving the products profit.", ex);
            }
        }

        public IEnumerable<ProductProfitMarginViewModel> GetProductsProfitMargin()
        {
            try
            {
                return _productRepository.GetProductsProfitMargin();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service Layer: An error occurred while retrieving the products profit margin.", ex);
            }
        }
    }
}
