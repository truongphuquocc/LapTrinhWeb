using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021201.Web.Controllers
{
    [RoutePrefix("thu-nghiem")]
    public class TestController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
       [Route("xin-chao/{name}/{age?}")]
       public string SayHello(string name, int age=10)
        {
            return $"hello { name}, {age} years old";
        }
    }
}