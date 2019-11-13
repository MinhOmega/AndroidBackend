using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using SWHRMS.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SWHRMS.Positions
{
    [Table("Position")]
    public class Position : Entity, IFullAudited
    {
        //[Required]
        //[Key]
        //public int BranchID { get; set; }
        [StringLength(250)]
        public string PositionName { get; set; }
        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public virtual List<User> Users { get; set; }
        public long? CreatorUserId { get ; set ; }
        public long? LastModifierUserId { get ; set ; }
        public DateTime? LastModificationTime { get ; set ; }
        public long? DeleterUserId { get ; set ; }
        public DateTime? DeletionTime { get ; set ; }
        public bool IsDeleted { get ; set ; }
    }
}
