using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
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
            //return View();
        }

        public IActionResult testEmails()
        {
            _emailSender.Send("1028374217@qq.com","标题 测试","内容  哈哈哈哈哈，收到了吧！");
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

        /// <summary>
        /// DownloadBigFile用于下载大文件，循环读取大文件的内容到服务器内存，然后发送给客户端浏览器
        /// </summary>
        public IActionResult DownloadBigFile()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var filePath = webRootPath + "/template/财务报表-订单明细.xlsx";
            //var filePath = @"D:\Download\测试文档.xlsx";//要下载的文件地址，这个文件会被分成片段，通过循环逐步读取到ASP.NET Core中，然后发送给客户端浏览器
            var fileName = Path.GetFileName(filePath);//测试文档.xlsx

            int bufferSize = 1024;//这就是ASP.NET Core循环读取下载文件的缓存大小，这里我们设置为了1024字节，也就是说ASP.NET Core每次会从下载文件中读取1024字节的内容到服务器内存中，然后发送到客户端浏览器，这样避免了一次将整个下载文件都加载到服务器内存中，导致服务器崩溃

            Response.ContentType = "application/vnd.ms-excel";//由于我们下载的是一个Excel文件，所以设置ContentType为application/vnd.ms-excel
             //在Response的Header中设置下载文件的文件名，这样客户端浏览器才能正确显示下载的文件名，
            //注意这里要用HttpUtility.UrlEncode编码文件名，否则有些浏览器可能会显示乱码文件名
            var contentDisposition = "attachment;" + "filename=" + HttpUtility.UrlEncode(fileName);
            
            
            Response.Headers.Add("Content-Disposition", new string[] { contentDisposition });

            //使用FileStream开始循环读取要下载文件的内容
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (Response.Body)//调用Response.Body.Dispose()并不会关闭客户端浏览器到ASP.NET Core服务器的连接，之后还可以继续往Response.Body中写入数据
                {
                    long contentLength = fs.Length;//获取下载文件的大小
                    Response.ContentLength = contentLength;//在Response的Header中设置下载文件的大小，这样客户端浏览器才能正确显示下载的进度

                    byte[] buffer;
                    long hasRead = 0;//变量hasRead用于记录已经发送了多少字节的数据到客户端浏览器

                    //如果hasRead小于contentLength，说明下载文件还没读取完毕，继续循环读取下载文件的内容，并发送到客户端浏览器
                    while (hasRead < contentLength)
                    {
                        //HttpContext.RequestAborted.IsCancellationRequested可用于检测客户端浏览器和ASP.NET Core服务器之间的连接状态，如果HttpContext.RequestAborted.IsCancellationRequested返回true，说明客户端浏览器中断了连接
                        if (HttpContext.RequestAborted.IsCancellationRequested)
                        {
                            //如果客户端浏览器中断了到ASP.NET Core服务器的连接，这里应该立刻break，取消下载文件的读取和发送，避免服务器耗费资源
                            break;
                        }

                        buffer = new byte[bufferSize];

                        int currentRead = fs.Read(buffer, 0, bufferSize);//从下载文件中读取bufferSize(1024字节)大小的内容到服务器内存中

                        Response.Body.Write(buffer, 0, currentRead);//发送读取的内容数据到客户端浏览器
                        Response.Body.Flush();//注意每次Write后，要及时调用Flush方法，及时释放服务器内存空间

                        hasRead += currentRead;//更新已经发送到客户端浏览器的字节数
                    }
                }
            }

            return new EmptyResult();
        }
    }
}
