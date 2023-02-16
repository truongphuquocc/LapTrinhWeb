using _19T1021201.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021201.Web.Models
{
    /// <summary>
    /// kết quả tìm kiếm nhà cung cấp dưới dạng phân trang
    /// </summary>
    public class CustomerSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách nhà cung cấp
        /// </summary>
        public List<Customer> Data { get; set; }
    }
}