using AutoMapper;
using SWHRMS.Authorization.Users;

namespace SWHRMS.Skills.Dto
{
    public class SkillMapProfile : Profile
    {
        public SkillMapProfile()
        {
            CreateMap<SkillDto, Skill>();
            CreateMap<SkillDto, Skill>()
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.UserSkills, opt => opt.Ignore());

            CreateMap<CreateSkillDto, Skill>();
            CreateMap<CreateSkillDto, Skill>().ForMember(x => x.UserSkills, opt => opt.Ignore());

            CreateMap<UserSkill, Skill>().ConvertUsing(us => us.Skill);
        }
    }
}
