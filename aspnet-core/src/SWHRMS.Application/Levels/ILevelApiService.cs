using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SWHRMS.Levels.Dto;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.Levels
{
    public interface ILevelApiService : IApplicationService
    {
        Task<ListResultDto<LevelDto>> GetAll();
        Task<LevelDto> Get(int? LevelId);
        Task<LevelDto> Create(CreateLevelDto input);
        Task<LevelDto> Update(LevelDto input);
        Task<LevelDto> Delete(EntityDto<int> input);
    }
}
