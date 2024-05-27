using DB.Context;
using DB.Models;
using DB.Models.ViewModels;
using magnetron.Domain.Models;
using magnetron.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace magnetron.Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly FacturacionContext _context;

        public ProductRepository(FacturacionContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            try
            {
                return _context.Products
                    .Select(p => new ProductDTO
                    {
                        ProductId = p.ProductId,
                        Description = p.Description,
                        Price = p.Price,
                        Cost = p.Cost,
                        UnitOfMeasure = p.UnitOfMeasure
                    }).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Repository Layer: An error occurred while retrieving all products.", ex);
            }
        }

        public ProductDTO GetById(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null) return null;
                return new ProductDTO
                {
                    ProductId = product.ProductId,
                    Description = product.Description,
                    Price = product.Price,
                    Cost = product.Cost,
                    UnitOfMeasure = product.UnitOfMeasure
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Repository Layer: An error occurred while retrieving the product with ID {id}.", ex);
            }
        }

        public void Add(ProductDTO productDto)
        {
            try
            {
                var product = new Product
                {
                    Description = productDto.Description,
                    Price = productDto.Price,
                    Cost = productDto.Cost,
                    UnitOfMeasure = productDto.UnitOfMeasure
                };
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Repository Layer: An error occurred while adding a new product.", ex);
            }
        }

        public void Update(ProductDTO productDto)
        {
            try
            {
                var product = _context.Products.Find(productDto.ProductId);
                if (product != null)
                {
                    product.Description = productDto.Description;
                    product.Price = productDto.Price;
                    product.Cost = productDto.Cost;
                    product.UnitOfMeasure = productDto.UnitOfMeasure;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Repository Layer: An error occurred while updating the product with ID {productDto.ProductId}.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Repository Layer: An error occurred while deleting the product with ID {id}.", ex);
            }
        }

        // Métodos para obtener datos calculados desde vistas SQL
        public IEnumerable<ProductSoldViewModel> GetSoldProducts()
        {
            try
            {
                return _context.ProductSoldViewModels
                    .FromSqlRaw("SELECT * FROM ProductosPorCantidadFacturada")
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Repository Layer: An error occurred while retrieving sold products.", ex);
            }
        }

        public IEnumerable<ProductProfitViewModel> GetProductsProfit()
        {
            try
            {
                return _context.ProductProfitViewModels
                    .FromSqlRaw("SELECT * FROM ProductosPorUtilidad")
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Repository Layer: An error occurred while retrieving products' profit.", ex);
            }
        }

        public IEnumerable<ProductProfitMarginViewModel> GetProductsProfitMargin()
        {
            try
            {
                return _context.ProductProfitMarginViewModels
                    .FromSqlRaw("SELECT * FROM ProductosMargenGanancia")
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Repository Layer: An error occurred while retrieving products' profit margin.", ex);
            }
        }
    }
}
