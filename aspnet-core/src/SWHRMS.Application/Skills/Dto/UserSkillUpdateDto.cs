using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using SWHRMS.Absences.Dto;
using SWHRMS.Authorization.Users;
using SWHRMS.Authorization.Users.Meta_Info;
using SWHRMS.Branches;
using SWHRMS.Branches.Dto;
using SWHRMS.Positions;
using SWHRMS.Positions.Dto;
using SWHRMS.Skills.Dto;
using SWHRMS.UserMetaInfoDetails.Dto;

namespace SWHRMS.Users.Dto
{
    [AutoMapTo(typeof(User))]
    public class UserSkillUpdateDto : EntityDto<long>
    {
        public virtual List<UserSkillStringDto> UserSkills { get; set; }
    }
}
