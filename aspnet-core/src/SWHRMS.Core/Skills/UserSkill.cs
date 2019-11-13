using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using SWHRMS.Authorization.Users;
using SWHRMS.Levels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SWHRMS.Skills
{
    [Table("UserSkills")]
    public class UserSkill : Entity<long>, IFullAudited
    {
        public int LevelId { get; set; }
        public Level Level { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public long SkillId { get; set; }
        public Skill Skill { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
