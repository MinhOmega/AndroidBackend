using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using SWHRMS.Controllers;

namespace SWHRMS.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : SWHRMSControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
