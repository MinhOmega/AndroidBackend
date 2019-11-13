using System.Threading.Tasks;
using SWHRMS.Configuration.Dto;

namespace SWHRMS.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
