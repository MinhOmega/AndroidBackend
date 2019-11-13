using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using SWHRMS.Levels.Dto;
using SWHRMS.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using SWHRMS.Users.Dto;
using SWHRMS.Authorization.Users;
using Microsoft.EntityFrameworkCore;
using SWHRMS.Levels;
using System.Linq;
using Abp.Domain.Entities;

namespace SWHRMS.Levels
{

    //[AbpAuthorize(PermissionNames.Pages_Branches)]
    public class LevelAppService : AsyncCrudAppService<Level, LevelDto, int, PagedLevelResultRequestDto, CreateLevelDto, LevelDto>, ILevelAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Level> _levelRepository;

        public LevelAppService(
            IRepository<Level, int> repository,
            IRepository<User, long> userRepository)
            : base(repository)
        {
            _userRepository = userRepository;
        }

    }
}
