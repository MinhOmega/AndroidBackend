using Abp.Authorization.Users;

using System.ComponentModel.DataAnnotations;

namespace SWHRMS.Api.V1.Models.Auth
{
    public class UpdateProfileModel
    {
 

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }


        [StringLength(AbpUserBase.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }
        [StringLength(299)]
        public string Address { get; set; }
        [StringLength(299)]
        public string DateOfBirth { get; set; }
        [StringLength(299)]
        public string Gender { get; set; }
        [StringLength(299)]
        public string EmailAddress { get; set; }
        [StringLength(299)]
        public string AvatarUrl { get; set; }
    }
}
