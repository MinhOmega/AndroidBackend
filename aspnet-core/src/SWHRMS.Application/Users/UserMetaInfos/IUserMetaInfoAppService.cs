using Abp.Application.Services;
using SWHRMS.UserMetaInfos.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.UserMetaInfos
{
    public interface IUserMetaInfoAppService : IAsyncCrudAppService<UserMetaInfoDto, int, PagedUserMetaInfoResultRequestDto, CreateUserMetaInfoDto, UserMetaInfoDto>
    {
    }
}
