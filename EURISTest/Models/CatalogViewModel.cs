using EURIS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EURISTest.Models
{
    public class CatalogViewModel
    {
        public int CatalogId { get; set; }
        [Required(ErrorMessage = "Code is required", AllowEmptyStrings = false)]
        public string Code { get; set; }
        [Required(ErrorMessage = "Description is required", AllowEmptyStrings = false)]
        public string Description { get; set; }
        [Display(Name = "Product")]
        public IEnumerable<ProductSelectViewModel> ProductsViewModel { get; set; }
        public List<Product> Products { get; set; }
    }
}