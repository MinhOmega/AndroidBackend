using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SWHRMS.Positions.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.Positions
{
    public interface IPositionApiService : IApplicationService
    {
        Task<ListResultDto<PositionDto>> GetAll();
        Task<PositionDto> Get(int? PositionId);
        Task<PositionDto> Create(CreatePositionDto input);
        Task<PositionDto> Update(PositionDto input);
        Task Delete(EntityDto<int> input);
    }
}
