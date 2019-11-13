using System.Collections.Generic;
using System.Linq;
using SWHRMS.Branches.Dto;
using SWHRMS.Positions.Dto;
using SWHRMS.Roles.Dto;
using SWHRMS.Users.Dto;

namespace SWHRMS.Api.V1.Models.Users
{
    public class EditUserModalViewModel
    {
        public UserDto User { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
        public IReadOnlyList<PositionDto> Positions { get; set; }
        public IReadOnlyList<BranchDto> Branches { get; set; }

        public bool UserIsInBranch(BranchDto branch)
        {
            return User.BranchId != null && User.BranchId.Equals(branch.Id);
        }

        public bool UserIsInPosition(PositionDto position)
        {
            return User.PositionId != null && User.PositionId.Equals(position.Id);
        }
        public bool UserIsInRole(RoleDto role)
        {
            return User.RoleNames != null && User.RoleNames.Any(r => r == role.NormalizedName);
        }
    }
}
