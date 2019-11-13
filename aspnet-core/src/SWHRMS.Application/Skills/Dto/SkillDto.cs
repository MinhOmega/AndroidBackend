using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SWHRMS.Skills;
using SWHRMS.Authorization.Users;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.Skills.Dto
{
    [AutoMapFrom(typeof(Skill))]
    public class SkillDto : EntityDto<long>
    {
        public string SkillName { get; set; }
        public string ColorCode { get; set; }
        public List<UserSkillDto> UserSkills { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
