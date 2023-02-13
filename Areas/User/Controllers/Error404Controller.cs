using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fruitkha_main.Areas.User.Controllers
{
    public class Error404Controller : Controller
    {
        // GET: User/Error404
        public ActionResult Index()
        {
            return View();
        }
    }
}