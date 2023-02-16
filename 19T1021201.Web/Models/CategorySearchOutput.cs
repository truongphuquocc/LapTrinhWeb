using _19T1021201.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021201.Web.Models
{
    /// <summary>
    /// kết quả tìm kiếm loại hàng dưới dạng phân trang
    /// </summary>
    public class CategorySearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách loại hàng 
        /// </summary>
        public List<Category> Data { get; set; }
    }
}