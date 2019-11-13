using System.Collections.Generic;
using SWHRMS.Roles.Dto;
using SWHRMS.Users.Dto;

namespace SWHRMS.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
