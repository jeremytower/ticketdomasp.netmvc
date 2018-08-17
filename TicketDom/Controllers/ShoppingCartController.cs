using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketDom.Models;

namespace TicketDom.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Cart()
        {
            int cartId = Convert.ToInt32(System.Web.HttpContext.Current.Session["cartId"]);
            using (TicketDomContext context = new TicketDomContext())
            {
                ShoppingCart cart = context.ShoppingCart.FirstOrDefault(x => x.Id == cartId);
                return View(cart);
            }
            
        }
    }
}