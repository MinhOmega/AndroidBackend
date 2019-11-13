using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SWHRMS.Authorization;

namespace SWHRMS
{
    [DependsOn(
        typeof(SWHRMSCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class SWHRMSApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<SWHRMSAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(SWHRMSApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
