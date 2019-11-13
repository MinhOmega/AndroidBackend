using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using SWHRMS.Authorization;
using SWHRMS.Positions;
using SWHRMS.Positions.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.Positions
{

    //[AbpAuthorize(PermissionNames.Pages_Positiones)]
    public class PositionAppService : AsyncCrudAppService<Position, PositionDto, int, PagedPositionResultRequestDto, CreatePositionDto, PositionDto>, IPositionAppService
    {
        //private readonly IRepository<Position> _PositionRepository;

        public PositionAppService(
            IRepository<Position> repository)
            : base(repository)
        {

        }
    }
}
