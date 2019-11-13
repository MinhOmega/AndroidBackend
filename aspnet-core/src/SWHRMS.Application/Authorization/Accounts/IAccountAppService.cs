using System.Threading.Tasks;
using Abp.Application.Services;
using SWHRMS.Authorization.Accounts.Dto;

namespace SWHRMS.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
