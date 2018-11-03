using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SnowOverFlow.Models;

namespace SnowOverFlow.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "WHY DO WE EXIST?";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "HOW TO REACH OUR TEAM?";

            return View();
        }
        public IActionResult Continent()
        {
            ViewData["Message"] = "We have a few:";

            return View();
        }
        public IActionResult Statistics()
        {
            ViewData["Message"] = "few more ways to learn - which site is the best for you:";

            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
