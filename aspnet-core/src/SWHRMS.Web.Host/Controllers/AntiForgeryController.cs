using Microsoft.AspNetCore.Antiforgery;
using SWHRMS.Controllers;

namespace SWHRMS.Web.Host.Controllers
{
    public class AntiForgeryController : SWHRMSControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
