using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SWHRMS.Authorization.Users.Meta_Info
{
    [Table("UserMetaInfos")]
    public class UserMetaInfo : Entity, IFullAudited
    {
        public UserMetaInfo() { }
        public UserMetaInfo(string infoName)
        {
            InfoName = infoName;
        }

        //[Required]
        //[Key]
        //public int BranchID { get; set; }
        [StringLength(250)]
        public string InfoName { get; set; }
        public string Description { get; set; }

        public virtual List<UserMetaInfoDetail> UserMetaInfoDetails { get; set; }

        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
