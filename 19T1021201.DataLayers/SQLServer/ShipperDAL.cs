using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021201.DomainModels;

namespace _19T1021201.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt các phép dữ liệu cho shipper
    /// </summary>
    public class ShipperDAL : _BaseDAL, ICommonDAL<Shipper>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public ShipperDAL(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Thêm một người giao hàng
        /// </summary>
        /// <param name="data">Thông tin người giao hàng cần thêm</param>
        /// <returns>Kết quả theo kiểu số nguyên thành công hay thất bại</returns>
        public int Add(Shipper data)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Đếm sô lượng người giao hàng thỏa mãn sau kết quả tìm kiếm
        /// </summary>
        /// <param name="searchValue">Tên cần tìm kiếm. Nếu không nhập thì đếm toàn bộ</param>
        /// <returns>Tên cần tìm kiếm. Nếu không nhập thì đếm toàn bộ</returns>
        public int Count(string searchValue = "")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Xóa 1 người giao hàn theo ID người giao hàng
        /// </summary>
        /// <param name="ID">ID người giao hàng</param>
        /// <returns>Trả về kết quả true or false</returns>
        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lấy thông tin một người giao hàng
        /// </summary>
        /// <param name="ID">ID người giao hàng</param>
        /// <returns>Dữ liệu nhà cung cấp theo ID người giao hàng</returns>
        public Shipper Get(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Kiểm tra ID của người giao hàng ở bảng khác không
        /// </summary>
        /// <param name="id">ID người giao hàng</param>
        /// <returns>Trả về kết quả true or false</returns>
        public bool InUsed(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách người giao hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần tìm kiếm</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Tên hoặc số điện thoại. Nếu không nhập thì lấy toàn bộ</param>
        /// <returns>Danh sách  người giao hàng kết quả tìm kiếm</returns>
        public IList<Shipper> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cập nhật thông tin một người giao hàng
        /// </summary>
        /// <param name="data">Thông tin người giao hàng cập nhật</param>
        /// <returns>Trả về kết quả true or false </returns>
        public bool Update(Shipper data)
        {
            throw new NotImplementedException();
        }
    }
}
