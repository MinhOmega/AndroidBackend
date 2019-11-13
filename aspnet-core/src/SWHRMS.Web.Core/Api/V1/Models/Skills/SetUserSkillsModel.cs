using SWHRMS.Skills.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.Api.V1.Models.Skills
{
    public class SetUserSkillsModel
    {
        public long Id { get; set; }
        public virtual List<UserSkillStringDto> UserSkills { get; set; }
    }
}
