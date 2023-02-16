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
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 8;
        private const string EMPLOYEE_SEARCH = "SearchEmployeeCondition";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Employee
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[EMPLOYEE_SEARCH] as PaginationSearchInput;

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
            var data = CommonDataService.ListOfEmployees(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var reault = new EmployeeSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[EMPLOYEE_SEARCH] = condition;



            return View(reault);
        }

        /// <summary>
        /// Giao diện để bổ sung nhân viên mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Employee()
            {
                EmployeeID = 0
            };
            ViewBag.Title = "Bổ sung nhân viên";
            return View("Edit", data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            int employeeId = Convert.ToInt32(id);

            var data = CommonDataService.GetEmployee(employeeId);
            ViewBag.Title = "Sửa đổi nhân viên";
            return View(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Employee data)
        {

            if (data.EmployeeID == 0)
            {
                CommonDataService.AddEmployee(data);
            }
            else
            {
                CommonDataService.UpdateEmployee(data);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            int employeeId = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetEmployee(employeeId);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteEmployee(employeeId);
                return RedirectToAction("Index");
            }
        }
    }
}