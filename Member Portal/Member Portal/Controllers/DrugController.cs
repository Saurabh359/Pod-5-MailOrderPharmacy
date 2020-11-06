using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Member_Portal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Member_Portal.Controllers
{
    public class DrugController : Controller
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SubscriptionController));

        public IActionResult Index()
        {
            _log4net.Debug("Index page for Drug Search Acccessed");
            return View();
        }
        
        public IActionResult Drug(string name)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                _log4net.Warn("Anonymous user trying to access "+nameof(DrugController));
                return RedirectToAction("Index", "Home");
            }

            // call Drug microservice
            _log4net.Debug("Accessing DrugService for Drug Details ");

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44393/api/DrugsApi/searchDrugsByName/" + name);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                using (var response = httpClient.SendAsync(request).Result)
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        _log4net.Error("Response failure ");

                        string message = "Something Went Wrong";
                        return RedirectToAction("ResponseDisplay", "Subscription", new { message });
                    }

                    var data = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<List<DrugDetails>>(data);

                    _log4net.Info("Successfull result display ");
                    return View(result);
                }
            }
        }
    }
}
