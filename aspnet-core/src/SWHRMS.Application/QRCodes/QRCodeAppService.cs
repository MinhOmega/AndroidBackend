using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using SWHRMS.QRCodes.Dto;
using SWHRMS.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using SWHRMS.Authorization.Users.Meta_Info;

namespace SWHRMS.QRCodes
{

    //[AbpAuthorize(PermissionNames.Pages_Branches)]
    public class QRCodeAppService : AsyncCrudAppService<QRCode, QRCodeDto, long, PagedQRCodeResultRequestDto, CreateQRCodeDto, QRCodeDto>, IQRCodeAppService
    {
        //private readonly IRepository<QRCode> _QRCodeRepository;

        public QRCodeAppService(
            IRepository<QRCode,long> repository)
            : base(repository)
        {

        }
    }
}
