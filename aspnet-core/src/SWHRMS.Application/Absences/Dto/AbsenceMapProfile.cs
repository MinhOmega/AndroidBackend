using AutoMapper;
using SWHRMS.Authorization.Users;

namespace SWHRMS.Absences.Dto
{
    public class AbsenceMapProfile : Profile
    {
        public AbsenceMapProfile()
        {
            CreateMap<AbsenceDto, Absence>();
            CreateMap<AbsenceDto, Absence>()
                .ForMember(x => x.User, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore());

            CreateMap<CreateAbsenceDto, Absence>();
            CreateMap<CreateAbsenceDto, Absence>().ForMember(x => x.User, opt => opt.Ignore());
        }
    }
}
