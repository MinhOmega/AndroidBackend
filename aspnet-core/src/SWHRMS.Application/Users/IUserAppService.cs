using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SWHRMS.Branches.Dto;
using SWHRMS.Positions.Dto;
using SWHRMS.Roles.Dto;
using SWHRMS.UserMetaInfos.Dto;
using SWHRMS.Users.Dto;

namespace SWHRMS.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<UserDto> UpdateLimited(UserDto input);

        Task<ListResultDto<UserMetaInfoDto>> GetMetaInfos();
        Task<ListResultDto<RoleDto>> GetRoles();
        Task<ListResultDto<BranchDto>> GetBranches();
        Task<ListResultDto<PositionDto>> GetPositions();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
