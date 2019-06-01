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
    public class ProductCatalogManager : IProductCatalogManager
    {
        private readonly LocalDbEntities _context;

        public ProductCatalogManager(LocalDbEntities context)
        {
            _context = context;
        }

        public void AddProductCatalog(ProductCatalog productCatalog)
        {
            _context.ProductCatalog.Add(productCatalog);
        }

        public List<ProductCatalog> GetProductCatalogs()
        {
            return _context.ProductCatalog.ToList();
        }

        public void DeleteProductCatalog(ProductCatalog productCatalog)
        {
            _context.ProductCatalog.Remove(productCatalog);
        }

        public void UpdateProductCatalog(List<Catalog> catalogs, int productId)
        {

            //delete       
            foreach (var c in GetProductCatalogs())
            {
                if (c.ProductId == productId)
                {
                    DeleteProductCatalog(c);
                }
            }

            //insert
            foreach (var p in catalogs)
            {
               
                ProductCatalog productCatalog = new ProductCatalog
                {
                    CatalogId = p.CatalogId,
                    ProductId = productId
                };

                AddProductCatalog(productCatalog);
            }
        }

        public void UpdateProductCatalog(List<Product> products, int catalogId)
        {

            //delete       
            foreach (var c in GetProductCatalogs())
            {
                if (c.CatalogId == catalogId)
                {
                    DeleteProductCatalog(c);
                }
            }

            //insert
            foreach (var p in products)
            {

                ProductCatalog productCatalog = new ProductCatalog
                {
                    ProductId = p.ProductId,
                    CatalogId = catalogId
                };

                AddProductCatalog(productCatalog);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
