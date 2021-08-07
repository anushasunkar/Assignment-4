using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FuelQuoteApplication14.Models;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;

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
            try
            {
                FuelQuoteDBEntities3 db_client = new FuelQuoteDBEntities3();
                

                    using (FuelQuoteDBEntities1 db = new FuelQuoteDBEntities1())
                    {
                        var enc = encrypt(model.Password);
                        var userdetails = db.UserCredentials.Where(x => x.Username == model.Username && x.Password == enc).FirstOrDefault();
                        
                        
                        if (userdetails == null)
                        {
                            model.LoginErrorMessage = "Incorrect username or password";
                            return View("LoginView", model);
                        }
                        else
                        {
                            var client = db_client.Client_Info.Where(x => x.Id == userdetails.Id).FirstOrDefault();
                            if (client == null)
                            {
                                Session["userID"] = userdetails.Id;
                                return RedirectToAction("Client_info", "Client");
                            }
                            Session["userID"] = userdetails.Id;
                            return RedirectToAction("Home", "Client");
                        }

                    }
                //return View("LoginView", model);
            }
            catch(NullReferenceException)
            {
                model.LoginErrorMessage = "Incorrect username or password";
                return View("LoginView", model);
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


        public string encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public ActionResult Save_details(UserCredential_register user)
        {

                if (ModelState.IsValid)
                {

                    Models.FuelQuoteDBEntities1 db = new FuelQuoteDBEntities1();
                    UserCredential u = new UserCredential();
                    u.Username = user.Username;
                    var p = user.Password;
                    var enc = encrypt(p);
                    u.Password = enc;

                    var max = db.UserCredentials.OrderByDescending(P => P.Id).FirstOrDefault().Id;

                    u.Id = max + 1;

                    Session["userid"] = u.Id;

                    db.UserCredentials.Add(u);
                    db.SaveChanges();

                    return RedirectToAction("Client_info", "Client");
                }
                return View("Register", user);
            
        }

        public bool RegisterDataValidation(UserCredential_register registerinfo)
        {
            bool flag = false;
            if ((registerinfo.Username.Length <= 50) && (registerinfo.Username != String.Empty) && (Regex.IsMatch(registerinfo.Username, @"^[A-Za-z0-9._]+$")))
            {
                if ((Regex.IsMatch(registerinfo.Password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")) && (registerinfo.Password != String.Empty))
                {
                    if (registerinfo.Password == registerinfo.Confirm_Password)
                    {
                        flag = true;
                    }
                }
            }
            else
            {
                flag = false;
            }

            return flag;
        }

    }
}
