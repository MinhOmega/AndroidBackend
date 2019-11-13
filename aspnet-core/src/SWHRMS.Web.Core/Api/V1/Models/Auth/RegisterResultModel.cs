namespace SWHRMS.Api.V1.Models.Auth
{
    public class RegisterResultModel
    {
        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public long UserId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
