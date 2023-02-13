using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fruitkha_main.Models;

namespace fruitkha_main.Areas.User.Controllers
{
    public class Single_ProductController : Controller
    {
        LKFRUITEntities db = new LKFRUITEntities();
        // GET: User/Single_Product
        public ActionResult Index(int id)
        {
            List<fruitkha_main.Models.product> lst = db.products.Where(x => x.id == id).ToList();
            return View(lst);
        }
    }
}