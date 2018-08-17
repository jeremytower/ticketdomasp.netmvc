using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketDom.Models;


namespace TicketDom.Controllers
{
    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            base.OnActionExecuting(filterContext);
        }
    }

    public class TicketsController : Controller
    {


        // GET: Tickets
        public ActionResult Index()
        {
            using (TicketDomContext context = new TicketDomContext())
            {
                var list = context.Tickets.OrderBy(x => x.Date).ToList();
                return View(list);
            }
        }

        public ActionResult Shop()
        {
            using (TicketDomContext context = new TicketDomContext())
            {
                var list = context.Tickets.OrderBy(x => x.Date).ToList();
                return View(list);
            }
        }
        [HttpPost]
        public ActionResult Shop(string keyword)
        {

            using (TicketDomContext context = new TicketDomContext())
            {
                var list = context.Tickets.OrderBy(x => x.Date).ToList();
                var results = new List<Tickets>();
                foreach (Tickets item in list)
                {
                    if (item.Title.ToLower().Contains(keyword.ToLower()) || item.Type.ToLower().Contains(keyword.ToLower()) || item.Venue.ToLower().Contains(keyword.ToLower()) || item.City.ToLower().Contains(keyword.ToLower()))
                    {
                        results.Add(item);
                    }

                }
                results.OrderBy(x => x.Date);
                return View(results);
            }
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int id)
        {
            using (TicketDomContext context = new TicketDomContext())
            {
                Tickets item = context.Tickets.FirstOrDefault(x => x.Id == id);
                return View(item);
            }
        }

        public ActionResult AddToCart(int id)
        {
            using (TicketDomContext context = new TicketDomContext())
            {
                Tickets item = context.Tickets.FirstOrDefault(x => x.Id == id);
                return View(item);
            }
        }
        [HttpPost]
        public ActionResult AddToCart(string TicketId, string Quantity, string Price)
        {

            int TickId = Convert.ToInt32(TicketId);

            int qty = Convert.ToInt32(Quantity);
            double price = Convert.ToDouble(Price);
            using (TicketDomContext context = new TicketDomContext())
            {
                Tickets ticket = context.Tickets.FirstOrDefault(x => x.Id == TickId);
                if (System.Web.HttpContext.Current.Session["cartId"] == null)
                {
                    ShoppingCart newCart = new Models.ShoppingCart();
                    context.ShoppingCart.Add(newCart);
                    context.SaveChanges();
                    System.Web.HttpContext.Current.Session["cartId"] = newCart.Id;
                }


                int cartId = Convert.ToInt32(System.Web.HttpContext.Current.Session["cartId"]);
                ShoppingCart cart = context.ShoppingCart.FirstOrDefault(x => x.Id == cartId);
                bool newTicket = true;

                if (cart.Items.Count() > 0)
                {
                    foreach (CartItem item in cart.Items)
                    {
                        if (item.TicketId == TickId)
                        {
                            newTicket = false;
                            int qt = item.Quantity + qty;
                            item.Quantity = qt;
                            context.SaveChanges();
                        }
                    }
                }
                if (newTicket == true)
                {
                    CartItem newItem = new CartItem();
                    newItem.TicketId = TickId;
                    newItem.Quantity = qty;
                    newItem.TicketPrice = price;
                    newItem.CartId = cart.Id;
                    newItem.Ticket = ticket;
                    newItem.Description = ticket.Title + " at " + ticket.Venue + " located in " + ticket.City + ", " + ticket.State + " on " + ticket.Date.ToString("M/d/yyyy hh:mm tt");
                    cart.Items.Add(newItem);
                    context.CartItem.Add(newItem);
                    context.SaveChanges();
                }


            }
            return RedirectToAction("Shop");
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            return View(new Tickets());
        }
        public ActionResult Home()
        {



            return View();


        }

        public ActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Admin(string password)
        {
            if (password == "AdminPass333")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("AdminError");
            }
        }
        public ActionResult AdminError()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminError(string password)
        {
            if (password == "AdminPass333")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult About()
        {
            return View();
        }

        // POST: Tickets/Create
        [HttpPost]
        public ActionResult Create(Tickets obj)
        {

            try
            {
                using (TicketDomContext context = new TicketDomContext())
                {
                    context.Tickets.Add(obj);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }


            }
            catch
            {
                return View();
            }
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int id)
        {
            Tickets result = null;
            using (TicketDomContext context = new TicketDomContext())
            {
                result = context.Tickets.FirstOrDefault(x => x.Id == id);
            }
            return View(result);
        }

        // POST: Tickets/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                using (TicketDomContext context = new TicketDomContext())
                {
                    var item = context.Tickets.FirstOrDefault(x => x.Id == id);
                    TryUpdateModel(item);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int id)
        {
            using (TicketDomContext context = new TicketDomContext())
            {
                Tickets item = context.Tickets.FirstOrDefault(x => x.Id == id);
                return View(item);
            }
        }

        // POST: Tickets/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (TicketDomContext context = new TicketDomContext())
                {
                    var item = context.Tickets.FirstOrDefault(x => x.Id == id);


                    context.Tickets.Remove(item);
                    context.SaveChanges();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        TicketDomContext db = new TicketDomContext();
        private IQueryable GetTickets()
        {
            return (from obj in db.Tickets select new { obj.Type, obj.Title, obj.Price, obj.Date, obj.Venue, obj.City, obj.State, obj.Seating, obj.Seat });
        }

        public JsonResult GetTicketsJson()
        {
            return Json(GetTickets(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult getTicks()
        {
            using (TicketDomContext context = new TicketDomContext())
            {
                var list = context.Tickets.OrderBy(x => x.Date).ToList();
                var jsonData = "[";
                foreach(Tickets item in list)
                {
                    jsonData += "{'Id':'" + item.Id + "','Type':'" + item.Type + "','Title':'" + item.Title + "','Price':'" + item.Price + "','Date':'" + item.Date + "','Title':'"  + item.Title + "','Venue':'" + item.Venue + "','City':'" + item.City + "','State':'" + item.State + "'},";
                }
                jsonData = jsonData.TrimEnd(',');
                jsonData += "]";
                ViewBag.Data = jsonData;
                return View();            }
        }
    }

    


}

    

