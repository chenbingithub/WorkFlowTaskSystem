namespace WorkFlowTaskSystem.Web.Core.Models.TokenAuth
{
    public class AuthenticateResultModel
    {
        public string AccessToken { get; set; }

        public object EncryptedAccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public string UserId { get; set; }
    }
}
