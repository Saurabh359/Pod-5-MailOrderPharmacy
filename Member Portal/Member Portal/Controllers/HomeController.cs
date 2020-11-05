using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Member_Portal.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Member_Portal.Controllers
{
    public class HomeController : Controller
    {

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(HomeController));

        public IActionResult Index()
        {
            _log4net.Info("Login Page");

            return View();
        }

        public IActionResult Login([Bind(include:"Email,Password")]UserDetails user)
        {
            AuthorizedData authorizedData;

            // call Authentication service and recieve token
            _log4net.Info("Access Authorization Microservice and get Token");

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync("https://localhost:32768/token", content).Result)
                {
                    _log4net.Info("Back to Member Portal from Authorization Microservice");
                    if (!response.IsSuccessStatusCode)
                    {
                        _log4net.Info("Login failed");
                        return RedirectToAction("Index");
                    }

                    var data =  response.Content.ReadAsStringAsync().Result;

                    authorizedData = JsonConvert.DeserializeObject<AuthorizedData>(data);

                    HttpContext.Session.SetString("Token", authorizedData.Token);
                    HttpContext.Session.SetInt32("MemberId", authorizedData.Id);
                    HttpContext.Session.SetString("MemberLocation", authorizedData.Location);

                    _log4net.Info("Login Successfull");
                    return RedirectToAction("Index", "Subscription");
                }
            }
           
        }

        public IActionResult Register()
        {
            _log4net.Info("Register Page");
            return View();
        }

        [HttpPost]
        public IActionResult Register([Bind("Name,Email,Password,Location")]UserDetails user)
        {
            bool success = true;
            // Register user in Member Database
            _log4net.Info("Register to Site ");
            if (success)
                return RedirectToAction("Index");

            return View();
        }

        public IActionResult Logout()
        {
            _log4net.Info("Logout -- clear all session data");

            //clear all session data
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
