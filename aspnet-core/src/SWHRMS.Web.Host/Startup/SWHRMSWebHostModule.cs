using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SWHRMS.Configuration;

namespace SWHRMS.Web.Host.Startup
{
    [DependsOn(
       typeof(SWHRMSWebCoreModule))]
    public class SWHRMSWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public SWHRMSWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SWHRMSWebHostModule).GetAssembly());
        }
    }
}
