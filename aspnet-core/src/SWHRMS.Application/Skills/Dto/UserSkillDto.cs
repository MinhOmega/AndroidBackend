using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SWHRMS.Levels.Dto;
using SWHRMS.Users.Dto;
using System;

namespace SWHRMS.Skills.Dto
{
    [AutoMapFrom(typeof(UserSkill))]
    public class UserSkillDto: EntityDto<long>
    {
        public int LevelId { get; set; }
        public LevelDto Level { get; set; }
        public long UserId { get; set; }
        public UserDto User { get; set; }
        public long SkillId { get; set; }
        public SkillDto Skill { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
