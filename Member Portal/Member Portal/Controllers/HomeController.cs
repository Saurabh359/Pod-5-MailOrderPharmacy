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
using Microsoft.Extensions.Configuration;

namespace Member_Portal.Controllers
{
    public class HomeController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(HomeController));

        private IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index(bool status)
        {
            _log4net.Debug("Login Page");
            _log4net.Warn("Trile Warn");
            _log4net.Error("Trile Error");
            if(status)
            {
                ModelState.AddModelError(string.Empty, "Email or Password is Invalid !!!");
            }
            return View();
        }

        public IActionResult Login([Bind(include:"Email,Password")]UserDetails user)
        {
            AuthorizedData authorizedData;

            // call Authentication service and recieve token
            _log4net.Info("Access Authorization Microservice and get Token for email- "+user.Email+" and password- "+user.Password);

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                string url = "" + configuration["ServiceUrls:Authorization"];

                using (var response = httpClient.PostAsync(url, content).Result)
                {
                    _log4net.Debug("Back to Member Portal from Authorization Microservice");
                    
                    if (!response.IsSuccessStatusCode)
                    {
                        _log4net.Error("Login failed "+ response.StatusCode);
                        var status = true;
                        return RedirectToAction("Index",new { status });
                    }

                    var data =  response.Content.ReadAsStringAsync().Result;

                    authorizedData = JsonConvert.DeserializeObject<AuthorizedData>(data);

                    HttpContext.Session.SetString("Token", authorizedData.Token);
                    HttpContext.Session.SetInt32("MemberId", authorizedData.Id);
                    HttpContext.Session.SetString("MemberLocation", authorizedData.Location);

                    _log4net.Info("Login Successfull for email- " + user.Email + " and password- " + user.Password);
                    return RedirectToAction("Index", "Subscription");
                }
            }
           
        }

        public IActionResult Register()
        {
            _log4net.Debug("Register Page");
            return View();
        }

        [HttpPost]
        public IActionResult Register([Bind("Name,Email,Password,Location")]UserDetails user)
        {
            bool success = true;
            // Register user in Member Database
            _log4net.Debug("Register to Site ");
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
