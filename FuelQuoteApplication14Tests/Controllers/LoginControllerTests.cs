using Microsoft.VisualStudio.TestTools.UnitTesting;
using FuelQuoteApplication14.Controllers;
using FuelQuoteApplication14.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelQuoteApplication14.Controllers.Tests
{
    [TestClass()]
    public class LoginControllerTests
    {
        private readonly LoginController _controllerLogin;
        private readonly ClientController _controllerClient;
        [TestMethod()]
        public void RegisterTest()
        {
            Models.UserCredential userinfo = new Models.UserCredential()
            {
                Username = "Pumba1",
                Password = "Simba@123"
            };
        }

        public void ClientProfileTest()
        {
            Models.Client_Info clientinfo = new Models.Client_Info()
            {
                Address1 = "Cullen Dr",
                Address2 = "Apt 4",
                City = "Houston",
                State = "TX",
                ZipCode = 77077
            };
        }
    }
}