using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace fruitkha_main.Areas.User.Controllers
{
    public class IndexController : Controller
    {
        // GET: User/Index
        public ActionResult Index()
        {
                return View();
        }
    }
}