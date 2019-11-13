namespace SWHRMS.Api.V1.Models.Auth
{
    public class LoginResultModel
    {
        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public long UserId { get; set; }

        public int? TenantId { get; set; }
    }
}
