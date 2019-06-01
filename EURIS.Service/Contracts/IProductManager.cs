using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;

namespace EURIS.Service.Contracts
{
    public interface IProductManager
    {
        List<Product> GetProducts();
        List<Product> GetProducts(Func<Product, bool> where);
        Product GetProduct(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        void Dispose();
    }
}