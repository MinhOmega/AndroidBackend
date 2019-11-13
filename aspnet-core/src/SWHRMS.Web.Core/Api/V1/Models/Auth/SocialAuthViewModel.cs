using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SWHRMS.Api.V1.Models.Auth
{
  
    public  class SocialAuthViewModel
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string LoginType { get; set; }

        public string DeviceToken { get; set; }
        public string DeviceId { get; set; }
        public string Platform { get; set; }

        //Need update phone
        public string PhoneNumber { get; set; }
        public string AccessTokenAKI { get; set; }
        public string AccountKitId { get; set; }
    }
    internal class SocialUserData
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        //[JsonProperty("first_name")]
        //public string FirstName { get; set; }
        //[JsonProperty("last_name")]
        //public string LastName { get; set; }
   
    }

}
