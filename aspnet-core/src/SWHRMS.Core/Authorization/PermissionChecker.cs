using Abp.Authorization;
using SWHRMS.Authorization.Roles;
using SWHRMS.Authorization.Users;

namespace SWHRMS.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
