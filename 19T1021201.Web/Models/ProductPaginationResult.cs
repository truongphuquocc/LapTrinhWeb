using _19T1021201.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021201.Web.Models
{
    public class ProductPaginationResult : BasePaginationResultProduct
    {
        public List<Product> Data { get; set; }
    }
}