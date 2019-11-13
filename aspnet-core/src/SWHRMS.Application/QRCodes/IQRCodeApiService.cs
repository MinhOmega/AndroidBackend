using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SWHRMS.QRCodes.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.QRCodes
{
    public interface IQRCodeApiService : IApplicationService
    {
        Task<ListResultDto<QRCodeDto>> GetAll();
        Task<QRCodeDto> Get(long? QRCodeId);
        Task<QRCodeDto> Create(CreateQRCodeDto input);
        Task<QRCodeDto> Update(QRCodeDto input);
        Task Delete(EntityDto<int> input);
    }
}
