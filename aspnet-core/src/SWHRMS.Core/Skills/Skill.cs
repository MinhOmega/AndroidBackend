using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using SWHRMS.Authorization.Users;
using SWHRMS.Branches;
using SWHRMS.Levels;
using SWHRMS.QRCodes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SWHRMS.Skills
{
    [Table("Skill")]
    public class Skill : Entity<long>, IFullAudited
    {
        public string SkillName { get; set; }
        public string ColorCode { get; set; }
        public List<UserSkill> UserSkills { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get ; set ; }
        public long? LastModifierUserId { get ; set ; }
        public DateTime? LastModificationTime { get ; set ; }
        public long? DeleterUserId { get ; set ; }
        public DateTime? DeletionTime { get ; set ; }
        public bool IsDeleted { get ; set ; }
    }
}
