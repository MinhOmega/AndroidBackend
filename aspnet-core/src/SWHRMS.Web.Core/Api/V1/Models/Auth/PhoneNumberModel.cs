using System.ComponentModel.DataAnnotations;


namespace SWHRMS.Api.V1.Models.Auth
{
    public class PhoneNumberModel
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string SecureHash { get; set; }
    }
}
