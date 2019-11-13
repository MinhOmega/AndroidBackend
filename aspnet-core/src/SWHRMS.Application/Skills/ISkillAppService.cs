using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SWHRMS.Levels.Dto;
using SWHRMS.Skills.Dto;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.Skills
{
    public interface ISkillAppService : IAsyncCrudAppService<SkillDto, long, PagedSkillResultRequestDto, CreateSkillDto, SkillDto>
    {
        Task<ListResultDto<UserDto>> GetAllUsers();
        Task<UserDto> GetUser(long UserId);
        Task<ListResultDto<LevelDto>> GetAllLevels();
    }
}
