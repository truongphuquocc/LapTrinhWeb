using _19T1021201.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021201.DataLayers
{
    /// <summary>
    /// định nghĩa các phép xử lý liên quan đến dữ liệu tài khoản người dùng
    /// </summary>
    public interface IUserAccountDAL
    {
        /// <summary>
        /// Kiểm tra tên đăng nhập hợp lệ hay không
        /// Nếu hợp lệ trả về thông tin tài khoản, ngược lại trả về null
        /// </summary>
        /// <param name="userName">Tài khoản đăng nhập</param>
        /// <param name="password">Mật khẩu</param>
        /// <returns></returns>
        UserAccount Authorize(string userName, string password);

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}
