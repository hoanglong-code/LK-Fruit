using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fruitkha_main.Models;


namespace fruitkha_main.Areas.User.Controllers
{
    public class CartController : Controller
    {
        // GET: User/Cart
        Models.LKFRUITEntities db = new Models.LKFRUITEntities();
        public ActionResult Index()
        {
            List<ShoppingCart> lst = getCart();
            Session["price"] = TinhGiaThanh();
            ViewBag.price = TinhGiaThanh();
            Session["qty"] = TinhSoLuong();
            ViewBag.qty = TinhSoLuong();
            ViewBag.total = TinhTongTien();
            return View(lst);
        }
        public ActionResult CheckOut()
        {
            List<ShoppingCart> lst = getCart();
            Session["price"] = TinhGiaThanh();
            ViewBag.price = TinhGiaThanh();
            Session["qty"] = TinhSoLuong();
            ViewBag.qty = TinhSoLuong();
            ViewBag.total = TinhTongTien();
            return View(lst);
        }
        public int TinhGiaThanh()
        {
            List<ShoppingCart> lst = Session["Cart"] as List<ShoppingCart>;
            if (lst == null)
            {
                return 0;
            }
            return (int)lst.Sum(x => x.price);
        }
        public int TinhSoLuong()
        {
            List<ShoppingCart> lst = Session["Cart"] as List<ShoppingCart>;
            if (lst == null)
            {
                return 0;
            }
            return (int)lst.Sum(x => x.qty);
        }
        public int TinhTongTien()
        {
            List<ShoppingCart> lst = Session["Cart"] as List<ShoppingCart>;
            if (lst == null)
            {
                return 0;
            }
            return (int)lst.Sum(x => x.total);
        }
        public List<ShoppingCart> getCart()
        {
            //nếu giỏ hàng đã tồn tại
            List<ShoppingCart> lst = Session["Cart"] as List<ShoppingCart>;
            if (lst == null)
            {
                //Nếu session giỏ hàng ko tồn tại thì khởi tạo giỏ hàng
                lst = new List<ShoppingCart>();
                Session["Cart"] = lst;
            }
            return lst;
        }
        public ActionResult AddToCart(int id)
        {
            product prd = db.products.SingleOrDefault(x => x.id == id);
            if (prd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<ShoppingCart> lst = getCart();
            ShoppingCart item = lst.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                item.qty++;
                item.total = item.qty * item.price;
                return Redirect("~/user/Cart/Index");
            }
            int qty = int.Parse(Request["qty"]);                                            
            ShoppingCart itC = new ShoppingCart(id, qty);
            lst.Add(itC);
            return Redirect("~/User/Cart/Index");
        }       
        public ActionResult DeleteCart(int id)
        {
            List<ShoppingCart> lst = getCart();
            ShoppingCart item = lst.SingleOrDefault(x => x.id == id);
            lst.Remove(item);
            return Redirect("~/User/Cart/Index");
        }
    }
}