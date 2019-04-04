using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using Library.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AutomationP.Controllers
{
    public class HomeController : Controller
    {
        ProductContext db;
        public HomeController(ProductContext context)
        {
            db = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            var enter = db.Users.FirstOrDefault(s => s.Login == User.Identity.Name).Role.EnterpriseId;
            ViewBag.Enter = User.Claims.ToList()[1].Value + " "+ User.Claims.ToList()[0].Value;
          //  ViewBag.Enter = enter;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
