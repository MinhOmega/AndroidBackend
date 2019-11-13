using Abp.AutoMapper;
using SWHRMS.Skills.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.Api.V1.Models.Skills
{
    [AutoMapFrom(typeof(UserSkillDto))]
    public class UserSkillOutput
    {
        public int LevelId { get; set; }
        public LevelOutput Level { get; set; }
        public long SkillId { get; set; }
        public SkillOutput Skill { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
