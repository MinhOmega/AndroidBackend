using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using SWHRMS.Authorization;
using SWHRMS.Controllers;
using SWHRMS.Users;
using SWHRMS.Web.Models.Users;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SWHRMS.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class UsersController : SWHRMSControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task<ActionResult> Index()
        {
            var users = (await _userAppService.GetAll(new PagedUserResultRequestDto {MaxResultCount = int.MaxValue})).Items; // Paging not implemented yet
            var roles = (await _userAppService.GetRoles()).Items;
            var model = new UserListViewModel
            {
                Users = users,
                Roles = roles
            };
            return View(model);
        }

        [Route("[controller]/{userId:long}/[action]")]
        public async Task<ActionResult> Edit(long userId)
        {
            var user = await _userAppService.Get(new EntityDto<long>(userId));
            var roles = (await _userAppService.GetRoles()).Items;
            var branches = (await _userAppService.GetBranches()).Items;
            var positions = (await _userAppService.GetPositions()).Items;
            var metaInfos = (await _userAppService.GetMetaInfos()).Items;

            //var daysOffList = new List<string> { "Sunday", "Saturday" };
            //var absenceDates = new List<DateTime>();
            //foreach (var absence in user.Absences)
            //{
            //    for (DateTime date = absence.StartDate; date <= absence.EndDate; date = date.AddDays(1))
            //    {
            //        absenceDates.Add(date);
            //    }
            //}
            //absenceDates = absenceDates.Select(x => x.Date).Where(x => !daysOffList.Contains(x.Date.DayOfWeek.ToString())).Distinct().ToList();

            var model = new EditUserModalViewModel
            {
                User = user,
                Branches = branches,
                Positions = positions,
                Roles = roles,
                UserMetaInfos = metaInfos
            };
            //return View("_EditUserModal", model);
            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            var user = new UserDto();
            var roles = (await _userAppService.GetRoles()).Items;
            var branches = (await _userAppService.GetBranches()).Items;
            var positions = (await _userAppService.GetPositions()).Items;
            var metaInfos = (await _userAppService.GetMetaInfos()).Items;
            var model = new EditUserModalViewModel
            {
                User = user,
                Branches = branches,
                Positions = positions,
                Roles = roles,
                UserMetaInfos = metaInfos
            };
            //return View("_CreateUserModal", model);
            return View(model);
        }
    }
}
