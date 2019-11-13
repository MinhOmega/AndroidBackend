using Abp.Application.Navigation;
using Abp.Localization;
using SWHRMS.Authorization;

namespace SWHRMS.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class SWHRMSNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "home",
                        requiresAuthentication: true
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Tenants,
                        L("Tenants"),
                        url: "Tenants",
                        icon: "business",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                    )
                ).AddItem( 
                    new MenuItemDefinition(
                        "SkillMultiLevel",
                        L("Skills"),
                        icon: "work"
                    ).AddItem(
                        new MenuItemDefinition(
                             PageNames.Skills,
                            L("SkillsProfile"),
                            url: "Skills",
                            icon: "",
                            requiredPermissionName: PermissionNames.Page_Skills
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                             PageNames.SkillsManage,
                            L("SkillsManage"),
                            url: "SkillsManager",
                            icon: "",
                            requiredPermissionName: PermissionNames.Page_SkillsManage
                        )
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        "AbsenceMultiLevel",
                        L("Absence"),
                        icon: "event"
                    ).AddItem(
                        new MenuItemDefinition(
                             PageNames.Absence,
                            L("AbsenceBooking"),
                            url: "Absences",
                            icon: "",
                            requiredPermissionName: PermissionNames.Page_Absence
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                             PageNames.AbsenceManage,
                            L("AbsenceManage"),
                            url: "AbsencesManager",
                            icon: "",
                            requiredPermissionName: PermissionNames.Page_AbsenceManage
                        )
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Employees"),
                        url: "Users",
                        icon: "people",
                        requiredPermissionName: PermissionNames.Pages_Users
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        L("Roles"),
                        url: "Roles",
                        icon: "local_offer",
                        requiredPermissionName: PermissionNames.Pages_Roles
                    )
                )
            //.AddItem(
            //    new MenuItemDefinition(
            //        PageNames.About,
            //        L("About"),
            //        url: "About",
            //        icon: "info"
            //    )
            //)
            ;
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SWHRMSConsts.LocalizationSourceName);
        }
    }
}
