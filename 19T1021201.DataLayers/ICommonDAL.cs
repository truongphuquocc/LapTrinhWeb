using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021201.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép dữ liệu chung cho các dữ liệu đơn giản trên các bảng
    /// </summary>
    public interface ICommonDAL<T> where T : class
    {
        /// <summary>
        /// Tìm kiếm, phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang (0 nếu không phân trang)</param>
        /// <param name="searchValue">Giá trị tìm kiềm (rỗng nếu bỏ qua)</param>
        /// <returns></returns>
        IList<T> List(int page = 1, int pageSize = 0, string searchValue = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue = "");

        /// <summary>
        /// Lấy một dòng dữ liệu vào id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        T Get(int ID);

        /// <summary>
        /// Bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(T data);

        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(T data);

        /// <summary>
        /// Xoá
        /// </summary>
        /// <param name="ID">ID của Dữ liệu cần xoá</param>
        /// <returns></returns>
        bool Delete(int ID);

        /// <summary>
        /// Kiểm tra xem có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="ID">ID cần kiểm tra</param>
        /// <returns></returns>
        bool InUsed(int id);

    }
}
