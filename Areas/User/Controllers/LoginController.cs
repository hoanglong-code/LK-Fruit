using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using fruitkha_main.Models;
using System.Text;

namespace fruitkha_main.Areas.User.Controllers
{
    public class LoginController : Controller
    {
        LKFRUITEntities db = new LKFRUITEntities();
        // GET: User/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Data.DTO.Class4 user)
        {
            if (ModelState.IsValid)
            {
                int isVal = Data.DAO.Class3.CheckLogin(user.UserName, user.Passwords);

                if (isVal == 1)
                {
                    string ReturnUrl = "";
                    Session["UserName"] = user.UserName;
                    List<Models.user> num = db.users.Where(x => x.username == user.UserName).ToList();
                    int id = 0;
                    string img = "";
                    foreach (var item in num)
                    {
                        id = item.id;
                        img = item.avatar;
                    }
                    ViewBag.id = num;
                    Session["id"] = id;
                    FormsAuthentication.SetAuthCookie(user.Passwords, false);
                    if (Request.QueryString["ReturnUrl"] == null)
                        ReturnUrl = "~/User/Index/Index";
                    else ReturnUrl = Request.QueryString["ReturnUrl"].ToString();
                    return Redirect(ReturnUrl);
                }
                else
                {
                    // error
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
                }
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            // Hủy session đã lưu dưới Client
            Session.Remove("id");
            Session.Remove("userName");
            return Redirect("~/User/Index/Index");
        }
        public ActionResult SignUp()
        {
            return View();
        }       
    }
}