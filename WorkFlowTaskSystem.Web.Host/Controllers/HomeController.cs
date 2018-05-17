using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkFlowTaskSystem.Controllers;
using WorkFlowTaskSystem.Web.Host.Models;

namespace WorkFlowTaskSystem.Web.Host.Controllers
{
    public class HomeController : WorkFlowTaskSystemControllerBase
    {
        public IActionResult Index()
        {
            return Redirect("/swagger");
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
