using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Member_Portal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Member_Portal.Controllers
{
    public class DrugController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Drug(string name)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                // _log4net.Info("Anonymous Log in try to add vehicle but redirected to login page");
                return RedirectToAction("Login", "User");
            }

            // call Drug microservice

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync("https://localhost:44329/api/DrugsApi/searchDrugsByName/" + name).Result)
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        string message = "Something Went Wrong";
                        return RedirectToAction("ResponseDisplay", message);
                    }

                    var data = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<DrugDetails>(data);

                    return View(result);
                }
            }
        }
    }
}
