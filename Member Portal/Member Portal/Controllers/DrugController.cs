using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Member_Portal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Member_Portal.Controllers
{
    public class DrugController : Controller
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SubscriptionController));

        private IConfiguration configuration;

        public DrugController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index(string message)
        {
            _log4net.Debug("Index page for Drug Search Acccessed");

            ViewBag.Response = message;
            return View();
        }
        
        public IActionResult DrugbyName(string name)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                _log4net.Warn("Anonymous user trying to access "+nameof(DrugController));
                return RedirectToAction("Index", "Home");
            }

            // call Drug microservice
            _log4net.Debug("Accessing DrugService for Details of "+name+" Drug");

            using (var httpClient = new HttpClient())
            {
                string url = "" + configuration["ServiceUrls:Drug"]+ "searchDrugsByName/";
                var request = new HttpRequestMessage(HttpMethod.Get, url + name);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                using (var response = httpClient.SendAsync(request).Result)
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        _log4net.Error("Response failure ");

                        string message = "Something Went Wrong "+ response.StatusCode;
                        return RedirectToAction("Index", new { message });
                    }

                    var data = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<List<DrugDetails>>(data);

                    if (result.Count==0)
                    {
                        string message = name+"Drug Not Available";
                        return RedirectToAction("Index", new { message });
                    }

                    _log4net.Info("Successfull result display ");
                    return View(result);
                }
            }
        }


        public IActionResult DrugbyId(int id)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                _log4net.Warn("Anonymous user trying to access " + nameof(DrugController));
                return RedirectToAction("Index", "Home");
            }

            // call Drug microservice
            _log4net.Debug("Accessing DrugService for Details of " + id + " Id Drug");

            using (var httpClient = new HttpClient())
            {
                string url = "" + configuration["ServiceUrls:Drug"] + "searchDrugsById/";
                var request = new HttpRequestMessage(HttpMethod.Get, url + id);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                using (var response = httpClient.SendAsync(request).Result)
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        _log4net.Error("Response failure ");

                        string message = "Something Went wrong " + response.StatusCode;
                        return RedirectToAction("Index", new { message });
                    }

                    var data = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<List<DrugDetails>>(data);

                    if (result.Count==0)
                    {
                        string message = id + " Id Drug Not Available";
                        return RedirectToAction("Index", new { message });
                    }

                    _log4net.Info("Successfull result display ");
                    return View(result);
                }
            }
        }
    }
}
