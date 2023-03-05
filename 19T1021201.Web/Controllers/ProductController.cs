using _19T1021201.BusinessLayers;
using _19T1021201.DomainModels;
using _19T1021201.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021201.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        private const int PAGE_SIZE = 8;
        /// <summary>
        /// Tìm kiếm, hiển thị mặt hàng dưới dạng phân trang
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //trong session luôn lưu điều kiện tìm kiếm vừa thực hiện
            PaginationSearchInputProduct model = Session["PRODUCT_SEARCH"] as PaginationSearchInputProduct;
            //Nếu không có điều kiện tìm kiếm thì tạo điều kiện mặc định
            if (model == null)
            {
                model = new PaginationSearchInputProduct()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                    SupplierID = 0,
                    CategoryID = 0

                };
            }
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(Models.PaginationSearchInputProduct condition)
        {
            int rowCount = 0;
            var data = ProductDataService.ListProducts(condition.Page, condition.PageSize, condition.SearchValue, condition.CategoryID, condition.SupplierID, out rowCount);

            var model = new ProductPaginationResult()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                SupplierID = condition.SupplierID,
                CategoryID = condition.CategoryID,
                RowCount = rowCount,
                Data = data

            };
            Session["PRODUCT_SEARCH"] = condition;
            return View(model);
        }


        /// <summary>
        /// Tạo mặt hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Product model = new Product()
            {
                ProductID = 0
            };

            return View(model);
        }
        /// <summary>
        /// Cập nhật thông tin mặt hàng, 
        /// Hiển thị danh sách ảnh và thuộc tính của mặt hàng, điều hướng đến các chức năng
        /// quản lý ảnh và thuộc tính của mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>  
        [Route("edit/{productID}")]
        public ActionResult Edit(int productID)
        {
            if (productID == 0)
                return RedirectToAction("Index");
            Product Product = ProductDataService.GetProduct(productID);
            var DataPhoto = ProductDataService.ListPhotos(productID);
            var DataAttribute = ProductDataService.ListAttributes(productID);
            if (Product == null)
                return RedirectToAction("Index");
            ProductEditModel model = new Models.ProductEditModel()
            {
                Product = Product,
                DataPhoto = DataPhoto,
                DataAttribute = DataAttribute
            };
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="uploadPhoto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save/{productID}")]
        public ActionResult Save(Product model, HttpPostedFileBase uploadPhoto)
        {
            if (string.IsNullOrWhiteSpace(model.ProductName))
                ModelState.AddModelError("ProductName", "Tên sản phẫm không được bổ trống");
            if (string.IsNullOrWhiteSpace(model.Unit))
                ModelState.AddModelError("Unit", "Đơn vị tính không được bỏ trống");
            if (model.CategoryID == 0)
                ModelState.AddModelError("CategoryID", "Vui lòng chọn danh mục");
            if (model.SupplierID == 0)
                ModelState.AddModelError("SupplierID", "Vui lòng chọn nhà cung cấp");
            if (string.IsNullOrWhiteSpace(model.Price.ToString()))
                ModelState.AddModelError("Price", "Gía không được bổ trống");

            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Product");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string uploadFilePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(uploadFilePath);
                model.Photo = $"/Images/Product/{fileName}";

            }
            else
                ModelState.AddModelError("Photo", "Phải chọn ảnh");


            // Nếu dữ liệu đầu vào không hợp lệ thì trả lại giao diện và thông báo lỗi
            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }

            ProductDataService.AddProduct(model);
            PaginationSearchInputProduct input = Session["PRODUCT_SEARCH"] as PaginationSearchInputProduct;
            input.SearchValue = model.ProductName;
            input.SupplierID = model.SupplierID;
            input.CategoryID = model.CategoryID;
            Session["PRODUCT_SEARCH"] = input;

            ViewBag.mess = model.ProductName;

            return RedirectToAction("Edit", model.ProductID);
        }

        public ActionResult update(ProductEditModel model, HttpPostedFileBase uploadPhoto)
        {
            if (string.IsNullOrWhiteSpace(model.Product.ProductName))
                ModelState.AddModelError("ProductName", "Tên sản phẫm không được bổ trống");
            if (string.IsNullOrWhiteSpace(model.Product.Unit))
                ModelState.AddModelError("Unit", "Đơn vị tính không được bỏ trống");
            if (model.Product.CategoryID == 0)
                ModelState.AddModelError("CategoryID", "Vui lòng chọn danh mục");
            if (model.Product.SupplierID == 0)
                ModelState.AddModelError("SupplierID", "Vui lòng chọn nhà cung cấp");
            if (string.IsNullOrWhiteSpace(model.Product.Price.ToString()))
                ModelState.AddModelError("Price", "Gía không được bổ trống");

            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/images/products");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string uploadFilePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(uploadFilePath);
                model.Product.Photo = $"/images/products/{fileName}";

            }
            else
            {
                model.Product.Photo = ProductDataService.GetProduct(model.Product.ProductID).Photo;
            }
            // Nếu dữ liệu đầu vào không hợp lệ thì trả lại giao diện và thông báo lỗi
            if (!ModelState.IsValid)
            {
                var DataPhoto = ProductDataService.ListPhotos(model.Product.ProductID);
                var DataAttribute = ProductDataService.ListAttributes(model.Product.ProductID);

                Models.ProductEditModel modelProduct = new Models.ProductEditModel()
                {
                    Product = model.Product,
                    DataPhoto = DataPhoto,
                    DataAttribute = DataAttribute
                };
                return View("Edit", modelProduct);
            }

            ProductDataService.UpdateProduct(model.Product);
            PaginationSearchInputProduct input = Session["PRODUCT_SEARCH"] as PaginationSearchInputProduct;
            input.SearchValue = model.Product.ProductName;
            input.CategoryID = model.Product.CategoryID;
            input.SupplierID = model.Product.SupplierID;
            Session["PRODUCT_SEARCH"] = input;
            return RedirectToAction("Index");
        }

        [Route("delete/{productID}")]
        public ActionResult Delete(int productID)
        {
            var dataPhoto = ProductDataService.ListPhotos(productID);
            var dataAttribute = ProductDataService.ListAttributes(productID);
            if (Request.HttpMethod == "POST")
            {
                if(!ProductDataService.InUsedProduct(productID))
                {
                    foreach (var item in dataPhoto)
                        ProductDataService.DeletePhoto(item.PhotoID);
                    foreach (var item in dataAttribute)
                        ProductDataService.DeleteAttribute(item.AttributeID);
                    ProductDataService.DeleteProduct(productID);
                }    
                
                return RedirectToAction("Index");
            }

            var model = ProductDataService.GetProduct(productID);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        /// <summary>
        /// Các chức năng quản lý ảnh của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method?}/{productID?}/{photoID?}")]
        public ActionResult Photo(string method, int productID = 0, long photoID = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung ảnh";
                    ProductPhoto model = new ProductPhoto()
                    {
                        PhotoID = 0,
                        ProductID = productID
                    };
                    return View(model);

                case "edit":
                    ViewBag.Title = "Thay đổi ảnh";
                    ProductPhoto model2 = ProductDataService.GetPhoto(photoID);
                    return View(model2);
                case "delete":
                    ProductDataService.DeletePhoto(photoID);
                    return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Route("PhoToRevise/{productID}")]
        public ActionResult PhoToRevise(ProductPhoto model, HttpPostedFileBase uploadPhoto, int productID)
        {
            Product Product = ProductDataService.GetProduct(productID);
            var DataPhoto = ProductDataService.ListPhotos(productID);
            var DataAttribute = ProductDataService.ListAttributes(productID);

            if (string.IsNullOrWhiteSpace(model.Description))
                ModelState.AddModelError("Description", "Mô tả không được để trống");
            //if (string.IsNullOrWhiteSpace(model.DisplayOrder.ToString()))
            // ModelState.AddModelError("DisplayOrder", "Mô tả không được để trống");
            //if (string.IsNullOrWhiteSpace(model.IsHidden.ToString()))
            // ModelState.AddModelError("IsHidden", "Thứ tự không được để trống");

            Models.ProductEditModel modelProduct = new Models.ProductEditModel()
            {
                Product = Product,
                DataPhoto = DataPhoto,
                DataAttribute = DataAttribute
            };

            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Product");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string uploadFilePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(uploadFilePath);
                model.Photo = $"/Images/Product/{fileName}";

            }
            if (string.IsNullOrWhiteSpace(model.Photo))
                ModelState.AddModelError("Photo", "Ảnh không được để trống");
            if (!ModelState.IsValid)
            {

                return View("Photo", model);
            }
            if (model.IsHidden)
                model.IsHidden = true;
            if (model.PhotoID > 0)
                ProductDataService.UpdatePhoto(model);
            else
                ProductDataService.AddPhoto(model);

            return RedirectToAction("Edit");
        }

        /// <summary>
        /// Các chức năng quản lý thuộc tính của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method?}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method = "add", int productID = 0, int attributeID = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung thuộc tính";
                    ProductAttribute model = new ProductAttribute()
                    {
                        AttributeID = 0,
                        ProductID = productID
                    };
                    return View(model);
                case "edit":
                    ProductAttribute model2 = ProductDataService.GetAttribute(attributeID);
                    return View(model2);
                case "delete":
                    ProductDataService.DeleteAttribute(attributeID);
                    return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }

        [HttpPost]
        
        [Route("AttributeProduct/{productID}")]
        public ActionResult AttributeProduct(ProductAttribute model, int productID)
        {
            Product Product = ProductDataService.GetProduct(productID);
            var DataPhoto = ProductDataService.ListPhotos(productID);
            var DataAttribute = ProductDataService.ListAttributes(productID);

            Models.ProductEditModel product = new Models.ProductEditModel()
            {
                Product = Product,
                DataPhoto = DataPhoto,
                DataAttribute = DataAttribute
            };

            if (string.IsNullOrWhiteSpace(model.ProductID.ToString()))
            {
                ModelState.AddModelError("ProductID", "Tên không được để trống");
            }
            if (string.IsNullOrWhiteSpace(model.AttributeName))
            {
                ModelState.AddModelError("AttributeName", "Tên thuộc tính không được để trống");
            }
            if (string.IsNullOrWhiteSpace(model.AttributeValue))
            {
                ModelState.AddModelError("AttributeValue", "Giá trị không được để trống");
            }
            if (string.IsNullOrWhiteSpace(model.DisplayOrder.ToString()))
            {
                ModelState.AddModelError("DisplayOrder", "Thứ tự không được để trống");
            }

            if (!ModelState.IsValid)
            {

                return View("Attribute", model);
            }

            if (model.AttributeID > 0)
                ProductDataService.UpdateAttribute(model);
            else
                ProductDataService.AddAttribute(model);

            return RedirectToAction("Edit");
        }
    }
}