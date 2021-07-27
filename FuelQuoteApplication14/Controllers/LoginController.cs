using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FuelQuoteApplication14.Models;
using System.Data.SqlClient;

namespace FuelQuoteApplication14.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View("LoginView");
        }

        [HttpPost]
        public ActionResult Autherize(FuelQuoteApplication14.Models.UserCredential model)
        {
            FuelQuoteDBEntities3 db_client = new FuelQuoteDBEntities3();

            using(FuelQuoteDBEntities1 db=new FuelQuoteDBEntities1())
            {
                var userdetails = db.UserCredentials.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();
                var client = db_client.Client_Info.Where(x => x.Id == userdetails.Id).FirstOrDefault();
                if (userdetails == null)
                {
                    model.LoginErrorMessage = "Wrong username or password";
                    return View("LoginView", model);
                }
                else
                {
                    if(client==null)
                    {
                        Session["userID"] = userdetails.Id;
                        return RedirectToAction("Client_info", "Client");
                    }
                    Session["userID"] = userdetails.Id;
                    return RedirectToAction("Home", "Client");
                }
                
            }
            
        }
        public ActionResult RegisterView()
        {
            return View("Register");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }

        public ActionResult Save_details(UserCredential user)
        {
            //sqlConnection con = new SqlConnection()


              Models.FuelQuoteDBEntities1 db = new FuelQuoteDBEntities1();
            UserCredential u = new UserCredential();
            u.Username = user.Username;
            u.Password = user.Password;


            
            var max = db.UserCredentials.OrderByDescending(P => P.Id).FirstOrDefault().Id;
            
            u.Id = max + 1;

            Session["userid"] = u.Id;

            db.UserCredentials.Add(u);
            db.SaveChanges();
            

            return RedirectToAction("Client_info", "Client");

                }
    }
}