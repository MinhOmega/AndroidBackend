using Abp.AutoMapper;
using SWHRMS.Authentication.External;

namespace SWHRMS.Api.V1.Models.Auth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
