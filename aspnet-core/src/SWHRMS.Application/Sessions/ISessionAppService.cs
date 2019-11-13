using System.Threading.Tasks;
using Abp.Application.Services;
using SWHRMS.Sessions.Dto;

namespace SWHRMS.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
