using SWHRMS.Absences.Dto;
using SWHRMS.Levels.Dto;
using SWHRMS.Skills.Dto;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWHRMS.Web.Models.Skills
{
    public class EditUserSkillModalViewModel
    {
        public SkillsUserListModel User { get; set; }
        public IReadOnlyList<SkillDto> Skills { get; set; }
        public IReadOnlyList<LevelDto> Levels { get; set; }
        public bool UserHasSkillAtLevel(LevelDto level, SkillDto skill)
        {
            return User.UserSkills.Any(us => us.LevelId == level.Id && us.SkillId == skill.Id);
        }
    }
}
