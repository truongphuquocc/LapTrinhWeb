using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021201.DomainModels;

namespace _19T1021201.DataLayers
{
    /// <summary>
    /// Các chức năng xử lý dữ liệu cho quốc gia
    /// </summary>
    public interface ICountryDAL
    {
        /// <summary>
        /// Lấy danh sách tất cả quốc gia
        /// </summary>
        /// <returns></returns>
        IList<Country> List();
    }
}
