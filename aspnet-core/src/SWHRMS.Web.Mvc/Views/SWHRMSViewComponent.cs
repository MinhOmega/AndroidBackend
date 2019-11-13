using Abp.AspNetCore.Mvc.ViewComponents;

namespace SWHRMS.Web.Views
{
    public abstract class SWHRMSViewComponent : AbpViewComponent
    {
        protected SWHRMSViewComponent()
        {
            LocalizationSourceName = SWHRMSConsts.LocalizationSourceName;
        }
    }
}
