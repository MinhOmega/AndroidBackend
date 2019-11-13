using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SWHRMS.Attendances;
using SWHRMS.Authorization.Users;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.Attendances.Dto
{
    [AutoMapFrom(typeof(Attendance))]
    public class AttendanceDto : EntityDto<long>
    {
        public AttendanceDto()
        {
        }
        public AttendanceDto(long AttendanceId, int type, int branchId, long userId)
        {
            Id = Id;
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
        public long UserId { get; set; }
        public string CreatorQRCode { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
