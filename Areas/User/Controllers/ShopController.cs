using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fruitkha_main.Models;
using System.Data;
using System.Data.Entity;

namespace fruitkha_main.Areas.User.Controllers
{
    public class ShopController : Controller
    {
       LKFRUITEntities db = new LKFRUITEntities(); 
        // GET: User/Shop
        public ActionResult Index()
        {
            List<fruitkha_main.Models.product> lst = db.products.ToList();

            return View(lst);
        }
        public ActionResult items()
        {
            List<fruitkha_main.Models.product> lst = db.products.ToList();
            return PartialView();
        }

    }
}