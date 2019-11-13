using System.ComponentModel.DataAnnotations;

namespace SWHRMS.Api.V1.Models.Auth
{
    public class ResetPasswordModel
    {
        [Required]
        public string TokenVerify { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
