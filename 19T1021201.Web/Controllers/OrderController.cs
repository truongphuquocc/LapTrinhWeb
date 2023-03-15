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
    [RoutePrefix("Order")]
    public class OrderController : Controller
    {
        private const string SHOPPING_CART = "ShoppingCart";
        private const string ERROR_MESSAGE = "ErrorMessage";
        private const int PAGE_SIZE = 4;

        /// <summary>
        /// Tìm kiếm, phân trang
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //Lấy điều kiện tìm kiếm từ trong session
            Models.PaginationSearchInputOrder model = Session["ORDER_SEARCH"] as Models.PaginationSearchInputOrder;
            //Nếu không có điều kiện tìm kiếm (có sẵn) thì tạo điều kiện mặc định
            if (model == null)
                model = new Models.PaginationSearchInputOrder()
                {
                    Page = 1,
                    PageSize = 10,
                    SearchValue = "",
                    Status = 0,

                };
            return View(model);
        }

        public ActionResult Search(Models.PaginationSearchInputOrder input)
        {
            int rowCount = 0;
            var data = OrderService.ListOrders(input.Page, input.PageSize, input.Status, input.SearchValue, out rowCount);
            OrderSearchOutput model = new OrderSearchOutput()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                Status = input.Status,
                RowCount = rowCount,
                Data = data
            };

            //Lưu lại điều kiện tìm kiếm vào sessign
            Session["ORDER_SEARCH"] = input;
            //init (cus, employ, time....null) -> Xử lý (cancel, reject, accept) -> shipping hoặc cancel
            //xoa don hang khi o int canel reject
            //nhan vien phu trach chi co nahn vien phu trach don hang do moi xoa dc don hang do
            return View(model);
        }

        /// <summary>
        /// Xem thông tin và chi tiết của đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            var Order = OrderService.GetOrder(id);
            var OrderDetail = OrderService.ListOrderDetails(id);
            if (Order == null)
            {
                return RedirectToAction("Index");
            }
            OrderEditModel model = new OrderEditModel()
            {
                Order = Order,
                OrderDetail = OrderDetail,
            };
            return View(model);
        }
        /// <summary>
        /// Giao diện Thay đổi thông tin chi tiết đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("EditDetail/{orderID}/{productID}")]
        public ActionResult EditDetail(int orderID = 0, int productID = 0)
        {
            if (orderID == 0 || productID == 0)
                return RedirectToAction("Index");
            var model = OrderService.GetOrderDetail(orderID, productID);
            if (model == null)
                return RedirectToAction($"Details/{ orderID}");
            return View(model);
        }
        /// <summary>
        /// Thay đổi thông tin chi tiết đơn hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateDetail(OrderDetail data)
        {
            if (data.Quantity == 0)
                return "Số lượng không được để trống";
            if (data.SalePrice == 0)
                return "Giá bán không được để trống";


            OrderService.SaveOrderDetail(data.OrderID, data.ProductID, data.Quantity, data.SalePrice);

            return "";
        }

        /// <summary>
        /// Xóa 1 chi tiết trong đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("DeleteDetail/{orderID}/{productID}")]
        public ActionResult DeleteDetail(int orderID = 0, int productID = 0)
        {
            var employeeID = Converter.CookieToUserAccount(User.Identity.Name).UserId;
            var order = OrderService.GetOrder(orderID);

            if (employeeID.Equals(order.EmployeeID.ToString()))
                OrderService.DeleteOrderDetail(orderID, productID);         
            return RedirectToAction($"Details/{orderID}");
        }
        /// <summary>
        /// Xóa đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            //TODO: Code chức năng để xóa đơn hàng (nếu được phép xóa)
            var employeeID = Converter.CookieToUserAccount(User.Identity.Name).UserId;
            var order = OrderService.GetOrder(id);
            if(employeeID.Equals(order.EmployeeID.ToString()))
                OrderService.DeleteOrder(id);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Chấp nhận đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Accept(int id = 0)
        {
            //TODO: Code chức năng chấp nhận đơn hàng (nếu được phép)
            if (id == 0)
            {
                return RedirectToAction($"Details/{id}");
            }
            var employeeID = Converter.CookieToUserAccount(User.Identity.Name).UserId;
            var order = OrderService.GetOrder(id);
            if (employeeID.Equals(order.EmployeeID.ToString()))
                OrderService.AcceptOrder(id);
            return RedirectToAction($"Details/{id}");
        }

        /// <summary>
        /// Xác nhận chuyển đơn hàng cho người giao hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Shipping(int id = 0, int shipperID = 0)
        {
            //TODO: Code chức năng chuyển đơn hàng sang trạng thái đang giao hàng (nếu được phép)

            if (Request.HttpMethod == "GET")
            {
                ViewBag.OrderID = id;
                return View();
                //return RedirectToAction();
                //return Content("abc");
                //return Json()
            }

            if (shipperID == 0)
                return Json(ApiResult.CreateFailResult("Vui lòng chọn Shipper"), JsonRequestBehavior.AllowGet);

            bool successs = OrderService.ShipOrder(id, shipperID);
            if (!successs)
                return Json(ApiResult.CreateFailResult("Chuyển trạng thái không thành công"), JsonRequestBehavior.AllowGet);

            return Json(ApiResult.CreateSucessResult());
        }

        /// <summary>
        /// Ghi nhận hoàn tất thành công đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Finish(int id = 0)
        {
            //TODO: Code chức năng ghi nhận hoàn tất đơn hàng (nếu được phép)
            if (id == 0)
            {
                return RedirectToAction($"Details/{id}");
            }
            var employeeID = Converter.CookieToUserAccount(User.Identity.Name).UserId;
            var order = OrderService.GetOrder(id);
            if (employeeID.Equals(order.EmployeeID.ToString()))
                OrderService.FinishOrder(id);
            return RedirectToAction($"Details/{id}");
        }

        /// <summary>
        /// Hủy bỏ đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Cancel(int id = 0)
        {
            //TODO: Code chức năng hủy đơn hàng (nếu được phép)
            if (id == 0)
            {
                return RedirectToAction($"Details/{id}");
            }
            var employeeID = Converter.CookieToUserAccount(User.Identity.Name).UserId;
            var order = OrderService.GetOrder(id);
            if (employeeID.Equals(order.EmployeeID.ToString()))
                OrderService.CancelOrder(id);
            return RedirectToAction($"Details/{id}");
        }

        /// <summary>
        /// Từ chối đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Reject(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction($"Details/{id}");
            }
            var employeeID = Converter.CookieToUserAccount(User.Identity.Name).UserId;
            var order = OrderService.GetOrder(id);
            if (employeeID.Equals(order.EmployeeID.ToString()))
                OrderService.RejectOrder(id);
            return RedirectToAction($"Details/{id}");
        }


        /// <summary>
        /// Sử dụng 1 biến session để lưu tạm giỏ hàng (danh sách các chi tiết của đơn hàng) trong quá trình xử lý.
        /// Hàm này lấy giỏ hàng hiện đang có trong session (nếu chưa có thì tạo mới giỏ hàng rỗng)
        /// </summary>
        /// <returns></returns>
        private List<OrderDetail> GetShoppingCart()
        {
            List<OrderDetail> shoppingCart = Session[SHOPPING_CART] as List<OrderDetail>;
            if (shoppingCart == null)
            {
                shoppingCart = new List<OrderDetail>();
                Session[SHOPPING_CART] = shoppingCart;
            }
            return shoppingCart;
        }
        /// <summary>
        /// Giao diện lập đơn hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.ErrorMessage = TempData[ERROR_MESSAGE] ?? "";
            return View(GetShoppingCart());
        }
        /// <summary>
        /// Tìm kiếm mặt hàng để bổ sung vào giỏ hàng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult SearchProducts(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = ProductDataService.ListProducts(page, PAGE_SIZE, searchValue, 0, 0, out rowCount);
            ViewBag.Page = page;
            return View(data);
        }
        /// <summary>
        /// Bổ sung thêm hàng vào giỏ hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddToCart(OrderDetail data)
        {
            if (data == null)
            {
                TempData[ERROR_MESSAGE] = "Dữ liệu không hợp lệ";
                return RedirectToAction("Create");
            }
            if (data.SalePrice <= 0 || data.Quantity <= 0)
            {
                TempData[ERROR_MESSAGE] = "Giá bán và số lượng không hợp lệ";
                return RedirectToAction("Create");
            }

            List<OrderDetail> shoppingCart = GetShoppingCart();
            var existsProduct = shoppingCart.FirstOrDefault(m => m.ProductID == data.ProductID);

            if (existsProduct == null) //Nếu mặt hàng cần được bổ sung chưa có trong giỏ hàng thì bổ sung vào giỏ
            {

                shoppingCart.Add(data);
            }
            else //Trường hợp mặt hàng cần bổ sung đã có thì tăng số lượng và thay đổi đơn giá
            {
                existsProduct.Quantity += data.Quantity;
                existsProduct.SalePrice = data.SalePrice;
            }
            Session[SHOPPING_CART] = shoppingCart;
            return RedirectToAction("Create");
        }
        /// <summary>
        /// Xóa 1 mặt hàng khỏi giỏ hàng
        /// </summary>
        /// <param name="id">Mã mặt hàng</param>
        /// <returns></returns>
        public ActionResult RemoveFromCart(int id = 0)
        {
            List<OrderDetail> shoppingCart = GetShoppingCart();
            int index = shoppingCart.FindIndex(m => m.ProductID == id);
            if (index >= 0)
                shoppingCart.RemoveAt(index);
            Session[SHOPPING_CART] = shoppingCart;
            return RedirectToAction("Create");
        }
        /// <summary>
        /// Xóa toàn bộ giỏ hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearCart()
        {
            List<OrderDetail> shoppingCart = GetShoppingCart();
            shoppingCart.Clear();
            Session[SHOPPING_CART] = shoppingCart;
            return RedirectToAction("Create");
        }
        /// <summary>
        /// Khởi tạo đơn hàng (với phần thông tin chi tiết của đơn hàng là giỏ hàng đang có trong session)
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Init(int customerID = 0, int employeeID = 0)
        {
            List<OrderDetail> shoppingCart = GetShoppingCart();
            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                TempData[ERROR_MESSAGE] = "Không thể tạo đơn hàng với giỏ hàng trống";
                return RedirectToAction("Create");
            }

            if (customerID == 0 || employeeID == 0)
            {
                TempData[ERROR_MESSAGE] = "Vui lòng chọn khách hàng và nhân viên phụ trách";
                return RedirectToAction("Create");
            }

            int orderID = OrderService.InitOrder(customerID, employeeID, DateTime.Now, shoppingCart);

            Session.Remove(SHOPPING_CART); //Xóa giỏ hàng 

            return RedirectToAction($"Details/{orderID}");
        }
    }
}