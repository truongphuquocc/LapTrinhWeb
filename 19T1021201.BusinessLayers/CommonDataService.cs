using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021201.DataLayers;
using _19T1021201.DomainModels;
using System.Configuration;

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


            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

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
        /// <summary>
        /// Tìm kiếm khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">số trang</param>
        /// <param name="pageSize">số khách hàng trong 1 page</param>
        /// <param name="searchValue">Từ khóa tìm kiếm</param>
        /// <param name="rowCount">Tổng số khách hàng sau khi xử lý tìm kiếm</param>
        /// <returns>Danh sách khách hàng sau khi tìm kiếm</returns>
        /// 
    
        
        public static List<Category> ListOfCategorys(int page
                                                    , int pageSize
                                                    , string searchValue
                                                    , out int rowCount)
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Category> ListOfCategorys() => categoryDB.List().ToList();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public static Category GetCategory(int CategoryID)
        {
            return categoryDB.Get(CategoryID);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int CategoryID)
        {
            if (categoryDB.InUsed(CategoryID))
            {
                return false;
            }
            return categoryDB.Delete(CategoryID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public static bool InUsedCategory(int CategoryID)
        {
            return categoryDB.InUsed(CategoryID);
        }
        #endregion

        #region các chức năng liên quan đến khách hàng
        public static List<Customer> ListOfCustomers()
        {
            return customerDB.List().ToList();
        }

        /// <summary>
        /// Tìm kiếm khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">số trang</param>
        /// <param name="pageSize">số khách hàng trong 1 page</param>
        /// <param name="searchValue">Từ khóa tìm kiếm</param>
        /// <param name="rowCount">Tổng số khách hàng sau khi xử lý tìm kiếm</param>
        /// <returns>Danh sách khách hàng sau khi tìm kiếm</returns>
        /// 
        public static List<Customer> ListOfCustomer(int page
                                                    , int pageSize
                                                    , string searchValue
                                                    , out int rowCount)
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int CustomerID)
        {
            return customerDB.Get(CustomerID);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int CustomerID)
        {
            if (customerDB.InUsed(CustomerID))
                return false;

            return customerDB.Delete(CustomerID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static bool InUsedCustomer(int CustomerID)
        {
            return customerDB.InUsed(CustomerID);
        }
        #endregion

        #region các chức năng liên quan đến nhân viên

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Employee> ListOfEmployee(string searchValue)
        {
            return employeeDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">số trang</param>
        /// <param name="pageSize">số khách hàng trong 1 page</param>
        /// <param name="searchValue">Từ khóa tìm kiếm</param>
        /// <param name="rowCount">Tổng số khách hàng sau khi xử lý tìm kiếm</param>
        /// <returns>Danh sách khách hàng sau khi tìm kiếm</returns>
        public static List<Employee> ListOfEmployees(int page
                                                    , int pageSize
                                                    , string searchValue
                                                    , out int rowCount)
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int EmployeeID)
        {
            return employeeDB.Get(EmployeeID);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int EmployeeID)
        {
            if (employeeDB.InUsed(EmployeeID))
            {
                return false;
            }
            return employeeDB.Delete(EmployeeID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public static bool InUsedEmployee(int EmployeeID)
        {
            return employeeDB.InUsed(EmployeeID);
        }
        #endregion

        #region các chức năng liên quan đến người giao hàng
        public static List<Shipper> ListOfShippers() => shipperDB.List().ToList();


        /// <summary>
        /// Tìm kiếm khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">số trang</param>
        /// <param name="pageSize">số khách hàng trong 1 page</param>
        /// <param name="searchValue">Từ khóa tìm kiếm</param>
        /// <param name="rowCount">Tổng số khách hàng sau khi xử lý tìm kiếm</param>
        /// <returns>Danh sách khách hàng sau khi tìm kiếm</returns>
        /// 
        public static List<Shipper> ListOfShippers(int page
                                                    , int pageSize
                                                    , string searchValue
                                                    , out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ShipperID"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int ShipperID)
        {
            return shipperDB.Get(ShipperID);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ShipperID"></param>
        /// <returns></returns>
        public static bool DeleteShipper(int ShipperID)
        {
            if (shipperDB.InUsed(ShipperID))
            {
                return false;
            }
            return shipperDB.Delete(ShipperID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="ShipperID"></param>
        /// <returns></returns>
        public static bool InUsedShipper(int ShipperID)
        {
            return shipperDB.InUsed(ShipperID);
        }
        #endregion

        #region các chức năng liên quan đến nhà cung cấp
        /// <summary>
        /// Tìm kiếm, lấy danh sách các nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Giá trị tìm kiếm</param>
        /// <param name="rowCount">output: tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Supplier> ListOfSupplier(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách nhà cung cấp (không phân trang)
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(string searchValue)
        {
            return supplierDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// Bổ sung nhà cung cấp
        /// </summary>
        /// <param name="data">Mã của nhà cung cấp được bổ sung</param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }

        /// <summary>
        /// cập nhật một nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int supplierID)
        {
            return supplierDB.Delete(supplierID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return supplierDB.Get(supplierID);
        }

        /// <summary>
        /// Kiểm tra xem một nahf cung cấp hiện có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="supplierID">Mã nhà cung cấp cần kiểm tra</param>
        /// <returns></returns>
        public static bool InUsedSupplier(int supplierID)
        {
            return supplierDB.InUsed(supplierID);
        }
        #endregion

        #region các chức năng liên quan đến mặt hàng

        #endregion

    }
}
