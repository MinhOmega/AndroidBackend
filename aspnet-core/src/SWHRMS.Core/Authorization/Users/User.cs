using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Users;
using Abp.Extensions;
using SWHRMS.Absences;
using SWHRMS.Authorization.Users.Meta_Info;
using SWHRMS.Branches;
using SWHRMS.Positions;
using SWHRMS.Skills;

namespace SWHRMS.Authorization.Users
{
    public class User : AbpUser<User>
    {
        [Display(Name = "BirthDay")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDay { get; set; }
        public  string Address { get; set; }
        public  string AddressTemporal { get; set; }
        public  string AccGit { get; set; }
        public  string AccRedmine { get; set; }
        public  string SocialInsuranceNo { get; set; }
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
        public int? PositionId { get; set; }
        //[ForeignKey("PositionId")]
        public Position Position { get; set; }
        public  int? BranchId { get; set; }
        //[ForeignKey("BranchID")]
        public Branch Branch { get; set; }
        public virtual List<Absence> Absences { get; set; }
        public List<UserSkill> UserSkills { get; set; }
        public virtual List<UserMetaInfoDetail> UserMetaInfoDetails { get; set; }
        public  int? DaysAbsent { get; set; }
        public const string DefaultPassword = "123qwe";
        [StringLength(1000)]
        public string Devices { get; set; }
        [StringLength(20)]
        public string Platform { get; set; }
        public DateTime? LastLogin { get; set; }

        [StringLength(199)]
        public string RequestPasswordHash { get; set; }
        [StringLength(50)]
        public string RequestExpired { get; set; }
        public bool IsChangePassword { get; set; } = false;
        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }
    }
}
