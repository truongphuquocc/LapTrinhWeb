﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
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
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Suppliers(SupplierName, ContactName, Address, City, PostalCode, Country, Phone)
                                    VALUES(@SupplierName, @ContactName, @Address, @City, @PostalCode, @Country, @Phone);
                                    SELECT SCOPE_IDENTITY()";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SupplierName", data.SupplierName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// Đếm sô lượng khách hàng thỏa mã sau kết quả tìm kiếm
        /// </summary>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm kiếm. Nếu không nhập thì đếm toàn bộ</param>
        /// <returns>Tên hoặc địa chỉ cần tìm kiếm. Nếu không nhập thì đếm toàn bộ</returns>
        public int Count(string searchValue = "")
        {
            int count = 0;

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	COUNT(*)
                                    FROM	Suppliers 
                                    WHERE	(@SearchValue = N'')
	                                    OR	(
			                                    (SupplierName LIKE @SearchValue)
			                                    OR (ContactName LIKE @SearchValue)
			                                    OR (Address LIKE @SearchValue)
                                                OR (Phone LIKE @SearchValue)
		                                    )";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                count = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return count;
        }

        /// <summary>
        /// Xóa 1 nhà cung cấp theo ID nhà cung cấp
        /// </summary>
        /// <param name="id">ID nhà cung cấp</param>
        /// <returns>Trả về kết quả true or false</returns>
        public bool Delete(int id)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Suppliers 
                                    WHERE SupplierID = @SupplierID AND NOT EXISTS(SELECT * FROM Products WHERE SupplierID = @SupplierID)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SupplierID", id);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// Lấy thông tin một nhà cung cấp
        /// </summary>
        /// <param name="id">ID nhà cung cấp</param>
        /// <returns>Dữ liệu nhà cung cấp theo ID Nhà cung cấp</returns>
        public Supplier Get(int id)
        {
            Supplier data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Suppliers WHERE SupplierID = @SupplierID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SupplierID", id);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new Supplier()
                    {
                        SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                        SupplierName = Convert.ToString(dbReader["SupplierName"]),
                        ContactName = Convert.ToString(dbReader["ContactName"]),
                        Address = Convert.ToString(dbReader["Address"]),
                        City = Convert.ToString(dbReader["City"]),
                        PostalCode = Convert.ToString(dbReader["PostalCode"]),
                        Country = Convert.ToString(dbReader["Country"]),
                        Phone = Convert.ToString(dbReader["Phone"])
                    };
                }
                cn.Close();
            }
            return data;
        }

        /// <summary>
        /// Kiểm tra ID nhà cung cấp có tồn tại trong bảng dữ liệu khác
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Trả về kết quả true or false</returns>
        public bool InUsed(int id)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT CASE 
                                                WHEN EXISTS(SELECT * FROM Products WHERE SupplierID = @SupplierID) THEN 1 
                                                ELSE 0 
                                            END";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SupplierID", id);

                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
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
            List<Supplier> data = new List<Supplier>();

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM 
                                    (
	                                    SELECT	*, ROW_NUMBER() OVER (ORDER BY SupplierName) AS RowNumber
	                                    FROM	Suppliers 
	                                    WHERE	(@SearchValue = N'')
		                                    OR	(
				                                    (SupplierName LIKE @SearchValue)
			                                     OR (ContactName LIKE @SearchValue)
			                                     OR (Address LIKE @SearchValue)
                                                 OR (Phone LIKE @SearchValue)
			                                    )
                                    ) AS t
                                    WHERE (@PageSize = 0) OR (t.RowNumber BETWEEN (@Page - 1) * @PageSize + 1 AND @Page * @PageSize);";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@Page", page);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Supplier()
                    {
                        SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                        SupplierName = Convert.ToString(dbReader["SupplierName"]),
                        ContactName = Convert.ToString(dbReader["ContactName"]),
                        Address = Convert.ToString(dbReader["Address"]),
                        City = Convert.ToString(dbReader["City"]),
                        PostalCode = Convert.ToString(dbReader["PostalCode"]),
                        Country = Convert.ToString(dbReader["Country"]),
                        Phone = Convert.ToString(dbReader["Phone"])
                    });
                }
                dbReader.Close();
                cn.Close();
            }

            return data;
        }

        /// <summary>
        /// Cập nhật thông tin 1 nhà cung cấp
        /// </summary>
        /// <param name="data">Thông tin nhà cung cấp cần cập nhật</param>
        /// <returns>Trả về kết quả true or false </returns>
        public bool Update(Supplier data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Suppliers
                                    SET SupplierName = @SupplierName, ContactName = @ContactName, Address = @Address, City = @City, PostalCode = @PostalCode, Country = @Country, Phone = @Phone
                                    WHERE SupplierID = @SupplierID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@SupplierName", data.SupplierName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }
}
