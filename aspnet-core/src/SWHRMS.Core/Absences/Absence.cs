using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using SWHRMS.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SWHRMS.Absences
{
    [Table("Absence")]
    public class Absence : Entity<long>, IFullAudited
    {
        [Display(Name = "StartDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Display(Name = "EndDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public string Details { get; set; }
        public int Status { get; set; }

        public long UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get ; set ; }
        public long? LastModifierUserId { get ; set ; }
        public DateTime? LastModificationTime { get ; set ; }
        public long? DeleterUserId { get ; set ; }
        public DateTime? DeletionTime { get ; set ; }
        public bool IsDeleted { get ; set ; }
    }
}
