using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SWHRMS.UserMetaInfoDetails;
using SWHRMS.Authorization.Users;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SWHRMS.Authorization.Users.Meta_Info;

namespace SWHRMS.UserMetaInfoDetails.Dto
{
    [AutoMapFrom(typeof(UserMetaInfoDetail))]
    public class UserMetaInfoDetailDto : EntityDto
    {
        public string InfoDetail { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreationTime { get; set; }

        [Required]
        public virtual long UserId { get; set; }
        public virtual UserData User { get; set; }
        [Required]
        public virtual int UserMetaInfoId { get; set; }
        public virtual UserMetaInfoData UserMetaInfo { get; set; }

        public class UserData
        {
            public long Id { get; set; }
            public string UserName { get; set; }
        }

        public class UserMetaInfoData
        {
            public long Id { get; set; }
            public string InfoName { get; set; }
        }
    }
}
