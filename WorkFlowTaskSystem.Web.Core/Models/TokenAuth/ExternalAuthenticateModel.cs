using System.ComponentModel.DataAnnotations;

namespace WorkFlowTaskSystem.Web.Core.Models.TokenAuth
{
    public class ExternalAuthenticateModel
    {
        [Required]
        public string AuthProvider { get; set; }

        [Required]
        public string ProviderKey { get; set; }

        [Required]
        public string ProviderAccessCode { get; set; }
    }
}
