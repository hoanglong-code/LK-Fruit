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
    public class ordersController : Controller
    {
        private LKFRUITEntities db = new LKFRUITEntities();

        // GET: Admin/orders
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var orders = db.orders.Include(o => o.bill).Include(o => o.product).Include(o => o.user);
            return View(orders.ToList());
        }

        // GET: Admin/orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/orders/Create
        public ActionResult Create()
        {
            ViewBag.bill_id = new SelectList(db.bills, "id", "address");
            ViewBag.prd_id = new SelectList(db.products, "id", "name");
            ViewBag.us_id = new SelectList(db.users, "id", "username");
            return View();
        }

        // POST: Admin/orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,bill_id,us_id,prd_id,total,price,old_price,sale,qty,avatar")] order order)
        {
            var f = Request.Files["avatar"];
            if (f != null && f.ContentLength > 0)
            {
                var path = Server.MapPath("../../Areas/Asset/FileUpload/") + f.FileName;
                f.SaveAs(path);
                order.avatar = f.FileName;
            }
            if (ModelState.IsValid)
            {
                db.orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bill_id = new SelectList(db.bills, "id", "address", order.bill_id);
            ViewBag.prd_id = new SelectList(db.products, "id", "name", order.prd_id);
            ViewBag.us_id = new SelectList(db.users, "id", "username", order.us_id);
            return View(order);
        }

        // GET: Admin/orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.bill_id = new SelectList(db.bills, "id", "address", order.bill_id);
            ViewBag.prd_id = new SelectList(db.products, "id", "name", order.prd_id);
            ViewBag.us_id = new SelectList(db.users, "id", "username", order.us_id);
            return View(order);
        }

        // POST: Admin/orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,bill_id,us_id,prd_id,total,price,old_price,sale,qty,avatar")] order order)
        {
            var f = Request.Files["avatar"];
            if (f != null && f.ContentLength > 0)
            {
                var path = Server.MapPath("../../Areas/Asset/FileUpload/") + f.FileName;
                f.SaveAs(path);
                order.avatar = f.FileName;
            }
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bill_id = new SelectList(db.bills, "id", "address", order.bill_id);
            ViewBag.prd_id = new SelectList(db.products, "id", "name", order.prd_id);
            ViewBag.us_id = new SelectList(db.users, "id", "username", order.us_id);
            return View(order);
        }

        // GET: Admin/orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order order = db.orders.Find(id);
            db.orders.Remove(order);
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
