using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SWHRMS.Authorization.Users;
using SWHRMS.Branches.Dto;
using SWHRMS.Positions.Dto;
using SWHRMS.Roles.Dto;
using SWHRMS.Users.Dto;
using System.Threading.Tasks;

namespace SWHRMS.Users
{
    public interface IUserApiService : IApplicationService
    {
        Task<ListResultDto<UserDto>> GetAll();
        Task<UserDto> Get(long UserId);
        Task<ListResultDto<RoleDto>> GetRoles();
        Task<ListResultDto<BranchDto>> GetBranches();
        Task<ListResultDto<PositionDto>> GetPositions();
        Task<UserDto> Create(CreateUserDto input);
        Task<UserDto> Update(UserDto input);
        Task<UserDto> Delete(EntityDto<long> input);
        Task ChangeLanguage(ChangeUserLanguageDto input);
        Task<User> FindByPhoneNumber(string phone);
    }
}
