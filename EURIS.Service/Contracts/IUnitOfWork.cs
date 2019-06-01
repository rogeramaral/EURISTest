using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;

namespace EURIS.Service.Contracts
{
    public interface IUnitOfWork
    {
        IProductManager ProductManager { get; set; }
        ICatalogManager CatalogManager { get; set; }
        IProductCatalogManager ProductCatalogManager { get; set; }

        void Save();
    }
}
