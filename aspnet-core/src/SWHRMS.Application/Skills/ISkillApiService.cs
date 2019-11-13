using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SWHRMS.Skills.Dto;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.Skills
{
    public interface ISkillApiService : IApplicationService
    {
        Task<ListResultDto<SkillDto>> GetAll();
        Task<SkillDto> Get(long? SkillId);
        Task<ListResultDto<SkillDto>> GetAllSkillsByUserId(long UserId);
        Task<UserDto> GetUser(long UserId);
        Task<ListResultDto<UserSkillDto>> UpdateUserSkills(UserSkillUpdateDto userDto);
        Task<SkillDto> Create(CreateSkillDto input);
        Task<SkillDto> Update(SkillDto input);
        Task<SkillDto> Delete(EntityDto<long> input);
    }
}
