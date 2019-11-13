using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using SWHRMS.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SWHRMS.Branches
{
    [Table("Branch")]
    public class Branch : Entity, IFullAudited
    {
        //[Required]
        //[Key]
        //public int BranchID { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Location { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public virtual List<User> Users { get; set; }

        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get ; set ; }
        public long? LastModifierUserId { get ; set ; }
        public DateTime? LastModificationTime { get ; set ; }
        public long? DeleterUserId { get ; set ; }
        public DateTime? DeletionTime { get ; set ; }
        public bool IsDeleted { get ; set ; }
    }
}
