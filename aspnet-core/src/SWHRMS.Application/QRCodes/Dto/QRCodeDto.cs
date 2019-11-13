using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SWHRMS.QRCodes;
using SWHRMS.Authorization.Users;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SWHRMS.Authorization.Users.Meta_Info;

namespace SWHRMS.QRCodes.Dto
{
    [AutoMapFrom(typeof(QRCode))]
    public class QRCodeDto : EntityDto<long>
    {
        [Required]
        public string CodeString { get; set; }
        [Required]
        public string Type { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
