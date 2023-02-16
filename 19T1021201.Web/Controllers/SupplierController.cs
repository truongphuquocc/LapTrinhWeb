using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021201.DomainModels;
using _19T1021201.BusinessLayers;
using _19T1021201.Web.Models;

namespace _19T1021201.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SupplierController : Controller
    {

        private const int PAGE_SIZE = 8;
        private const string SUPPLIER_SEARCH = "SearchSupplierCondition";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Supplier
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[SUPPLIER_SEARCH] as PaginationSearchInput;

            if(condition == null)
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
            var data = CommonDataService.ListOfSupplier(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var reault = new SupplierSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[SUPPLIER_SEARCH] = condition;



            return View(reault);
        }

        /// <summary>
        /// Giao diện để bổ sung nhà cung cấp mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Supplier()
            {
                SupplierID = 0
            };
            ViewBag.Title = "Bổ sung nhà cung cấp";
            return View("Edit", data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            int supplierId = Convert.ToInt32(id);

            var data = CommonDataService.GetSupplier(supplierId);
            ViewBag.Title = "Sửa đổi nhà cung cấp";
            return View(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Supplier data)
        {
            if(data.SupplierID == 0)
            {
                CommonDataService.AddSupplier(data);
            }
            else
            {
                CommonDataService.UpdateSupplier(data);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            int supplierID = Convert.ToInt32(id);
            if(Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetSupplier(supplierID);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteSupplier(supplierID);
                return RedirectToAction("Index");
            }
        }
    }
}