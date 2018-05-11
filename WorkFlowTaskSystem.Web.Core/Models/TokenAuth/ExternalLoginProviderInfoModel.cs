using Abp.AutoMapper;

namespace WorkFlowTaskSystem.Web.Core.Models.TokenAuth
{
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
