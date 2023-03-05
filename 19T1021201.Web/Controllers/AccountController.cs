using _19T1021201.BusinessLayers;
using _19T1021201.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _19T1021201.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string userName = "", string password = "")
        {

            ViewBag.UserName = userName;
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {

                ModelState.AddModelError("", "vui lòng nhập đầy đủ thông tin");
                return View();
            }

            var userAccount = UserAccountService.Authorize(AccountTypes.Employee, userName, password);
            if (userAccount == null)
            {
                ModelState.AddModelError("", "tài khoản hoặc mật khẩu không đúng");
                return View();
            }


            string cookivalue = Newtonsoft.Json.JsonConvert.SerializeObject(userAccount);

            FormsAuthentication.SetAuthCookie(cookivalue, false);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ChangePassword(string userName, string oldPassword, string newPassword="", string confirmPassword = "")
        {

            var model = Converter.CookieToUserAccount(User.Identity.Name);
            userName = model.Email;

            ViewBag.oldPassword = oldPassword;
            ViewBag.newPassword = newPassword;
            ViewBag.confirmPassword = confirmPassword;

            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(oldPassword))
            {

                ViewBag.Message = "Các trường không được để trống";

            }


            else if (oldPassword.Equals(model.Password))
            {
                if (newPassword.Equals(confirmPassword))
                {
                    UserAccountService.ChangePassword(AccountTypes.Employee, userName, oldPassword, newPassword);
                    ViewBag.Message = "Cập nhật thành công";
                    var userAccount = UserAccountService.Authorize(AccountTypes.Employee, userName, newPassword);
                    string cookivalue = Newtonsoft.Json.JsonConvert.SerializeObject(userAccount);
                    FormsAuthentication.SetAuthCookie(cookivalue, false);
                }
                else
                {
                    ViewBag.Message = "Xác nhận mật khẩu không đúng";
                }
            }
            else
            {
                ViewBag.Message = "Mật khẩu cũ không đúng";
            }


            return View();

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}