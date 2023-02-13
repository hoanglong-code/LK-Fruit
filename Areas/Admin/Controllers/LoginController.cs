using fruitkha_main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace fruitkha_main.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin.Data.DTO.Class2 user, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra tài khoản và mật khẩu có khớp trong CSDL hay không?
                Models.LKFRUITEntities db = new Models.LKFRUITEntities();
                Models.admin _user = db.admins.FirstOrDefault(x => x.username == user.UserName && x.password == user.Passwords);
                if (_user == null)
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
                }
                else
                {
                    // Lưu trạng thái đăng nhập của người dùng vào Session
                    Session["user"] = _user;
                    // Lưu trạng thái đăng nhập (đã mã hóa) của người dùng vào Cookie, sử dụng với ActionFilter Authorized
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    if (ReturnUrl != null)
                    {
                        return Redirect(ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(user);          
        }
        public ActionResult Logout()
        {
            // Hủy cookie đã lưu dưới Client
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}