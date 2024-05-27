using DB.Context;
using DB.Models;
using DB.Models.ViewModels;
using magnetron.Domain.Models;
using magnetron.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public ProductDTO GetById(int id)
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

        public void Add(ProductDTO productDto)
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

        public void Update(ProductDTO productDto)
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

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        // Métodos para obtener datos calculados desde vistas SQL
        public IEnumerable<ProductSoldViewModel> GetSoldProducts()
        {
            return _context.ProductSoldViewModels
                .FromSqlRaw("SELECT * FROM ProductosPorCantidadFacturada")
                .ToList();
        }

        public IEnumerable<ProductProfitViewModel> GetProductsProfit()
        {
            return _context.ProductProfitViewModels
                .FromSqlRaw("SELECT * FROM ProductosPorUtilidad")
                .ToList();
        }

        public IEnumerable<ProductProfitMarginViewModel> GetProductsProfitMargin()
        {
            return _context.ProductProfitMarginViewModels
                .FromSqlRaw("SELECT * FROM ProductosMargenGanancia")
                .ToList();
        }
    }
}
