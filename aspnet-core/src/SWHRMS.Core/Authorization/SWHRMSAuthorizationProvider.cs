using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace SWHRMS.Authorization
{
    public class SWHRMSAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Page_Skills, L("Skills"));
            context.CreatePermission(PermissionNames.Page_SkillsManage, L("SkillsManage"));
            context.CreatePermission(PermissionNames.Page_Absence, L("Absense"));
            context.CreatePermission(PermissionNames.Page_AbsenceManage, L("AbsenseManage"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SWHRMSConsts.LocalizationSourceName);
        }
    }
}
