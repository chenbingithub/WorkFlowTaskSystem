using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkFlowTaskSystem.Application.Basics.PermissionInfos;
using WorkFlowTaskSystem.Controllers;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Web.Host.Models;

namespace WorkFlowTaskSystem.Web.Host.Controllers
{
    public class HomeController : WorkFlowTaskSystemControllerBase
    {
        private IPermissionInfoAppService _permissionInfoAppService;
        public HomeController(IPermissionInfoAppService permissionInfoAppService)
        {
            _permissionInfoAppService = permissionInfoAppService;
        }
        public IActionResult Index()
        {
            //Test dd = new Test();
            //var f = dd.GetType().GetProperties();
            //foreach (System.Reflection.PropertyInfo p in dd.GetType().GetProperties())
            //{
            //    var s = p.Name.Split('_');
            //    var sea = s.Length == 1 ? "-2" : p.Name.Replace("_" + s[s.Length - 1], "").Replace('_', '.');
            //    var m = _permissionInfoAppService.GetPermission(sea);
            //    _permissionInfoAppService.Create(new Application.Basics.PermissionInfos.Dto.CreatePermissionInfoDto
            //    {
            //        Code = p.Name.Replace('_', '.'),
            //        Name = Test.ToName(s[s.Length - 1]),
            //        ParentId = m?.Id,
            //        ParentName = m?.Name
            //    });
            //}
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
