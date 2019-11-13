using Abp.Application.Services;
using SWHRMS.UserMetaInfoDetails.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.UserMetaInfoDetails
{
    public interface IUserMetaInfoDetailAppService : IAsyncCrudAppService<UserMetaInfoDetailDto, int, PagedUserMetaInfoDetailResultRequestDto, CreateUserMetaInfoDetailDto, UserMetaInfoDetailDto>
    {
    }
}
