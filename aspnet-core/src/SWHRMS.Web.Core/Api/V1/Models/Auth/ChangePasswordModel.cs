using Abp.Authorization.Users;
using System.ComponentModel.DataAnnotations;

namespace SWHRMS.Api.V1.Models.Auth
{
    public class ChangePasswordModel
    {
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        public string ConfirmNewPassword { get; set; }
        

    }
}
