using Abp.AutoMapper;
using Abp.Runtime.Validation;
using SWHRMS.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.Skills.Dto
{
    [AutoMapTo(typeof(Skill))]
    public class CreateSkillDto
    {
        public string SkillName { get; set; }
        public string ColorCode { get; set; }
        public List<UserSkill> UserSkills { get; set; }
    }
}
