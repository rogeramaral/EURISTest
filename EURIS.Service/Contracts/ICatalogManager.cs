using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;

namespace EURIS.Service.Contracts
{
    public interface ICatalogManager
    {
        List<Catalog> GetCatalogs();
        Catalog GetCatalog(int id);
        void CreateCatalog(Catalog catalog);
        void UpdateCatalog(Catalog catalog);
        void DeleteCatalog(int id);
        void Dispose();
    }
}