using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;

namespace SWHRMS.Web.Views
{
    public abstract class SWHRMSRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected SWHRMSRazorPage()
        {
            LocalizationSourceName = SWHRMSConsts.LocalizationSourceName;
        }
    }
}
