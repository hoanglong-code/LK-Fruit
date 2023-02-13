using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fruitkha_main.Models;


namespace fruitkha_main.Areas.User.Controllers
{
    public class NewsController : Controller
    {
        LKFRUITEntities db = new LKFRUITEntities();
        // GET: User/News
        public ActionResult Index()
        {
            List<fruitkha_main.Models.article> lst = db.articles.ToList();
            return View(lst);
        }
        

    }
}