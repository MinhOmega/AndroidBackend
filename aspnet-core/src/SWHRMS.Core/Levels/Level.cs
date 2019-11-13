using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using SWHRMS.Authorization.Users;
using SWHRMS.Skills;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWHRMS.Levels
{
    [Table("Level")]
    public class Level : Entity, IFullAudited
    {
        public Level() { }
        public Level(string name, string description, int progress)
        {
            Name = name;
            Description = description;
            Progress = progress;
        }

        [StringLength(250)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }
        public virtual List<UserSkill> UserSkills { get; set; }

        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get ; set ; }
        public long? LastModifierUserId { get ; set ; }
        public DateTime? LastModificationTime { get ; set ; }
        public long? DeleterUserId { get ; set ; }
        public DateTime? DeletionTime { get ; set ; }
        public bool IsDeleted { get ; set ; }
    }
}
