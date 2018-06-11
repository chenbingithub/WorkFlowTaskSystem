using System.ComponentModel.DataAnnotations;

namespace WorkFlowTaskSystem.Web.Core.Models.TokenAuth
{
    public class AuthenticateModel
    {
        [Required]
        public string UserNameOrEmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
        
        public bool RememberClient { get; set; }
    }

    public class ChangePwdModel
    {
        public string NewPass { get; set; }
        public string OldPass { get; set; }
        public string RePass { get; set; }
        
    }
}
