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
    public class SearchController : Controller
    {
        // GET: User/Search
        LKFRUITEntities db = new LKFRUITEntities();
        public ActionResult Index()
        {
            string search = Request["search"];
            List<fruitkha_main.Models.product> lst = db.products.Where(x => x.name.Contains(search)).ToList();
            return View(lst);
        }
    }
}