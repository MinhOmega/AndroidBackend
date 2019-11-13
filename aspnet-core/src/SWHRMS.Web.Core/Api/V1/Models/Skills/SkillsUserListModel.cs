using Abp.Authorization.Users;
using Abp.AutoMapper;
using SWHRMS.Branches;
using SWHRMS.Branches.Dto;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SWHRMS.Skills.Dto;
using SWHRMS.Levels.Dto;

namespace SWHRMS.Api.V1.Models.Skills
{
    [AutoMapFrom(typeof(UserDto))]
    public class SkillsUserListModel
    {
        [Required]
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public virtual List<UserSkillOutput> UserSkills { get; set; }

    }
}
