using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkFlowTaskSystem.Web.Core.Configuration;

namespace WorkFlowTaskSystem.Web.Core.Controllers
{
   public class MyAbpUserConfigurationController : AbpController
    {
        private readonly MyAbpUserConfigurationBuilder _abpUserConfigurationBuilder;

        public MyAbpUserConfigurationController(MyAbpUserConfigurationBuilder abpUserConfigurationBuilder)
        {
            _abpUserConfigurationBuilder = abpUserConfigurationBuilder;
        }

        public async Task<JsonResult> GetAll()
        {
            var userConfig = await _abpUserConfigurationBuilder.GetAll();
            return Json(userConfig);
        }
    }
}
