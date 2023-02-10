using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021201.DomainModels;

namespace _19T1021201.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt các phép dữ liệu cho nhà cung cấp
    /// </summary>
    public class SupplierDAL : _BaseDAL, ICommonDAL<Supplier>
    {
        public SupplierDAL(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Thêm một nhà cung cấp
        /// </summary>
        /// <param name="data">Thông tin nhà cung cấp cần thêm</param>
        /// <returns>Kết quả theo kiểu số nguyên thành công hay thất bại</returns>
        public int Add(Supplier data)
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
        /// Xóa 1 nhà cung cấp theo ID nhà cung cấp
        /// </summary>
        /// <param name="ID">ID nhà cung cấp</param>
        /// <returns>Trả về kết quả true or false</returns>
        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lấy thông tin một nhà cung cấp
        /// </summary>
        /// <param name="ID">ID nhà cung cấp</param>
        /// <returns>Dữ liệu nhà cung cấp theo ID Nhà cung cấp</returns>
        public Supplier Get(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Kiểm tra ID nhà cung cấp có tồn tại trong bảng dữ liệu khác
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Trả về kết quả true or false</returns>
        public bool InUsed(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần tìm kiếm</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm kiếm. Nếu không nhập thì lấy toàn bộ</param>
        /// <returns>Danh sách  nhà cung cấp kết quả tìm kiếm</returns>
        public IList<Supplier> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cập nhật thông tin 1 nhà cung cấp
        /// </summary>
        /// <param name="data">Thông tin nhà cung cấp cần cập nhật</param>
        /// <returns>Trả về kết quả true or false </returns>
        public bool Update(Supplier data)
        {
            throw new NotImplementedException();
        }
    }
}
