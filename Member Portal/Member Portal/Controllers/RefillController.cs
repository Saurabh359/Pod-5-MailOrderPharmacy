using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Member_Portal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Member_Portal.Controllers
{
    public class RefillController : Controller
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SubscriptionController));

        private IConfiguration configuration;

        public RefillController(IConfiguration configuration)
        {
            this.configuration = configuration;
        } 

        public IActionResult RefillStatus(int id)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                _log4net.Warn("Anonymous user trying to access "+nameof(SubscriptionController));
                return RedirectToAction("Index", "Home");
            }

            // call Refill microservice
            _log4net.Debug("Accessing RefillService for latest completed refill of Subscription Id - "+ id);
            
            using (var httpClient = new HttpClient())
            {
                string url = "" + configuration["ServiceUrls:Refill"]+ "RefillStatus";

                var request = new HttpRequestMessage(HttpMethod.Get, url+"/" + id);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                using (var response = httpClient.SendAsync(request).Result)
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        string message = "Something Went Wrong" + response.StatusCode;
                        return RedirectToAction("ResponseDisplay", "Subscription", new { message });
                    }

                    var data = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<RefillOrderDetails>(data);

                    if (result == null)
                    {
                        string message = "No Fullfilled Refill Order for Subscription Id  - "+id;
                        return RedirectToAction("ResponseDisplay", "Subscription", new { message });
                    }

                    return View(result);
                }
            }
        }

        public IActionResult RefillDues(int id)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                _log4net.Warn("Anonymous user trying to access " + nameof(SubscriptionController));
                return RedirectToAction("Index", "Home");
            }

            int due;

            //Call Refill Microservice -- DueRefills Method

            _log4net.Debug("Accessing RefillService for due refill count of Subscription Id - " + id);

            using (var httpClient = new HttpClient())
            {
                string url = "" + configuration["ServiceUrls:Refill"] + "RefillDues";

                var request = new HttpRequestMessage(HttpMethod.Get, url+"/" + id);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                using (var response = httpClient.SendAsync(request).Result)
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        string message = "Something Went Wrong" + response.StatusCode;
                        return RedirectToAction("ResponseDisplay", "Subscription", new { message });
                    }

                    var data = response.Content.ReadAsStringAsync().Result;

                    due = JsonConvert.DeserializeObject<int>(data);

                    ViewBag.DueCount = due;

                    return View();
                }
            }

        }

        public IActionResult AdhocRefill(int id)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                _log4net.Warn("Anonymous user trying to access " + nameof(SubscriptionController));
                return RedirectToAction("Index", "Home");
            }

            //Call Refill Microservice -- Adhoc Method
            _log4net.Debug("Accessing RefillService for Adhoc refill for Subscription Id - " + id);


            int PolicyId = 2;
            int MemberId= (int)HttpContext.Session.GetInt32("MemberId");

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject("hello"), Encoding.UTF8, "application/json");

                string url = "" + configuration["ServiceUrls:Refill"] + "AdhocRefill";

                var request = new HttpRequestMessage(HttpMethod.Post, url+"/" + PolicyId + "/" + MemberId + "/" + id)
                {
                    Content = content
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                using (var response = httpClient.SendAsync(request).Result)
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        string message = "Something Went Wrong " + response.StatusCode;
                        return RedirectToAction("ResponseDisplay", "Subscription", new { message });
                    }

                    var data = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<RefillOrderDetails>(data);

                    if(result==null)
                    {
                        string message = "Adhoc Refill Not Possible";
                        return RedirectToAction("ResponseDisplay", "Subscription", new { message });
                    }

                    return View(result);
                }
            }
        }
    }
}
