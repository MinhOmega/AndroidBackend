using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SWHRMS.Authorization.Users.Meta_Info
{
    [Table("UserMetaInfoDetails")]
    public class UserMetaInfoDetail : Entity, IFullAudited
    {
        public string InfoDetail { get; set; }

        [Required]
        public virtual long UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        public virtual int UserMetaInfoId { get; set; }
        public virtual UserMetaInfo UserMetaInfo { get; set; }

        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
