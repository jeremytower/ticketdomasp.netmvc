using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketDom.Models;

namespace TicketDom.Controllers
{
    public class CartItemController : Controller
    {
        // GET: CartItem
        public ActionResult Cart()
        {
            if (System.Web.HttpContext.Current.Session["cartId"] != null)
            {
                using (TicketDomContext context = new TicketDomContext())
                {
                    var list = context.CartItem.ToList();
                    var results = new List<CartItem>();
                    int x = Convert.ToInt32(System.Web.HttpContext.Current.Session["cartId"]);
                    foreach (CartItem item in list)
                    {
                        if (item.CartId == x)
                        {
                            results.Add(item);
                        }

                    }

                    return View(results);
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Remove(int id)
        {
            try
            {
                using (TicketDomContext context = new TicketDomContext())
                {
                    var item = context.CartItem.FirstOrDefault(x => x.Id == id);


                    context.CartItem.Remove(item);
                    context.SaveChanges();

                }

                return RedirectToAction("Cart");
            }
            catch
            {
                return View("Cart");
            }
        }

        public ActionResult Clear()
        {
            Session.Abandon();
            return RedirectToAction("Cart");
        }
        public ActionResult Checkout(double Total)
        {
            ViewBag.Total = Total;
            Session.Abandon();
            return View();
        }
    }
}