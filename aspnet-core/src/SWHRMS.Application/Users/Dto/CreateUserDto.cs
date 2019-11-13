using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using SWHRMS.Authorization.Users;
using SWHRMS.Branches;
using SWHRMS.Positions;

namespace SWHRMS.Users.Dto
{
    [AutoMapTo(typeof(User))]
    public class CreateUserDto : IShouldNormalize
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [Display(Name = "BirthDay")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string AddressTemporal { get; set; }
        public string AccGit { get; set; }
        public string AccRedmine { get; set; }
        public string SocialInsuranceNo { get; set; }
        public string IdentityCardNo { get; set; }
        public DateTime? IdentityCardIssueDate { get; set; }
        public string IdentityCardIssuePlace { get; set; }
        public string BankAccountNo { get; set; }
        public string BankAccountName { get; set; }
        public string CompanyEmail { get; set; }
        public int ContractType { get; set; }
        public int WorkingStatus { get; set; }
        public DateTime? TrialStartDate { get; set; }
        public DateTime? OfficialStartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? BranchId { get; set; }
        public int? PositionId { get; set; }
        public int? DaysAbsent { get; set; }

        public bool IsActive { get; set; }

        public string[] RoleNames { get; set; }
        public List<MetaInfoStringDto> MetaInfos { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }


        public void Normalize()
        {
            if (RoleNames == null)
            {
                RoleNames = new string[0];
            }
        }
    }
}
