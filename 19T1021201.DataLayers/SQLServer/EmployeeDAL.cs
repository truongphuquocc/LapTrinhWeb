using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021201.DomainModels;

namespace _19T1021201.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt các phép dữ liệu cho nhân viên
    /// </summary>
    public class EmployeeDAL : _BaseDAL, ICommonDAL<Employee>
    {
        public EmployeeDAL(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Thêm 1 nhân viên
        /// </summary>
        /// <param name="data">Thông tin nhân viên cần thêm</param>
        /// <returns>Kết quả theo kiểu số nguyên thành công hay thất bại</returns>
        public int Add(Employee data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Đếm sô lượng nhân viên thỏa mã sau kết quả tìm kiếm
        /// </summary>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm kiếm. Nếu không nhập thì đếm toàn bộ</param>
        /// <returns>Tên hoặc địa chỉ cần tìm kiếm. Nếu không nhập thì đếm toàn bộ</returns>
        public int Count(string searchValue = "")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Xóa 1 nhân viên theo ID nhân viên
        /// </summary>
        /// <param name="ID">Id nhân viên</param>
        /// <returns>Trả về kết quả true or false</returns>
        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lấy thông tin 1 nhân viên
        /// </summary>
        /// <param name="ID">ID nhân viên</param>
        /// <returns>Dữ liệu nhân viên theo ID nhân viên</returns>
        public Employee Get(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Kiểm tra ID nhân viên có tồn tại trong bảng dữ liệu khác
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Trả về kết quả true or false</returns>
        public bool InUsed(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách nhân viên dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần tìm kiếm</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm kiếm. Nếu không nhập thì lấy toàn bộ</param>
        /// <returns>Danh sách  nhân viên sau kết quả tìm kiếm</returns>
        public IList<Employee> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cập nhật thông tin 1 nhân viên
        /// </summary>
        /// <param name="data">Thông tin nhân viên cần cập nhật</param>
        /// <returns>Trả về kết quả true or false </returns>
        public bool Update(Employee data)
        {
            throw new NotImplementedException();
        }
    }
}
