using System.Collections.Generic;
using SWHRMS.Roles.Dto;

namespace SWHRMS.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}