using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;
using System.Data.Entity;
using EURIS.Service.Contracts;
using System.Data;

namespace EURIS.Service
{
    public class ProductManager : IProductManager
    {
        private readonly LocalDbEntities _context;

        public ProductManager(LocalDbEntities context)
        {
            _context = context;
        }

        public void CreateProduct(Product product)
        {
            _context.Product.Add(product);
        }

        public void DeleteProduct(int id)
        {
            Product product = GetProduct(id);
            var productCatalogs = product.ProductCatalog.ToList();

            foreach (ProductCatalog c in productCatalogs)
                _context.Entry(c).State = EntityState.Deleted;

            _context.Entry(product).State = EntityState.Deleted;
        }

        public List<Product> GetProducts()
        {
            return _context.Product.ToList();
        }

        public Product GetProduct(int id)
        {
            return _context.Product.Find(id);
        }

        public List<Product> GetProducts(Func<Product, bool> where)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
