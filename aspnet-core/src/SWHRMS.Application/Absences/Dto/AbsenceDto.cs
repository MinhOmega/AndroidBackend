using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SWHRMS.Authorization.Users;
using SWHRMS.Branches;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.Absences.Dto
{
    [AutoMapFrom(typeof(Absence))]
    public class AbsenceDto : EntityDto<long>
    {
        public AbsenceDto()
        {
        }
        public AbsenceDto(long absenceId, DateTime startDate, DateTime endDate, string details, int status, long userId)
        {
            Id = absenceId;
            StartDate = startDate;
            EndDate = endDate;
            Details = details;
            Status = status;
            UserId = userId;
        }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string Details { get; set; }
        public int Status { get; set; }

        [Required]
        public long UserId { get; set; }
        public virtual UserAbsence User { get; set; }

        public DateTime CreationTime { get; set; }
        public class UserAbsence
        {
            public long Id { get; set; }
            public string UserName { get; set; }
            public string FullName { get; set; }
            public int? BranchId { get; set; }
            public Branch Branch { get; set; }
        }
    }
}
