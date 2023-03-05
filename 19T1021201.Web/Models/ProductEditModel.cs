using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021201.DomainModels;

namespace _19T1021201.Web.Models
{
    public class ProductEditModel 
   
    {
        public Product Product { get; set; }
        public List<ProductPhoto> DataPhoto { get; set; }
        public List<ProductAttribute> DataAttribute { get; set; }
    }
}