using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using SWHRMS.Authorization.Users;
using SWHRMS.Branches;
using SWHRMS.QRCodes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SWHRMS.Attendances
{
    [Table("Attendance")]
    public class Attendance : Entity<long>, IFullAudited
    {
        [Required]
        public int Type { get; set; }
        [Required]
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        [Required]
        public long UserId { get; set; }
        public User User { get; set; }
        public string CreatorQRCode{ get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get ; set ; }
        public long? LastModifierUserId { get ; set ; }
        public DateTime? LastModificationTime { get ; set ; }
        public long? DeleterUserId { get ; set ; }
        public DateTime? DeletionTime { get ; set ; }
        public bool IsDeleted { get ; set ; }
    }
}
