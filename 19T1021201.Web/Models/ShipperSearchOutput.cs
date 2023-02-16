using _19T1021201.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021201.Web.Models
{
    /// <summary>
    /// kết quả tìm kiếm người giao hàng dưới dạng phân trang
    /// </summary>
    public class ShipperSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách người giao hàng
        /// </summary>
        public List<Shipper> Data { get; set; }
    }
}