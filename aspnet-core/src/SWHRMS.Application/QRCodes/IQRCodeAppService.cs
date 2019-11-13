using Abp.Application.Services;
using SWHRMS.QRCodes.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.QRCodes
{
    public interface IQRCodeAppService : IAsyncCrudAppService<QRCodeDto, long, PagedQRCodeResultRequestDto, CreateQRCodeDto, QRCodeDto>
    {
    }
}
