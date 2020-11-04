using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public IActionResult RefillStatus(int id)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                // _log4net.Info("Anonymous Log in try to add vehicle but redirected to login page");
                return RedirectToAction("Login", "User");
            }

            // call Refill microservice

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync("https://localhost:44329/api/RefillOrders/RefillStatus/"+ id).Result)
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
            int due;

            //send Subscription Id

            //Call Refill Microservice -- DueRefills Method
            //Return Refill Counts


            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync("https://localhost:44329/api/RefillOrders/RefillDues/" + id).Result)
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
                // _log4net.Info("Anonymous Log in try to add vehicle but redirected to login page");
                return RedirectToAction("Login", "User");
            }

            //Call Refill Microservice -- Adhoc Method

            int PolicyId = 2;
            int MemberId= (int)HttpContext.Session.GetInt32("MemberId");

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject("hello"), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync("https://localhost:44329/api/RefillOrders/AdhocRefill/" + PolicyId + "/" + MemberId + "/" + id, content).Result)
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
