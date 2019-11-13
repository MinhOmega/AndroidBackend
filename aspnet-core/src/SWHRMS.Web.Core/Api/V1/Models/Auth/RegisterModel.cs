using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Extensions;
using SWHRMS.Validation;

namespace SWHRMS.Api.V1.Models.Auth
{
    public class RegisterModel
    {
        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string UserName { get; set; }

        public string FullName { get; set; }
        [StringLength(AbpUserBase.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }
      
        [Required]
        public string TypeReg { get; set; }
        //AccessToken of account kit
        public string AccessTokenAKI { get; set; }
        public string AccountKitId { get; set; }
        public string DeviceToken { get; set; }
        public string DeviceId { get; set; }
        public string Platform { get; set; }
        public string EmailAddress { get; set; }



        [Required]
        public string Password { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (!PhoneNumber.IsNullOrEmpty())
        //    {
        //        if (!ValidationHelper.IsPhoneNumber(PhoneNumber))
        //        {
        //            yield return new ValidationResult("Please provide a valid phone number!");
        //        }
        //    }

        //    if (!EmailAddress.IsNullOrEmpty())
        //    {
        //        if (!ValidationHelper.IsEmail(EmailAddress))
        //        {
        //            yield return new ValidationResult("Please provide a valid email address!");
        //        }
        //    }
        //}
    }
}
