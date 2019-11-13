using Abp.AutoMapper;
using Abp.Runtime.Validation;
using SWHRMS.Authorization.Users;
using SWHRMS.Authorization.Users.Meta_Info;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.UserMetaInfoDetails.Dto
{
    [AutoMapTo(typeof(UserMetaInfoDetail))]
    public class CreateUserMetaInfoDetailDto
    {
        public string InfoDetail { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public virtual long UserId { get; set; }
        [Required]
        public virtual int UserMetaInfoId { get; set; }
    }
}
