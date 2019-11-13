using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SWHRMS.Absences.Dto;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.Absences
{
    public interface IAbsenceAppService : IAsyncCrudAppService<AbsenceDto, long, PagedAbsenceResultRequestDto, CreateAbsenceDto, AbsenceDto>
    {
        Task<ListResultDto<UserDto>> GetAllUsers();
        Task<UserDto> GetUser(long UserId);
        Task<AbsenceDto> UpdateLimited(AbsenceDto input);
        Task DeleteLimited(EntityDto<long> input);
    }
}
