using Abp.Application.Services;
using SWHRMS.Positions.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.Positions
{
    public interface IPositionAppService : IAsyncCrudAppService<PositionDto, int, PagedPositionResultRequestDto, CreatePositionDto, PositionDto>
    {
    }
}
