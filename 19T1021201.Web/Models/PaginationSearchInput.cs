using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021201.Web.Models
{
    /// <summary>
    /// biểu diễn dữ liệu đầu vào để tìm kiếm, phân trang chung
    /// </summary>
    public class PaginationSearchInput
    {
        /// <summary>
        /// Trang cần hiển thị
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Số dòng cần hiển thị trên mỗi trang
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Giá trị cần tìm
        /// </summary>
        public string SearchValue { get; set; }
    }

    public class PaginationSearchInputProduct : PaginationSearchInput
    {
        public int SupplierID { get; set; }

        public int CategoryID { get; set; }
    }

    public class PaginationSearchInputOrder : PaginationSearchInput
    {
        public int Status { get; set; }
    }

}