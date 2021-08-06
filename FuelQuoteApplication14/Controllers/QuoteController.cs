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

        public ActionResult Quote_display(Models.Quote q)
        {
            if (ModelState.IsValid)
            {

                Models.FuelQuoteDBEntities3 db = new Models.FuelQuoteDBEntities3();
                var id = (int)System.Web.HttpContext.Current.Session["userID"];
                Client_Info c = db.Client_Info.Where(x => x.Id == id).FirstOrDefault();
                var Location_factor = 0.0;
                var Rate_history_factor = 0.0;
                var current_price = 1.5;
                ViewBag.current_price = 1.5;
                var company_profit_factor = 0.1;
                var gallons_requested_factor = 0.03;

                if (c.State == "TX")
                {
                    Location_factor = 0.02;
                }
                else
                {
                    Location_factor = 0.04;
                }

                Models.FuelQuoteDBEntities5 db1 = new FuelQuoteDBEntities5();
                Quote q1 = new Quote();

                var c1 = db1.Quote.Where(a => a.Id == id).OrderBy(a => a.Delivery_date).Count();
                if (c1 == 0)
                {
                    Rate_history_factor = 0;
                }
                else
                {
                    Rate_history_factor = 0.01;
                }
                if (q.Gallons_requested > 1000)
                {
                    gallons_requested_factor = 0.02;
                }
                else
                {
                    gallons_requested_factor = 0.03;
                }

                q1.price_per_gallon = current_price + (Location_factor - Rate_history_factor + company_profit_factor + gallons_requested_factor) * current_price;
                q1.Total_amount = q1.price_per_gallon * q.Gallons_requested;
                q1.Gallons_requested = q.Gallons_requested;
                q1.Delivery_date = q.Delivery_date;
                q1.Address = q.Address;
                return View(q1);
            }
            return View("Generate_quote", q);
        }
        public ActionResult Quote_save(Quote q)
        {
            if (ModelState.IsValid)
            {
                Models.FuelQuoteDBEntities5 db = new FuelQuoteDBEntities5();
                Quote q1 = new Quote();
                q1.Gallons_requested = q.Gallons_requested;
                q1.Address = q.Address;
                q1.Delivery_date = Convert.ToDateTime(q.Delivery_date);
                q1.Total_amount = q.Total_amount;
                q1.Id = (int)System.Web.HttpContext.Current.Session["userID"];
                q1.price_per_gallon = q.price_per_gallon;
                if (db.Quote.OrderByDescending(p => p.Quote_id).FirstOrDefault() != null)
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
            return View("Generate_quote", q);
        }

        public ActionResult Quote_history()
        {
            Models.FuelQuoteDBEntities5 db = new FuelQuoteDBEntities5();
            Quote q1 = new Quote();
            var id = (int)System.Web.HttpContext.Current.Session["userID"];
            var c = db.Quote.Where(a => a.Id == id).OrderBy(a=>a.Delivery_date);
            return View(c);
        }

        
        public ActionResult Remove_quote(Quote i)
        {
            Models.FuelQuoteDBEntities5 db = new Models.FuelQuoteDBEntities5();
            Quote q = db.Quote.Find(i.Quote_id);
            db.Quote.Remove(q);
            db.SaveChanges();
            return RedirectToAction("Quote_history");
        }
    }
}
