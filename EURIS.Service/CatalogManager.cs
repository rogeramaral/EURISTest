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
    public class CatalogManager : ICatalogManager
    {
        private readonly LocalDbEntities _context;

        public CatalogManager(LocalDbEntities context)
        {
            _context = context;
        }

        public List<Catalog> GetCatalogs()
        {
            return _context.Catalog.ToList();
        }

        public Catalog GetCatalog(int id)
        {
            return _context.Catalog.Find(id);
        }

        public void CreateCatalog(Catalog catalog)
        {
            _context.Catalog.Add(catalog);
        }

        public void DeleteCatalog(int id)
        {
            Catalog catalog = GetCatalog(id);
            var productCatalogs = catalog.ProductCatalog.ToList();

            foreach (ProductCatalog c in productCatalogs)
                _context.Entry(c).State = EntityState.Deleted;

            _context.Entry(catalog).State = EntityState.Deleted;
        }

        public void UpdateCatalog(Catalog catalog)
        {
            _context.Entry(catalog).State = EntityState.Modified;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
