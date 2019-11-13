using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SWHRMS.UserMetaInfoDetails.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.UserMetaInfoDetails
{
    public interface IUserMetaInfoDetailApiService : IApplicationService
    {
        Task<ListResultDto<UserMetaInfoDetailDto>> GetAll();
        Task<UserMetaInfoDetailDto> Get(int? UserMetaInfoDetailId);
        Task<ListResultDto<UserMetaInfoDetailDto>> GetAllByUserId(long? UserId);
        Task<UserMetaInfoDetailDto> GetByUserId(long? UserId);
        Task<UserMetaInfoDetailDto> Create(CreateUserMetaInfoDetailDto input);
        Task<UserMetaInfoDetailDto> Update(UserMetaInfoDetailDto input);
        Task Delete(EntityDto<int> input);
    }
}
