using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EURISTest.Models
{
    public class ProductSelectViewModel
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsSelected { get; set; }
    }
}