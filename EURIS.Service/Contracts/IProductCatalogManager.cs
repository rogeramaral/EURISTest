using EURIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURIS.Service.Contracts
{
    public interface IProductCatalogManager
    {
        void DeleteProductCatalog(ProductCatalog productCatalog);
        void AddProductCatalog(ProductCatalog productCatalog);
        List<ProductCatalog> GetProductCatalogs();
        void UpdateProductCatalog(List<Catalog> catalogs, int productId);
        void UpdateProductCatalog(List<Product> products, int catalogId);
    }
}
