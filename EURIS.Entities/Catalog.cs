
//------------------------------------------------------------------------------
// <auto-generated>
//    O código foi gerado a partir de um modelo.
//
//    Alterações manuais neste arquivo podem provocar comportamento inesperado no aplicativo.
//    Alterações manuais neste arquivo serão substituídas se o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------


namespace EURIS.Entities
{

using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Catalog
{

    public Catalog()
    {

        this.ProductCatalog = new HashSet<ProductCatalog>();

    }

    [Display(Name = "Id")]
    public int CatalogId { get; set; }

    public string Code { get; set; }

    public string Description { get; set; }



    public virtual ICollection<ProductCatalog> ProductCatalog { get; set; }

}

}
