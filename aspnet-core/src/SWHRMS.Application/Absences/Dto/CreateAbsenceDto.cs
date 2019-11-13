using Abp.AutoMapper;
using Abp.Runtime.Validation;
using SWHRMS.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.Absences.Dto
{
    [AutoMapTo(typeof(Absence))]
    public class CreateAbsenceDto : IShouldNormalize
    {
        public CreateAbsenceDto()
        {
        }

        public CreateAbsenceDto(DateTime? startDate, DateTime? endDate, string details, int status, long userId)
        {
            StartDate = startDate;
            EndDate = endDate;
            Details = details;
            Status = status;
            UserId = userId;
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Details { get; set; }
        public int Status { get; set; }

        public long UserId { get; set; }

        public void Normalize()
        {

        }
    }
}
