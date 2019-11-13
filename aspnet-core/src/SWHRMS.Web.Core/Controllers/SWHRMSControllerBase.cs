using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace SWHRMS.Controllers
{
    public abstract class SWHRMSControllerBase: AbpController
    {
        protected SWHRMSControllerBase()
        {
            LocalizationSourceName = SWHRMSConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
