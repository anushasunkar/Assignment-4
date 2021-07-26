using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FuelQuoteApplication14.Models;

namespace FuelQuoteApplication14.Controllers
{
    public class QuoteController : Controller
    {
        // GET: Quote
        public ActionResult Generate_quote()
        {
            Models.FuelQuoteDBEntities3 db = new Models.FuelQuoteDBEntities3();
            var id = (int)System.Web.HttpContext.Current.Session["userID"];
            Client_Info c = db.Client_Info.Where(x => x.Id == id).FirstOrDefault();
            var address = c.Address1 + " ," + c.Address2 + ", " + c.City + ", " + c.State + ", " + c.ZipCode;
            Quote q = new Quote();
            q.Address = address;
            return View(q);
        }
        public ActionResult Quote_save(Quote q)
        {
            Models.FuelQuoteDBEntities5 db = new FuelQuoteDBEntities5();
            Quote q1 = new Quote();
            q1.Gallons_requested = q.Gallons_requested;
            q1.Address = q.Address;
            q1.Delivery_date = Convert.ToDateTime(q.Delivery_date);
            q1.Total_amount = q.Total_amount;
            q1.Id = (int)System.Web.HttpContext.Current.Session["userID"];
            q1.price_per_gallon = q.price_per_gallon;
            if(db.Quote.OrderByDescending(p => p.Quote_id).FirstOrDefault()!=null)
            {
                q1.Quote_id = db.Quote.OrderByDescending(p => p.Quote_id).FirstOrDefault().Quote_id + 1;
            }
            else
            {
                q1.Quote_id = 1;
            }
            db.Quote.Add(q1);
            db.SaveChanges();

            return RedirectToAction("Home", "Client");
        }

        public ActionResult Quote_history()
        {
            Models.FuelQuoteDBEntities5 db = new FuelQuoteDBEntities5();
            Quote q1 = new Quote();
            var id = (int)System.Web.HttpContext.Current.Session["userID"];
            var c = db.Quote.Where(a => a.Id == id);
            return View(c);
        }
    }
}