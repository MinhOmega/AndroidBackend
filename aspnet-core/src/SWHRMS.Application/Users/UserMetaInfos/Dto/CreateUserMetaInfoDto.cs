using Abp.AutoMapper;
using Abp.Runtime.Validation;
using SWHRMS.Authorization.Users;
using SWHRMS.Authorization.Users.Meta_Info;
using SWHRMS.UserMetaInfoDetails.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.UserMetaInfos.Dto
{
    [AutoMapTo(typeof(UserMetaInfo))]
    public class CreateUserMetaInfoDto
    {
        //[Required]
        //[Key]
        //public int BranchID { get; set; }
        [StringLength(250)]
        public string InfoName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual List<UserMetaInfoDetailDto> UserMetaInfoDetails { get; set; }
    }
}
