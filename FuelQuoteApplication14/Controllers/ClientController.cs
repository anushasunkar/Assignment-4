using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FuelQuoteApplication14.Models;

namespace FuelQuoteApplication14.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Home()
        {
            HttpCookie cookie = new HttpCookie("WTR");
            var id = System.Web.HttpContext.Current.Session["userID"].ToString();
            cookie["id"] = id;
            return View();
        }

        public ActionResult Client_info()
        {
            return View();
        }

        public ActionResult Client_info_save(Client_Info c)
        {
            Models.FuelQuoteDBEntities3 db = new FuelQuoteDBEntities3();
            Models.FuelQuoteDBEntities1 db1 = new FuelQuoteDBEntities1();
            Client_Info c1 = new Client_Info();
            c1.FullName = c.FullName;
            c1.Address1 = c.Address1;
            c1.Address2 = c.Address2;
            c1.City = c.City;
            c1.State = c.State;
            c1.ZipCode = c.ZipCode;
            c1.Id = (int)System.Web.HttpContext.Current.Session["userid"];

            HttpCookie cookie = new HttpCookie("WTR");
            cookie["id"] = c1.Id.ToString();
            Response.Cookies.Add(cookie);

            db.Client_Info.Add(c1);
            db.SaveChanges();

            return RedirectToAction("Home", "Login");
        }
        public ActionResult ViewProfile()
        {
            Models.FuelQuoteDBEntities3 db = new FuelQuoteDBEntities3();
            Client_Info c1 = new Client_Info();
            var id = (int)System.Web.HttpContext.Current.Session["userID"];

            HttpCookie cook_obj = Request.Cookies["WTR"];
            //string id1 = cook_obj["id"];
            var c = db.Client_Info.Where(a => a.Id == id);
            foreach (var item in c)
            {
                c1.Id = id;
                c1.FullName = item.FullName;
                c1.Address1 = item.Address1;
                c1.Address2 = item.Address2;
                c1.City = item.City;
                c1.State = item.State;
                c1.ZipCode = item.ZipCode;
            }


            return View("View_profile", c1);
        }
    }
}