using Abp.AutoMapper;
using SWHRMS.Skills;
using System.Collections.Generic;

namespace SWHRMS.Levels.Dto
{
    [AutoMapTo(typeof(Level))]
    public class CreateLevelDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }
        public virtual List<UserSkill> UserSkills { get; set; }
    }
}
