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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login([Bind(include:"Email,Password")]UserDetails user)
        {
            AuthorizedData authorizedData;

            // call Authentication service and recieve token

           
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync("https://localhost:32770/token", content).Result)
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                    var data =  response.Content.ReadAsStringAsync().Result;

                    authorizedData = JsonConvert.DeserializeObject<AuthorizedData>(data);

                    HttpContext.Session.SetString("Token", authorizedData.Token);
                    HttpContext.Session.SetInt32("MemberId", authorizedData.Id);
                    HttpContext.Session.SetString("MemberLocation", authorizedData.Location);

                    return RedirectToAction("Index", "Subscription");
                }
            }
           
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register([Bind("Name,Email,Password,Location")]UserDetails user)
        {
            bool success = true;
            // Register user in Member Database

            if (success)
                return RedirectToAction("Index");

            return View();
        }

        public IActionResult Logout()
        {
            //clear all session data
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
