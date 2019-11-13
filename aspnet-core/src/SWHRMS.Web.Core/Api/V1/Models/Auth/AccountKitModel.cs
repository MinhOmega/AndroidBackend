using Newtonsoft.Json;

namespace SWHRMS.Api.V1.Models.Auth
{
    public class AccountKitModel
    {
        //AccountKit ID
        [JsonProperty("id")]
        public string Id { get; set; }
        public string NationalNumber { get; set; }
        
    }
}
