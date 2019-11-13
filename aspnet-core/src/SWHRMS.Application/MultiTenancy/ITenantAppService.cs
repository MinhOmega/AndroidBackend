using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SWHRMS.MultiTenancy.Dto;

namespace SWHRMS.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

