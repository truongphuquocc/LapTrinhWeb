using System;
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
    /// Cài đặt chức năng xử lý dữ liệu liên quan đến mặt hàng
    /// </summary>
    public class ProductDAL : _BaseDAL, IProductDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public ProductDAL(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Product data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"insert into Products(ProductName,SupplierID,CategoryID,Unit,Price,Photo)
                                            values(@ProductName,@SupplierID,@CategoryID,@Unit,@Price,@Photo)
                                             Select @@Identity;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long AddAttribute(ProductAttribute data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"insert into ProductAttributes(ProductID,AttributeName,AttributeValue,DisplayOrder)
                                            values(@ProductID,@AttributeName,@AttributeValue,@DisplayOrder)
                                             Select @@Identity;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long AddPhoto(ProductPhoto data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"insert into ProductPhotos(ProductID,Photo,Description,DisplayOrder,IsHidden)
                                            values(@ProductID,@Photo,@Description,@DisplayOrder,@IsHidden)
                                             Select @@Identity;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="categoryID"></param>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public int Count(string searchValue = "", int categoryID = 0, int supplierID = 0)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            int count = 0;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                if (supplierID == 0 && categoryID == 0)
                {
                    cmd.CommandText = @"select count(*)
                                    from    Products
                                    where  (@searchValue = N'')
                                        or (
                                                (ProductName like @searchValue)
                                              
                                            )";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;

                    //Truyền tham số cho câu truy vấn
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
                else if (supplierID != 0 && categoryID == 0)
                {
                    cmd.CommandText = @"select count(*)
                                    from    Products
                                    where  (@searchValue = N'')
                                        or (
                                              (ProductName like @searchValue)
                                            )
                                        and SupplierID = @SupplierID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;

                    //Truyền tham số cho câu truy vấn
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
                else if (supplierID == 0 && categoryID != 0)
                {
                    cmd.CommandText = @"select count(*)
                                    from    Products
                                    where  (@searchValue = N'')
                                        or (
                                                (ProductName like @searchValue)
                                              
                                            )
                                        and CategoryID = @CategoryID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;

                    //Truyền tham số cho câu truy vấn
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
                else
                {
                    cmd.CommandText = @"select count(*)
                                    from    Products
                                    where  (@searchValue = N'')
                                        or (
                                                (ProductName like @searchValue)
                                              
                                            )
                                        and CategoryID = @CategoryID
                                        and SupplierID = @SupplierID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;

                    //Truyền tham số cho câu truy vấn
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }

                cn.Close();
            }

            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public bool Delete(int productID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Products WHERE ProductID=@productID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@productID", productID);
                result = cmd.ExecuteNonQuery() > 0;
                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        public bool DeleteAttribute(long attributeID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Delete 
                                    from ProductAttributes
                                     where AttributeID = @AttributeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@AttributeID", attributeID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }


            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public bool DeletePhoto(long photoID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Delete 
                                    from ProductPhotos
                                     where PhotoID = @PhotoID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@PhotoID", photoID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }


            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public Product Get(int productID)
        {
            Product data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *
                                    from Products
                                     where ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@ProductID", productID);


                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (result.Read())
                {
                    data = new Product()
                    {
                        ProductID = Convert.ToInt32(result["ProductID"]),
                        ProductName = Convert.ToString(result["ProductName"]),
                        SupplierID = Convert.ToInt32(result["SupplierID"]),
                        CategoryID = Convert.ToInt32(result["CategoryID"]),
                        Unit = Convert.ToString(result["Unit"]),
                        Price = Convert.ToInt32(result["Price"]),
                        Photo = Convert.ToString(result["Photo"])

                    };
                }
                result.Close();
                cn.Close();
            }
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        public ProductAttribute GetAttribute(long attributeID)
        {
            ProductAttribute data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *
                                    from ProductAttributes
                                     where AttributeID = @AttributeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@AttributeID", attributeID);


                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (result.Read())
                {
                    data = new ProductAttribute()
                    {
                        AttributeID = Convert.ToInt32(result["AttributeID"]),
                        ProductID = Convert.ToInt32(result["ProductID"]),
                        AttributeName = Convert.ToString(result["AttributeName"]),
                        AttributeValue = Convert.ToString(result["AttributeValue"]),
                        DisplayOrder = Convert.ToInt32(result["DisplayOrder"])

                    };
                }
                result.Close();
                cn.Close();
            }
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public ProductPhoto GetPhoto(long photoID)
        {
            ProductPhoto data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *
                                    from ProductPhotos
                                     where PhotoID = @PhotoID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@PhotoID", photoID);


                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (result.Read())
                {
                    data = new ProductPhoto()
                    {
                        PhotoID = Convert.ToInt32(result["PhotoID"]),
                        ProductID = Convert.ToInt32(result["ProductID"]),
                        Photo = Convert.ToString(result["Photo"]),
                        Description = Convert.ToString(result["Description"]),
                        DisplayOrder = Convert.ToInt32(result["DisplayOrder"]),
                        IsHidden = Convert.ToBoolean(result["IsHidden"])

                    };
                }
                result.Close();
                cn.Close();
            }
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public bool InUsed(int productID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"select case when exists
                    (
					select ProductID from OrderDetails where ProductID=@productID
                                    ) then 1 else 0 end";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@productID", productID);
                result = Convert.ToBoolean(cmd.ExecuteScalar());
                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="categoryID"></param>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public IList<Product> List(int page = 1, int pageSize = 0, string searchValue = "", int categoryID = 0, int supplierID = 0)
        {
            IList<Product> data = new List<Product>();

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            //Tạo CSDL
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                if (supplierID == 0 && categoryID == 0)
                {
                    cmd.CommandText = @"select *
                                    from
                                        (
                                            select    *,
                                                    row_number() over(order by ProductName) as RowNumber
                                            from    Products
                                            where    (@searchValue = N'')
                                                or (
                                                        (ProductName like @searchValue)
                                                       
                                                    )

                                        ) as t
                                    where (@pageSize = 0) or (t.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
                                    order by t.RowNumber;";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;

                    //Truyền tham số cho câu truy vấn
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                }
                else if (categoryID== 0 && supplierID != 0)
                {
                    cmd.CommandText = @"select *
                                    from
                                        (
                                            select    *,
                                                    row_number() over(order by ProductName) as RowNumber
                                            from    Products
                                            where    (@searchValue = N'')
                                                or (
                                                        (ProductName like @searchValue)
                                                     
                                                    )
                                                  and SupplierID = @SupplierID

                                        ) as t
                                    where (@pageSize = 0) or (t.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
                                    order by t.RowNumber;";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;

                    //Truyền tham số cho câu truy vấn
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                }
                else if (supplierID == 0 && categoryID != 0)
                {
                    cmd.CommandText = @"select *
                                    from
                                        (
                                            select    *,
                                                    row_number() over(order by ProductName) as RowNumber
                                            from    Products
                                            where    (@searchValue = N'')
                                                or (
                                                        (ProductName like @searchValue)
                                                     
                                                    )
                                                  and CategoryID = @CategoryID

                                        ) as t
                                    where (@pageSize = 0) or (t.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
                                    order by t.RowNumber;";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;

                    //Truyền tham số cho câu truy vấn
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                }
                else
                {
                    cmd.CommandText = @"SELECT *
                                        FROM
                                          (SELECT *,
                                                  row_number() over(
                                                                    ORDER BY ProductName) AS RowNumber
                                           FROM Products
                                           WHERE (@searchValue = N'')
                                             OR ((ProductName like @searchValue))
                                             AND SupplierID = @SupplierID
                                             AND CategoryID = @CategoryID ) AS t
                                        WHERE (@pageSize = 0)
                                          OR (t.RowNumber BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize)
                                        ORDER BY t.RowNumber;";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;

                    //Truyền tham số cho câu truy vấn
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                }

                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Product()
                    {
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        ProductName = Convert.ToString(dbReader["ProductName"]),
                        SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        Unit = Convert.ToString(dbReader["Unit"]),
                        Price = Convert.ToInt32(dbReader["Price"]),
                        Photo = Convert.ToString(dbReader["Photo"])

                    });
                }
                dbReader.Close();
                cn.Close();
            }
            return data;
        }

        public IList<ProductAttribute> ListAttributes(int productID)
        {
            IList<ProductAttribute> data = new List<ProductAttribute>();


            //Tạo CSDL
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *
                                    from ProductAttributes where ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ProductID", productID);


                SqlDataReader result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (result.Read())
                {
                    data.Add(new ProductAttribute()
                    {

                        AttributeID = Convert.ToInt32(result["AttributeID"]),
                        ProductID = Convert.ToInt32(result["ProductID"]),
                        AttributeName = Convert.ToString(result["AttributeName"]),
                        AttributeValue = Convert.ToString(result["AttributeValue"]),
                        DisplayOrder = Convert.ToInt32(result["DisplayOrder"])
                    });
                }
                result.Close();
                cn.Close();
            }


            return data;
        }

        public IList<ProductPhoto> ListPhotos(int productID)
        {
            IList<ProductPhoto> data = new List<ProductPhoto>();


            //Tạo CSDL
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *
                                    from ProductPhotos where ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ProductID", productID);


                SqlDataReader result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (result.Read())
                {
                    data.Add(new ProductPhoto()
                    {
                        PhotoID = Convert.ToInt32(result["PhotoID"]),
                        ProductID = Convert.ToInt32(result["ProductID"]),
                        Photo = Convert.ToString(result["Photo"]),
                        Description = Convert.ToString(result["Description"]),
                        DisplayOrder = Convert.ToInt32(result["DisplayOrder"]),
                        IsHidden = Convert.ToBoolean(result["IsHidden"])
                    });
                }
                result.Close();
                cn.Close();
            }


            return data;
        }

        public bool Update(Product data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Update Products
                                    Set ProductName = @ProductName,
                                    SupplierID = @SupplierID,
                                    CategoryID = @CategoryID,
                                    Unit = @Unit,
                                    Photo = @Photo,
                                    Price = @Price
                                    WHERE ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        public bool UpdateAttribute(ProductAttribute data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Update ProductAttributes
                                    Set AttributeName = @AttributeName,
                                    AttributeValue = @AttributeValue,
                                    DisplayOrder = @DisplayOrder
                                    WHERE AttributeID = @AttributeID and ProductID = @ProductID ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@AttributeID", data.AttributeID);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        public bool UpdatePhoto(ProductPhoto data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Update ProductPhotos
                                    Set Photo = @Photo,
                                    Description = @Description,
                                    DisplayOrder = @DisplayOrder
                                    WHERE PhotoID = @PhotoID and ProductID = @ProductID ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);
                cmd.Parameters.AddWithValue("@PhotoID", data.PhotoID);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }
}
