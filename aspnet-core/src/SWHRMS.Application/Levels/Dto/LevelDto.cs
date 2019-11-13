using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using SWHRMS.Skills;
using SWHRMS.Skills.Dto;

namespace SWHRMS.Levels.Dto
{
    [AutoMapFrom(typeof(Level))]
    public class LevelDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }
        public virtual List<UserSkillDto> UserSkills { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
