﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fruitkha_main.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {          
                if (Session["user"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                return View();
        }
    }
}