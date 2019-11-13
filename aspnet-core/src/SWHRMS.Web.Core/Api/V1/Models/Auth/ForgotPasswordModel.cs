using Abp.Authorization.Users;
using Abp.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SWHRMS.Validation;

namespace SWHRMS.Api.V1.Models.Auth
{
    public class ForgotPasswordModel
    {
        //[Required]
        //public string AccessTokenAKI { get; set; }
        //[Required]
        //public string AccountKitId { get; set; }
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserNameOrEmail { get; set; }
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (!PhoneNumber.IsNullOrEmpty())
        //    {
        //        if (!ValidationHelper.IsPhoneNumber(PhoneNumber))
        //        {
        //            yield return new ValidationResult("Please provide a valid phone number!");
        //        }
        //    }

        //}
    }
}
