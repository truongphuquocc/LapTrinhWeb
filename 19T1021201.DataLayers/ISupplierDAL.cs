using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021201.DomainModels;

namespace _19T1021201.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép dữ liệu trên nhà cung cấp
    /// </summary>
    public interface ISupplierDAL
    {
        /// <summary>
        /// Tìm kiếm và lấy danh sácch các nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng hiển thị trên mỗi trang(0 tức là không yêu cầu phân trang)</param>
        /// <param name="searchValue">Tên cần tìm kiếm (chuỗi rỗng nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        IList<Supplier> List(int page = 1, int pageSize = 0, string searchValue = "");

        /// <summary>
        /// Lấy thông tin của một nhà cung cấp dựa vào mã nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        Supplier Get(int supplierID);

        /// <summary>
        /// Đếm số nhà cung cấp tìm được
        /// </summary>
        /// <param name="searchValue">Tên cần tìm kiếm (chuỗi rỗng nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        int Count(string searchValue = "");

        /// <summary>
        /// Bổ sung thêm một nhà cung cấp vào CSDL
        /// </summary>
        /// <param name="data">Đối tượng dữ liệu chứa nhà cung cấp</param>
        /// <returns>ID của nhà cung cấp được tạo mới</returns>
        int Add(Supplier data);

        /// <summary>
        /// Cập nhật thông tin của nhà cung cấp
        /// </summary>
        /// <param name="data">Đối tượng dữ liệu chứa nhà cung cấp</param>
        /// <returns></returns>
        bool Update(Supplier data);

        /// <summary>
        /// Xoá một nhà cung cấp dựa vào mã nhà cung cấp
        /// </summary>
        /// <param name="supplierID">Mã của nhà cung cấp cần xoá</param>
        /// <returns></returns>
        bool Delete(int supplierID);

        /// <summary>
        /// Kiểm tra xem nhà cung cấp hiện có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="supplierID">Mã của nhà cung cấp cần kiểm tra</param>
        /// <returns></returns>
        bool InUsed(int supplierID);

        
    }
}
