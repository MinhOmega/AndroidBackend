using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SWHRMS.UserMetaInfos;
using SWHRMS.Authorization.Users;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SWHRMS.Authorization.Users.Meta_Info;
using SWHRMS.UserMetaInfoDetails.Dto;

namespace SWHRMS.UserMetaInfos.Dto
{
    [AutoMapFrom(typeof(UserMetaInfo))]
    public class UserMetaInfoDto : EntityDto
    {
        //[Required]
        //[Key]
        //public int BranchID { get; set; }
        [StringLength(250)]
        public string InfoName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreationTime { get; set; }

        public virtual List<UserMetaInfoDetailDto> UserMetaInfoDetails { get; set; }
    }
}
