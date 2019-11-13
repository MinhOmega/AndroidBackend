namespace SWHRMS.Authentication.External
{
    public class ExternalAuthUserInfo
    {
        public string ProviderKey { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Surname { get; set; }

        public string Provider { get; set; }
        public string Platform { get; set; }
        public string Devices { get; set; }
    }
}
