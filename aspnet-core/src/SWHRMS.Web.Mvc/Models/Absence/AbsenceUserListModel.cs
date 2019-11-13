using Abp.Authorization.Users;
using Abp.AutoMapper;
using SWHRMS.Absences.Dto;
using SWHRMS.Branches;
using SWHRMS.Branches.Dto;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWHRMS.Web.Models.Absence
{
    [AutoMapFrom(typeof(UserDto))]
    public class AbsenceUserListModel
    {
        [Required]
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int? BranchId { get; set; }
        public BranchDto Branch { get; set; }
        public virtual List<AbsenceDto> Absences { get; set; }
        public int? DaysAbsent { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
