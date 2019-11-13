namespace SWHRMS
{
    public class AppConsts
    {
        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public const string DefaultPassPhrase = "gsKxGZ012HLL3MI5";
        /// <summary>
        /// Key create Md5 accept from WebApp
        /// </summary>
        public const string HashSecret = "7nrsPKxaM4ewYzm53gZUYMAke6D8N53u";

        /// <summary>
        /// Facebook AppId docs https://developers.facebook.com/docs/accountkit/graphapi
        /// </summary>
        public const string FacebookAppId = "gsKxGZ012HLL3MI5";
        /// <summary>
        /// Facebook AppSecret
        /// </summary>
        public const string FacebookAppSecret = "6275da317e7fd12a198ae6572fb05bbc";
        /// <summary>
        /// Get accountkit
        /// </summary>
        public const string FacebookAccessTokenUrl = "https://graph.accountkit.com/v1.3/me/?access_token=";
        /// <summary>
        /// Facebook get profile info
        /// </summary>
        public const string FacebookGetProfileUrl = "https://graph.facebook.com/v2.8/me?fields=id,email,name&access_token=";
        /// <summary>
        /// Google get profile info
        /// </summary>
        public const string GoogleGetProfileUrl = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=";
    }
}
