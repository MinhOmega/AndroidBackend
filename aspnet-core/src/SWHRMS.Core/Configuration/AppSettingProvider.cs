using System.Collections.Generic;
using Abp.Configuration;
using Abp.Localization;
using Abp.Zero.Configuration;

namespace SWHRMS.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "white", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: true),
                new SettingDefinition(
                           AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireDigit,
                           "false",
                           new FixedLocalizableString("Require digit."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           ),

               new SettingDefinition(
                           AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireLowercase,
                           "false",
                           new FixedLocalizableString("Require lowercase."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           ),

               new SettingDefinition(
                           AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireNonAlphanumeric,
                           "false",
                           new FixedLocalizableString("Require non alphanumeric."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           ),

               new SettingDefinition(
                           AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireUppercase,
                           "false",
                           new FixedLocalizableString("Require upper case."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           ),

               new SettingDefinition(
                           AbpZeroSettingNames.UserManagement.PasswordComplexity.RequiredLength,
                           "3",
                           new FixedLocalizableString("Required length."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           )
            };
        }
    }
}
