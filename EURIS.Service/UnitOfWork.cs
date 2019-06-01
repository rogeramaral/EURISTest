using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;

namespace EURIS.Service.Contracts
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LocalDbEntities _context;

        public IProductManager ProductManager { get; set; }
        public ICatalogManager CatalogManager { get; set; }
        public IProductCatalogManager ProductCatalogManager { get; set; }

        public UnitOfWork(LocalDbEntities context)
        {
            _context = context;
            ProductManager = new ProductManager(context);
            CatalogManager = new CatalogManager(context);
            ProductCatalogManager = new ProductCatalogManager(context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
