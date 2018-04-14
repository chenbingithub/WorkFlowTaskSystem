using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkFlowTaskSystem.Application.Forms;
using WorkFlowTaskSystem.Application.Forms.Dto;
using WorkFlowTaskSystem.Controllers;

namespace WorkFlowTaskSystem.Web.Host.Controllers
{
    public class HomeController : WorkFlowTaskSystemControllerBase
    {
        private IFormAppService _formAppService;

        public HomeController(IFormAppService formAppService)
        {
            _formAppService = formAppService;
        }

        public IActionResult Index()
        {
            _formAppService.Create(new CreateFormDto()
            {
                Name = "test1",
                Code = "first1",

            });
            FormDto dto=new FormDto(){Id = "4736e6dc28be46949a2b1fcaea84b1ad" }; 
            _formAppService.Delete(dto);
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
