using System.ComponentModel.DataAnnotations;

namespace SWHRMS.Api.V1.Models.Auth
{
    public class UpdatePhoneNumberModel
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string AccessTokenAKI { get; set; }

        [Required]
        public string AccountKitId { get; set; }
    }
}
