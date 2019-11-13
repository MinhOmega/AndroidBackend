using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using SWHRMS.Absences.Dto;
using SWHRMS.Authorization.Users;
using SWHRMS.Authorization.Users.Meta_Info;
using SWHRMS.Branches;
using SWHRMS.Branches.Dto;
using SWHRMS.Positions;
using SWHRMS.Positions.Dto;
using SWHRMS.Skills.Dto;
using SWHRMS.UserMetaInfoDetails.Dto;

namespace SWHRMS.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserDto : EntityDto<long>
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

        public bool IsActive { get; set; }

        public string FullName { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime CreationTime { get; set; }

        public string[] RoleNames { get; set; }
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
        public BranchDto Branch { get; set; }
        public int? PositionId { get; set; }
        public PositionDto Position { get; set; }
        public int? DaysAbsent { get; set; }
        public List<MetaInfoStringDto> MetaInfos { get;set;}

        public virtual List<AbsenceDto> Absences { get; set; }
        public virtual List<UserSkillDto> UserSkills { get; set; }
        public virtual List<UserMetaInfoDetailDto> UserMetaInfoDetails { get; set; }
    }
}
