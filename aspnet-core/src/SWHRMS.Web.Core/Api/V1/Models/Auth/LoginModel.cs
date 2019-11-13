using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;

namespace SWHRMS.Api.V1.Models.Auth
{
    public class LoginModel
    {
        [Required]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string Username { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        public string Password { get; set; }

        public string DeviceToken { get; set; }
        public string DeviceId { get; set; }
        public string Platform { get; set; }
        public bool RememberClient { get; set; }
    }
}
