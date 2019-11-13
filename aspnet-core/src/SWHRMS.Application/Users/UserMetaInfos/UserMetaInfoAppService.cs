using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using SWHRMS.UserMetaInfos.Dto;
using SWHRMS.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using SWHRMS.Authorization.Users.Meta_Info;

namespace SWHRMS.UserMetaInfos
{

    //[AbpAuthorize(PermissionNames.Pages_Branches)]
    public class UserMetaInfoAppService : AsyncCrudAppService<UserMetaInfo, UserMetaInfoDto, int, PagedUserMetaInfoResultRequestDto, CreateUserMetaInfoDto, UserMetaInfoDto>, IUserMetaInfoAppService
    {
        //private readonly IRepository<UserMetaInfo> _UserMetaInfoRepository;

        public UserMetaInfoAppService(
            IRepository<UserMetaInfo> repository)
            : base(repository)
        {

        }
    }
}
