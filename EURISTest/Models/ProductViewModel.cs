using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EURISTest.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Code is required", AllowEmptyStrings = false)]
        public string Code { get; set; }
        [Required(ErrorMessage = "Description is required", AllowEmptyStrings = false)]
        public string Description { get; set; }
    }
}