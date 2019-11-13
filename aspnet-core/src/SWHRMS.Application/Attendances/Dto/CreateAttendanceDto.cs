using Abp.AutoMapper;
using Abp.Runtime.Validation;
using SWHRMS.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.Attendances.Dto
{
    [AutoMapTo(typeof(Attendance))]
    public class CreateAttendanceDto
    {
        public CreateAttendanceDto()
        {
        }
        public CreateAttendanceDto(int type, int branchId, long? userId)
        {
            Type = type;
            BranchId = branchId;
            UserId = userId;
        }

        //[Required]
        //[Key]
        //public int BranchID { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public int BranchId { get; set; }
        [Required]
        public long? UserId { get; set; }
    }
}
