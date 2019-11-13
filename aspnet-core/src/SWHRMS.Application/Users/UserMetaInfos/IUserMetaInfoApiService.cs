using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SWHRMS.UserMetaInfos.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.UserMetaInfos
{
    public interface IUserMetaInfoApiService : IApplicationService
    {
        Task<ListResultDto<UserMetaInfoDto>> GetAll();
        Task<UserMetaInfoDto> Get(int? UserMetaInfoId);
        Task<UserMetaInfoDto> Create(CreateUserMetaInfoDto input);
        Task<UserMetaInfoDto> Update(UserMetaInfoDto input);
        Task Delete(EntityDto<int> input);
    }
}
