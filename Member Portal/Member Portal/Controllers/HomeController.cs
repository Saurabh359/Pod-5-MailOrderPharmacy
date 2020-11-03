using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Member_Portal.Models;

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
            bool success = true;

            // call Authentication service and recieve token

            if (success)
                return RedirectToAction("Index","Subscription");

            return View();
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

            return RedirectToAction("Index");
        }

    }
}
