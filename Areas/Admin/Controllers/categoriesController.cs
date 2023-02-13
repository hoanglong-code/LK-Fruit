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
    public class categoriesController : Controller
    {
        private LKFRUITEntities db = new LKFRUITEntities();

        // GET: Admin/categories
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var categories = db.categories.Include(c => c.admin);
            return View(categories.ToList());
        }

        // GET: Admin/categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/categories/Create
        public ActionResult Create()
        {
            ViewBag.author_id = new SelectList(db.admins, "id", "avatar");
            return View();
        }

        // POST: Admin/categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,avatar,total,author_id")] category category)
        {
            var f = Request.Files["avatar"];
            if (f != null && f.ContentLength > 0)
            {
                var path = Server.MapPath("../../Areas/Asset/FileUpload/") + f.FileName;
                f.SaveAs(path);
                category.avatar = f.FileName;
            }
            if (ModelState.IsValid)
            {
                db.categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.author_id = new SelectList(db.admins, "id", "avatar", category.author_id);
            return View(category);
        }

        // GET: Admin/categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.author_id = new SelectList(db.admins, "id", "avatar", category.author_id);
            return View(category);
        }

        // POST: Admin/categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,avatar,total,author_id")] category category)
        {
            var f = Request.Files["avatar"];
            if (f != null && f.ContentLength > 0)
            {
                var path = Server.MapPath("../../Areas/Asset/FileUpload/") + f.FileName;
                f.SaveAs(path);
                category.avatar = f.FileName;
            }
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.author_id = new SelectList(db.admins, "id", "avatar", category.author_id);
            return View(category);
        }

        // GET: Admin/categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            category category = db.categories.Find(id);
            db.categories.Remove(category);
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
