using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021201.DomainModels
{
    /// <summary>
    /// Khách hàng
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// KhachHang
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// Teeb khách hàng
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Thông tin liên lạc khách hàng
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// Địa chỉ khách hàng
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Thành phố khách hàng sinh sống
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Mã bưu chính thành phố
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// Quốc gia khách hàng
        /// </summary>
        public string Country { get; set; }
    }
}
