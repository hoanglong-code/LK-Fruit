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
    public class articlesController : Controller
    {
        private LKFRUITEntities db = new LKFRUITEntities();

        // GET: Admin/articles
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var articles = db.articles.Include(a => a.category);
            return View(articles.ToList());
        }

        // GET: Admin/articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            article article = db.articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Admin/articles/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.categories, "id", "name");
            return View();
        }

        // POST: Admin/articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,avatar,title,category_id,author,create_day,articles_content")] article article)
        {
            var f = Request.Files["avatar"];
            if (f != null && f.ContentLength > 0)
            {
                var path = Server.MapPath("../../Areas/Asset/FileUpload/") + f.FileName;
                f.SaveAs(path);
                article.avatar = f.FileName;
            }
            if (ModelState.IsValid)
            {
                db.articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.categories, "id", "name", article.category_id);
            return View(article);
        }

        // GET: Admin/articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            article article = db.articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.categories, "id", "name", article.category_id);
            return View(article);
        }

        // POST: Admin/articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,avatar,title,category_id,author,create_day,articles_content")] article article)
        {
            var f = Request.Files["avatar"];
            if (f != null && f.ContentLength > 0)
            {
                var path = Server.MapPath("../../Areas/Asset/FileUpload/") + f.FileName;
                f.SaveAs(path);
                article.avatar = f.FileName;
            }
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.categories, "id", "name", article.category_id);
            return View(article);
        }

        // GET: Admin/articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            article article = db.articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Admin/articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            article article = db.articles.Find(id);
            db.articles.Remove(article);
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
