using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using SWHRMS.Authorization.Users;
using SWHRMS.Branches;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SWHRMS.QRCodes
{
    [Table("QRCode")]
    public class QRCode : Entity<long>, IFullAudited
    {
        [Required]
        public string CodeString { get; set; }
        [Required]
        public string Type { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get ; set ; }
        public long? LastModifierUserId { get ; set ; }
        public DateTime? LastModificationTime { get ; set ; }
        public long? DeleterUserId { get ; set ; }
        public DateTime? DeletionTime { get ; set ; }
        public bool IsDeleted { get ; set ; }
    }
}
