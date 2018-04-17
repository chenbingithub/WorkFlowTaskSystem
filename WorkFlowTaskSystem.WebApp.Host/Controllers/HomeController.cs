using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Net.Mail.Smtp;
using Abp.Runtime.Caching;
using Microsoft.AspNetCore.Mvc;
using WorkFlowTaskSystem.Application.Forms;
using WorkFlowTaskSystem.Application.Forms.Dto;
using WorkFlowTaskSystem.Controllers;
using WorkFlowTaskSystem.WebApp.Host.Models;

namespace WorkFlowTaskSystem.WebApp.Host.Controllers
{
    public class HomeController : WorkFlowTaskSystemControllerBase
    {
        private IFormAppService _formAppService;
        private ICacheManager _cacheManager;
        private ISmtpEmailSender _emailSender;
        public HomeController(ICacheManager cacheManager, IFormAppService formAppService, ISmtpEmailSender emailSender)
        {
            _cacheManager = cacheManager;
            _formAppService = formAppService;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
           //var t= _formAppService.Create(new CreateFormDto()
           // {
           //     Name = "test2",
           //     Code = "first2",

           // });
           // _cacheManager.GetCache("test3").Set("one", t.Result);
           var dd= _formAppService.Get(new EntityDto<string>("25ae7f404d37420297939ffd427c2111"));
            _emailSender.Send("1028374217@qq.com","test","哈哈哈哈");
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
