using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fruitkha_main.Models;

namespace fruitkha_main.Areas.Admin.Controllers
{
    public class productsController : Controller
    {
        private LKFRUITEntities db = new LKFRUITEntities();

        // GET: Admin/products
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int page = 1;
            if (Request["page"] != null)
            {
                page = Convert.ToInt32(Request["page"]);
            }
            int limit = 10;
            int total = db.products.Count();
            int totalPage = (int)Math.Ceiling(total * 1.0 / limit);
            int skip = limit * (page - 1);
            List<fruitkha_main.Models.product> lst = db.products.OrderBy(x => x.id).Skip(skip).Take(limit).ToList();
            ViewBag.totalPage = totalPage;
            ViewBag.page = page;

            int left = page - 2;
            int right = page + 2;
            List<string> list_page = new List<string>();
            for (int i = 1; i <= totalPage; i++)
            {
                if (i == 1 || i == totalPage || i == page || (left <= i && i <= right))
                {
                    list_page.Add("" + i);
                }
                else if (i == left - 1 || i == right + 1)
                {
                    list_page.Add("...");
                }
            }
            ViewBag.list_page = list_page;

            return View(lst);

        }

        // GET: Admin/products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/products/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.categories, "id", "name");
            ViewBag.supplier_id = new SelectList(db.suppliers, "id", "name");
            return View();
        }

        // POST: Admin/products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,category_id,avatar,origin,price,supplier_id,detail,sale")] product product)
        {
            var f = Request.Files["avatar"];
            if (f != null && f.ContentLength > 0)
            {
                var path = Server.MapPath("../../Areas/Asset/FileUpload/") + f.FileName;
                f.SaveAs(path);
                product.avatar = f.FileName;
            }
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.categories, "id", "name", product.category_id);
            ViewBag.supplier_id = new SelectList(db.suppliers, "id", "name", product.supplier_id);
            return View(product);
        }

        // GET: Admin/products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.categories, "id", "name", product.category_id);
            ViewBag.supplier_id = new SelectList(db.suppliers, "id", "name", product.supplier_id);
            return View(product);
        }

        // POST: Admin/products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,category_id,avatar,origin,price,supplier_id,detail,sale")] product product)
        {
            var f = Request.Files["avatar"];
            if (f != null && f.ContentLength > 0)
            {
                var path = Server.MapPath("../../Areas/Asset/FileUpload/") + f.FileName;
                f.SaveAs(path);
                product.avatar = f.FileName;
            }
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.categories, "id", "name", product.category_id);
            ViewBag.supplier_id = new SelectList(db.suppliers, "id", "name", product.supplier_id);
            return View(product);
        }

        // GET: Admin/products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
