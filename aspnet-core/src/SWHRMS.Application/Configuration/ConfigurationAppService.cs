using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Localization;
using Abp.Runtime.Session;
using SWHRMS.Configuration.Dto;

namespace SWHRMS.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : SWHRMSAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
            await SettingManager.ChangeSettingForApplicationAsync(LocalizationSettingNames.DefaultLanguage, "vi");
        }
    }
}
