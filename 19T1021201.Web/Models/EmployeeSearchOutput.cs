using _19T1021201.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021201.Web.Models
{
    /// <summary>
    /// kết quả tìm kiếm nhân viên dưới dạng phân trang
    /// </summary>
    public class EmployeeSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        public List<Employee> Data { get; set; }
    }
}