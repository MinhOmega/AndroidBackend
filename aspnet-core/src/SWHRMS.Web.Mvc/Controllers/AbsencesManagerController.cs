using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using SWHRMS.Absences;
using SWHRMS.Absences.Dto;
using SWHRMS.Authorization;
using SWHRMS.Controllers;
using SWHRMS.Users;
using SWHRMS.Users.Dto;
using SWHRMS.Web.Models.Absence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWHRMS.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Page_AbsenceManage)]
    public class AbsencesManagerController : SWHRMSControllerBase
    {
        private readonly IAbsenceAppService _absenceAppService;
        private readonly IAbpSession _abpSession;

        public AbsencesManagerController(
            IAbsenceAppService absenceAppService,
            IAbpSession abpSession)
        {
            _absenceAppService = absenceAppService;
            _abpSession = abpSession;
        }

        // GET: Absences
        public async Task<IActionResult> Index()
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException(L("PleaseLogin"));
            }
            var users = (await _absenceAppService.GetAllUsers()).Items; // Paging not implemented yet
            var absences = (await _absenceAppService.GetAll(new PagedAbsenceResultRequestDto { MaxResultCount = int.MaxValue })).Items;

            var userAbsences = ObjectMapper.Map<List<AbsenceUserListModel>>(users);
            var model = new AbsenceManagerViewModel
            {
                Users = userAbsences,
                Absences = absences
            };
            return View(model);
        }

        public async Task<AbsenceDto> AbsenceApproval(long absenceId, int status)
        {
            var absence = await _absenceAppService.Get(new EntityDto<long>(absenceId));
            absence.Status = status;
            absence = await _absenceAppService.Update(absence);
            return absence;
        }

        public async Task<ActionResult> AbsenceCalendar(long absenceId, int status)
        {
            var users = (await _absenceAppService.GetAllUsers()).Items;
            var userAbsences = ObjectMapper.Map<List<AbsenceUserListModel>>(users);
            var model = new AbsenceCalendarPartialViewModelViewModel
            {
                Users = userAbsences
            };
            return View("_AbsenceCalendarPartial", model);
        }

        public async Task<ActionResult> GetUserAbsenceTable(long absenceId, int status)
        {
            var users = (await _absenceAppService.GetAllUsers()).Items;
            var userAbsences = ObjectMapper.Map<List<AbsenceUserListModel>>(users);
            var model = new AbsenceCalendarPartialViewModelViewModel
            {
                Users = userAbsences
            };
            return View("_UserAbsenceTablePartial", model);
        }
    }
}
