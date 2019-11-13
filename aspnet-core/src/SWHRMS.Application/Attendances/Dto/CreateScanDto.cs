using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.Attendances.Dto
{
    [AutoMapTo(typeof(Attendance))]
    public class CreateScan
    {
        public CreateScan()
        {
        }
        public CreateScan(string qrcode, long? userId)
        {
            CreatorQRCode = qrcode;
            UserId = userId;
        }

        [Required]
        public string CreatorQRCode { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public long? UserId { get; set; }
    }
}
