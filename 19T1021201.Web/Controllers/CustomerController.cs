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
    public class CustomerController : Controller
    {
        private const int PAGE_SIZE = 8;
        private const string CUSTOMER_SEARCH = "SearchCustomerCondition";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Customer
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[CUSTOMER_SEARCH] as PaginationSearchInput;

            if (condition == null)
            {
                condition = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }

            return View(condition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCustomer(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var reault = new CustomerSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[CUSTOMER_SEARCH] = condition;



            return View(reault);
        }

        /// <summary>
        /// Giao diện để bổ sung khách hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Customer()
            {
                CustomerID = 0
            };
            ViewBag.Title = "Bổ sung khách hàng";
            return View("Edit", data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            if (id == 0)
                return RedirectToAction("Index");

            var data = CommonDataService.GetCustomer(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Sửa đổi khách hàng";
            return View(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.CustomerName))
                    ModelState.AddModelError("CustomerName", "Tên khách hàng không được để trống");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống");
                if (string.IsNullOrWhiteSpace(data.Country))
                    ModelState.AddModelError("Country", "Tên Quốc gia không được để trống");
                if (string.IsNullOrWhiteSpace(data.Address))
                    ModelState.AddModelError("Address", "Tên Địa chỉ không được để trống");

                if (string.IsNullOrWhiteSpace(data.City))
                    data.City = "";
                if (string.IsNullOrWhiteSpace(data.PostalCode))
                    data.PostalCode = "";

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.CustomerID == 0 ? "Bổ sung khách hàng" : "Cập nhật khách hàng";
                    return View("Edit", data);
                }
                if (data.CustomerID == 0)
                {
                    CommonDataService.AddCustomer(data);
                }
                else
                {
                    CommonDataService.UpdateCustomer(data);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Có lỗi xãy ra. Vui lòng thử lại sau!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            if (id == 0)
                return RedirectToAction("Index");
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetCustomer(id);
                if(data == null)
                    return RedirectToAction("Index");
                return View(data);
            }
            else
            {
                CommonDataService.DeleteCustomer(id);
                return RedirectToAction("Index");
            }
        }
    }
}