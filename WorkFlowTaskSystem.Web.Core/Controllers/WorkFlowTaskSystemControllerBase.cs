using Abp.AspNetCore.Mvc.Controllers;
using WorkFlowTaskSystem.Core.Session;
using WorkFlowTaskSystem.Web.Core.Email;

namespace WorkFlowTaskSystem.Controllers
{
    public  class WorkFlowTaskSystemControllerBase : AbpController
    {
        //���ظ����AbpSession
        public new IAbpSessionExtension AbpSession { get; set; }

    }
}
