using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Net.Mail.Smtp;
using Abp.Runtime.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.International.Converters.PinYinConverter;
using MongoDB.Driver;
using WorkFlowTaskSystem.Application;
using WorkFlowTaskSystem.Application.Basics.PermissionInfos;
using WorkFlowTaskSystem.Application.Basics.PermissionInfos.Dto;
using WorkFlowTaskSystem.Application.Basics.Roles;
using WorkFlowTaskSystem.Application.Basics.Roles.Dto;
using WorkFlowTaskSystem.Application.Basics.Users;
using WorkFlowTaskSystem.Application.Basics.Users.Dto;
using WorkFlowTaskSystem.Application.Forms;
using WorkFlowTaskSystem.Controllers;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Services.Basics;
using WorkFlowTaskSystem.WebApp.Host.Models;

namespace WorkFlowTaskSystem.WebApp.Host.Controllers
{
    public class HomeController : WorkFlowTaskSystemControllerBase
    {
        private IFormAppService _formAppService;
        private ICacheManager _cacheManager;
        private ISmtpEmailSender _emailSender;
        private IRoleAppService _roleAppService;
        private IPermissionInfoAppService _permissionInfoAppService;
        private UserManager _userManager;
        private RoleManager _roleManager;
        private OrganizationUnitManager _organizationUnitManager;
        private IUserAppService _userAppService;
        private PermissionInfoManager _permissionInfoManager;
        
        public HomeController(ICacheManager cacheManager, IFormAppService formAppService, ISmtpEmailSender emailSender, IRoleAppService roleAppService, IPermissionInfoAppService permissionInfoAppService, UserManager userManager, RoleManager roleManager, OrganizationUnitManager organizationUnitManager, IUserAppService userAppService, PermissionInfoManager permissionInfoManager)
        {
            _cacheManager = cacheManager;
            _formAppService = formAppService;
            _emailSender = emailSender;
            _roleAppService = roleAppService;
            _permissionInfoAppService = permissionInfoAppService;
            _userManager = userManager;
            _roleManager = roleManager;
            _organizationUnitManager = organizationUnitManager;
            _userAppService = userAppService;
            _permissionInfoManager = permissionInfoManager;
        }

        public IActionResult Index()
        {
            //var t= _formAppService.Create(new CreateFormDto()
            // {
            //     Name = "test2",
            //     Code = "first2",

            // });
            // _cacheManager.GetCache("test3").Set("one", t.Result);
            // var dd= _formAppService.Get(new EntityDto<string>("25ae7f404d37420297939ffd427c2111"));
            //_emailSender.Send("1028374217@qq.com","test","哈哈哈哈");
            //_roleAppService.Update(new RoleDto()
            //{
            //    Id= "f7e4fa3aa53b41d2896955fbfe3cd6c8",
            //    Description="tttt",
            //    IsActive=true,
            //    Name = "管理员",
            //    Code = "adminasf"
            //});
            //_permissionInfoAppService.Create(new Application.PermissionInfos.Dto.CreatePermissionInfoDto {
            //    Name="主菜单",
            //    Code="Home",
            //    ParentId="-1",
            //    ParentName=""
            //});
            //return View();
            //var p= _permissionInfoAppService.Create(new CreatePermissionInfoDto()
            // {
            //     Name = "test",
            //     Code = "testcode",
            // });
            // var p2=_permissionInfoAppService.Create(new CreatePermissionInfoDto()
            // {
            //     Name = "test1",
            //     Code = "testcode2",
            // });
            // _userManager.SetRole("1","2");
            // _userManager.SetPermission("1", p.Result.Id);
            // _userManager.SetOrganizationUnit("1","1","4");
            //_roleManager.SetPermission("1", "d8ae87590c4a459ea6322f59e4f87375");
            //_organizationUnitManager.SetRole("4", "1");
            //var g=_userManager.GetAllPermissions("1");
            //_userAppService.Create(new CreateUserDto()
            //{
            //    UserName = "admin",
            //    Password = "123456"
            //});
            //var dd = PinYinUtil.GetAllPinYin("陈斌");
            //var ff = PinYinUtil.GetSimplePinYin("陈斌");
            //var dept = GetDataBase().GetCollection("PermissionModel");
            //foreach (var document in dept.FindAll().ToList())
            //{
            //    _permissionInfoManager.Insert(new PermissionInfo()
            //    {
            //        Id = document["_id"].ToString(),
            //        Name = document["Name"].ToString(),
            //        Code = document["Code"]?.ToString(),
            //        ParentId = document["ParentId"]?.ToString(),
            //        ParentName = document["ParentName"].ToString(),
            //        Path = document["Path"]?.ToString(),
            //        PathName = document["Value"]?.ToString(),

            //    });
            //    //_roleAppService.Create(new CreateRoleDto()
            //    //{
            //    //    Id = document["_id"].ToString(),
            //    //       Name = document["Name"].ToString(),
            //    //    Code=PinYinUtil.GetAllPinYin(document["Name"].ToString()),
            //    //    PersIds=new string[0]
            //    //});
            //    //var d = document["WorkDept"].AsBsonArray.ToList();
            //    //var st = new string[d.Count];
            //    //for (var i = 0; i < d.Count; i++)
            //    //{
            //    //    st[i] = d[i].AsString;
            //    //}
            //    //_userAppService.Create(new CreateUserDto()
            //    //{
            //    //    Id = document["_id"].ToString(),
            //    //    Name = document["Name"].ToString(),
            //    //    UserName = document["No"]?.ToString(),
            //    //    EmailAddress = document["Email"]?.ToString(),
            //    //    Sex = document["SEX"]?.ToString(),
            //    //    OrganIds = st,
            //    //    IsDeleted = !document["Status"].AsBoolean,
            //    //    RoleIds = new string[0],
            //    //    PersIds = new string[0],

            //    //});
            //    //_organizationUnitManager.Insert(new OrganizationUnit()
            //    //{
            //    //    Id = document["_id"].ToString(),
            //    //    Name = document["Name"].ToString(),
            //    //    Code = document["Code"]?.ToString(),
            //    //    ParentId = document["ParentID"].ToString(),
            //    //    No = document["No"].ToString(),
            //    //    Path = document["PathID"]?.ToString(),
            //    //    PathName = document["PathName"]?.ToString(),
            //    //    Header = document["Header"]?.ToString(),
            //    //    Leader = document["Leader"]?.ToString(),
            //    //});
            //}

            return Redirect("/swagger");
        }

        public MongoDatabase GetDataBase()
        {
            var murl=MongoUrl.Create("mongodb://localhost:27017/WorkFlowTaskSystemDB");
            var server = MongoServerSettings.FromUrl(murl);
#pragma warning disable 618
            return new MongoServer(server).GetDatabase(murl.DatabaseName);
#pragma warning restore 618

        }

        public ActionResult NavMenu() {
            return PartialView();
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
        public ActionResult Data() {
            return Json("haha");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
