using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using SWHRMS.UserMetaInfoDetails.Dto;
using SWHRMS.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using SWHRMS.Authorization.Users.Meta_Info;

namespace SWHRMS.UserMetaInfoDetails
{

    //[AbpAuthorize(PermissionNames.Pages_Branches)]
    public class UserMetaInfoDetailAppService : AsyncCrudAppService<UserMetaInfoDetail, UserMetaInfoDetailDto, int, PagedUserMetaInfoDetailResultRequestDto, CreateUserMetaInfoDetailDto, UserMetaInfoDetailDto>, IUserMetaInfoDetailAppService
    {
        //private readonly IRepository<UserMetaInfoDetail> _UserMetaInfoDetailRepository;

        public UserMetaInfoDetailAppService(
            IRepository<UserMetaInfoDetail> repository)
            : base(repository)
        {

        }
    }
}
