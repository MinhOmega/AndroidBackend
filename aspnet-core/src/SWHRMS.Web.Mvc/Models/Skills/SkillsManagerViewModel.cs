using SWHRMS.Levels.Dto;
using SWHRMS.Skills.Dto;
using SWHRMS.Users.Dto;
using SWHRMS.Web.Models.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWHRMS.Web.Models.Skills
{
    public class SkillsManagerViewModel
    {
        public IReadOnlyList<SkillsUserListModel> Users { get; set; }
        public IReadOnlyList<SkillDto> Skills { get; set; }
        public IReadOnlyList<LevelDto> Levels { get; set; }
    }
}
