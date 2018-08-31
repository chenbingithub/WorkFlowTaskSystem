using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Abp.Net.Mail.Smtp;
using Castle.Components.DictionaryAdapter;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MongoDB.Driver;
using WorkFlowTaskSystem.Application.Basics.PermissionInfos;
using WorkFlowTaskSystem.Application.TreeNodes;
using WorkFlowTaskSystem.Application.TreeNodes.Dto;
using WorkFlowTaskSystem.Controllers;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.Reports;
using WorkFlowTaskSystem.Web.Host.Models;

namespace WorkFlowTaskSystem.Web.Host.Controllers
{
    public class HomeController : WorkFlowTaskSystemControllerBase
    {
        private IPermissionInfoAppService _permissionInfoAppService;
        private IDocumentTreeNodeAppService _documentTreeNodeAppService;
        private IHostingEnvironment _hostingEnvironment;
        private ISmtpEmailSender _emailSender;
        public HomeController(IPermissionInfoAppService permissionInfoAppService, IDocumentTreeNodeAppService documentTreeNodeAppService, IHostingEnvironment hostingEnvironment, ISmtpEmailSender emailSender)
        {
            _permissionInfoAppService = permissionInfoAppService;
            _documentTreeNodeAppService = documentTreeNodeAppService;
            _hostingEnvironment = hostingEnvironment;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            ReportAppService.WebRootPath= _hostingEnvironment.WebRootPath;
            //var database= new MongoClient("mongodb://localhost:27017/WorkFlowTaskSystemDB")
            //     .GetServer()
            //     .GetDatabase("WorkFlowTaskSystemDB");
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
            // UtaisTelPhone.ReturnCode("18903907942","test");
            //var t= database.GetCollection("TreeNodeModel").FindAll().ToList();
            //foreach (var bson in t)
            //{
            //    _documentTreeNodeAppService.Create(new DocumentTreeNodeDto()
            //    {
            //        Id = bson["_id"].ToString(),
            //        Name = bson["Name"].ToString(),
            //        Code = bson["Code"].ToString(),
            //        Path = bson["Path"].ToString(),
            //        PathName = bson["PathName"].ToString(),
            //        IsLeaf = bson["IsLeaf"].ToBoolean(),
            //        DateOrderby = bson["DateOrderby"].AsInt32,
            //        DataDefine =new DefineType()
            //        {

            //        } ,
            //        Url =new Urls()

            //    });
            //}
            return Redirect("/swagger");
        }

        public IActionResult testEmails()
        {
            _emailSender.Send("binchen@chinergy.com.cn","标题 测试","内容  哈哈哈哈哈，收到了吧！");
            return Ok();
        }

        [RequestSizeLimit(100_000_000)] //最大100m左右
        //[DisableRequestSizeLimit]  //或者取消大小的限制
        public IActionResult Upload()
        {
            var files = Request.Form.Files;

            long size = files.Sum(f => f.Length);

            string webRootPath = _hostingEnvironment.WebRootPath;

            string contentRootPath = _hostingEnvironment.ContentRootPath;
            List<string> filenames=new List<string>(); 
            foreach (var formFile in files)

            {

                if (formFile.Length > 0)

                {
                    string fileExt = Path.GetExtension(formFile.FileName); //文件扩展名，不含“.”

                    long fileSize = formFile.Length; //获得文件大小，以字节为单位

                    string newFileName = System.Guid.NewGuid().ToString()+ fileExt; //随机生成新的文件名

                    var filePath = webRootPath + "/upload/";
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    using (var stream = new FileStream(filePath+ newFileName, FileMode.Create))

                    {
                        formFile.CopyTo(stream);
                    }
                    filenames.Add(newFileName);
                }

            }



            return Ok(new { filenames, count = files.Count,size });
        }
        public IActionResult DownLoad(string file)

        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var addrUrl = webRootPath+"/upload/"+ file;

            var stream = System.IO.File.OpenRead(addrUrl);

            string fileExt = Path.GetExtension(file);

            //获取文件的ContentType

            var provider = new FileExtensionContentTypeProvider();

            var memi = provider.Mappings[fileExt];

            return File(stream, memi, Path.GetFileName(addrUrl));

        }

        public IActionResult DeleteFile(string file)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var addrUrl = webRootPath + "/upload/" + file;
            if (System.IO.File.Exists(addrUrl))
            {
                //删除文件
                System.IO.File.Delete(addrUrl);               
            }
            return Ok(new { file });
        }

        /// <summary>
        /// 预览pdf报表
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IActionResult Preview(string file)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var addrUrl = webRootPath + "/reports/" + file;

            var stream = System.IO.File.OpenRead(addrUrl);

            string fileExt = Path.GetExtension(file);

            //获取文件的ContentType

            var provider = new FileExtensionContentTypeProvider();

            var memi = provider.Mappings[fileExt];

            return File(stream, memi, Path.GetFileName(addrUrl));
        }

       

        public IActionResult About(HttpContext context)
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
