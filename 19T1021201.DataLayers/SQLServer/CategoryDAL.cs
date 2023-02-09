using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021201.DomainModels;

namespace _19T1021201.DataLayers.SQLServer
{
    public class CategoryDAL : _BaseDAL, ICommonDAL<Category>
    {
        public CategoryDAL(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Thêm một loại hàng
        /// </summary>
        /// <param name="data">Thông tin loại hàng cần thêm</param>
        /// <returns>Kết quả theo kiểu số nguyên thành công hay thất bại</returns>
        public int Add(Category data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Đếm sô lượng loại hàng thỏa mã sau kết quả tìm kiếm
        /// </summary>
        /// <param name="searchValue">Tên cần tìm kiếm. Nếu không nhập thì đếm toàn bộ</param>
        /// <returns>Tên cần tìm kiếm. Nếu không nhập thì đếm toàn bộ</returns>
        public int Count(string searchValue = "")
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Xóa 1 nhà cung cấp theo ID nhà cung cấp
        /// </summary>
        /// <param name="CategoryID">ID nhà cung cấp</param>
        /// <returns>Trả về kết quả true or false</returns>
        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lấy thông tin một loại hàng
        /// </summary>
        /// <param name="CategoryID">ID loại hàng</param>
        /// <returns>Dữ liệu loại hàng theo ID loại hàng</returns>
        public Category Get(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Kiểm tra ID loại hàng có tồn tại trong bảng dữ liệu khác
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns>Trả về kết quả true or false</returns>
        public bool InUsed(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách loại hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần tìm kiếm</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Tên cần tìm kiếm. Nếu không nhập thì lấy toàn bộ</param>
        /// <returns>Danh sách  loại hàng kết quả tìm kiếm</returns>
        public IList<Category> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cập nhật thông tin một loại hàng
        /// </summary>
        /// <param name="data">Thông tin loại hàng cần cập nhật</param>
        /// <returns>Trả về kết quả true or false </returns>
        public bool Update(Category data)
        {
            throw new NotImplementedException();
        }
    }
}
