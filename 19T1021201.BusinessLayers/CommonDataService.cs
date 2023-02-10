using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021201.DataLayers;
using _19T1021201.DomainModels;

namespace _19T1021201.BusinessLayers
{
    /// <summary>
    /// Các chức năng nghiệp vụ liên quan đến : nhà cung cấp, khách hàng, người giao hàng, nhân viên, loại hàng
    /// </summary>
    public static class CommonDataService
    {
        private static ICountryDAL countryDB;
        private static ICommonDAL<Supplier> supplierDB;
        private static ICommonDAL<Shipper> shipperDB;
        private static ICommonDAL<Category> categoryDB;
        private static ICommonDAL<Customer> customerDB;
        private static ICommonDAL<Employee> employeeDB;

        /// <summary>
        /// Ctor
        /// </summary>
        static CommonDataService()
        {
            string connectionString = @"server=DESKTOP-QII8E8O;user id = sa; password = 123456; database = LiteCommerceDB";

            
            
            countryDB = new DataLayers.SQLServer.CountryDAL(connectionString);
        
            supplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);

            shipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);

            categoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);

            customerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);

            employeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
        }

        #region Quốc gia
        /// <summary>
        /// Lấy danh sách tất cả quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCountries()
        {
            return countryDB.List().ToList();
        }
        #endregion

        #region các chức năng liên quan đến loại hàng

        #endregion

        #region các chức năng liên quan đến khách hàng

        #endregion

        #region các chức năng liên quan đến nhân viên

        #endregion

        #region các chức năng liên quan đến người giao hàng

        #endregion

        #region các chức năng liên quan đến nhà cung cấp

        #endregion

        #region các chức năng liên quan đến mặt hàng

        #endregion

    }
}
