using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021201.Web.Controllers
{

    public class TestController : Controller
    {
        [HttpGet]
        public ActionResult Input()
        {
            Person p = new Person()
            {
                Birthdate = new DateTime(1990, 11, 28)
            };
            return View(p);
        }

        [HttpPost]
        public ActionResult Input(Person p)
        {
            var data = new
            {
                Name = p.Name,
                Birthdate = String.Format("{0:MM/dd/yyyy}", p.Birthdate),
                Salary = p.Salary
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public string TestDate(DateTime value)
        {
            DateTime d = value;
            return string.Format("{0:MM/dd/yyyy}", d);
        }
    }

    public class Person
    { 
        public string Name { get; set; }

        public DateTime Birthdate { get; set; } = new DateTime(1001, 01, 01);

        public float Salary { get; set; } = 100;
    }

}