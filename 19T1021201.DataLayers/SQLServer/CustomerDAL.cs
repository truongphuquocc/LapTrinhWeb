using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021201.DomainModels;

namespace _19T1021201.DataLayers.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerDAL : _BaseDAL, ICommonDAL<Customer>
    {
        public CustomerDAL(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Thêm 1 khách hàng
        /// </summary>
        /// <param name="data">Thông tin khách hàng cần thêm</param>
        /// <returns>Kết quả theo kiểu số nguyên thành công hay thất bại</returns>
        public int Add(Customer data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Đếm sô lượng khách hàng thỏa mã sau kết quả tìm kiếm
        /// </summary>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm kiếm. Nếu không nhập thì đếm toàn bộ</param>
        /// <returns>Tên hoặc địa chỉ cần tìm kiếm. Nếu không nhập thì đếm toàn bộ</returns>
        public int Count(string searchValue = "")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Xóa 1 khách hàng theo ID khách hàng
        /// </summary>
        /// <param name="ID">Id khách hàng</param>
        /// <returns>Trả về kết quả true or false</returns>
        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lấy thông tin 1 khách hàng
        /// </summary>
        /// <param name="ID">ID Khách hàng</param>
        /// <returns>Dữ liệu khách hàng theo ID Khách hàng</returns>
        public Customer Get(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Kiểm tra ID khách hàng có tồn tại trong bảng dữ liệu khác
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Trả về kết quả true or false</returns>
        public bool InUsed(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần tìm kiếm</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm kiếm. Nếu không nhập thì lấy toàn bộ</param>
        /// <returns>Danh sách  khách hàng sau kết quả tìm kiếm</returns>
        public IList<Customer> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cập nhật thông tin 1 khách hàng
        /// </summary>
        /// <param name="data">Thông tin khách hàng cần cập nhật</param>
        /// <returns>Trả về kết quả true or false </returns>
        public bool Update(Customer data)
        {
            throw new NotImplementedException();
        }
    }
}
