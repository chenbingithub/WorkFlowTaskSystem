using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;

namespace WorkFlowTaskSystem.WebApp.Host.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}