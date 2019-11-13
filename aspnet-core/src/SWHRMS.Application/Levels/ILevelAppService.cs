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
    public interface ILevelAppService : IAsyncCrudAppService<LevelDto, int, PagedLevelResultRequestDto, CreateLevelDto, LevelDto>
    {
    }
}
