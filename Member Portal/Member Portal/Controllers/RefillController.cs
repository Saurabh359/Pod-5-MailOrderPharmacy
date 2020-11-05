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
using Newtonsoft.Json;

namespace Member_Portal.Controllers
{
    public class RefillController : Controller
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SubscriptionController));

        public IActionResult RefillStatus(int id)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                _log4net.Info("Anonymous user trying to access "+nameof(SubscriptionController));
                return RedirectToAction("Login", "User");
            }

            // call Refill microservice
            _log4net.Info("Accessing RefillService for latest completed refill");

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44329/api/RefillOrders/RefillStatus/" + id);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                using (var response = httpClient.SendAsync(request).Result)
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        string message = "Something Went Wrong";
                        return RedirectToAction("ResponseDisplay", message);
                    }

                    var data = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<RefillOrderDetails>(data);

                    return View(result);
                }
            }
        }

        public IActionResult RefillDues(int id)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                _log4net.Info("Anonymous user trying to access " + nameof(SubscriptionController));
                return RedirectToAction("Login", "User");
            }

            int due;

            //Call Refill Microservice -- DueRefills Method

            _log4net.Info("Accessing RefillService for due refill count");

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44329/api/RefillOrders/RefillDues/" + id);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                using (var response = httpClient.SendAsync(request).Result)
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        string message = "Something Went Wrong";
                        return RedirectToAction("ResponseDisplay", message);
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
                _log4net.Info("Anonymous user trying to access " + nameof(SubscriptionController));
                return RedirectToAction("Login", "User");
            }

            //Call Refill Microservice -- Adhoc Method
            _log4net.Info("Accessing RefillService for Adhoc refill");


            int PolicyId = 2;
            int MemberId= (int)HttpContext.Session.GetInt32("MemberId");

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject("hello"), Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44329/api/RefillOrders/AdhocRefill/" + PolicyId + "/" + MemberId + "/" + id)
                {
                    Content = content
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                using (var response = httpClient.SendAsync(request).Result)
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        string message = "Something Went Wrong";
                        return RedirectToAction("ResponseDisplay", message);
                    }

                    var data = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<RefillOrderDetails>(data);

                    if(result==null)
                    {
                        string message = "Adhoc Refill Not Possible";
                        return RedirectToAction("ResponseDisplay", message);
                    }

                    return View(result);
                }
            }
        }
    }
}
