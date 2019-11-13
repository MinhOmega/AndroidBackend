using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SWHRMS.Absences;
using SWHRMS.Authorization;
using SWHRMS.Controllers;
using SWHRMS.EntityFrameworkCore;
using SWHRMS.Users;
using SWHRMS.Users.Dto;
using SWHRMS.Web.Models.Absence;

namespace SWHRMS.Web.Mvc.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Page_Absence)]
    public class AbsencesController : SWHRMSControllerBase
    {
        private readonly IAbsenceAppService _absenceAppService;
        private readonly IAbpSession _abpSession;

        public AbsencesController(
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
            var users =  (await _absenceAppService.GetAllUsers()).Items; // Paging not implemented yet
            var currentUser =  await _absenceAppService.GetUser(_abpSession.UserId ?? default(long));

            var userAbsences = ObjectMapper.Map<List<AbsenceUserListModel>>(users);
            var daysOffList = new List<string> { "Sunday", "Saturday" };
            var absenceDates = new List<DateTime>();
            foreach (var absence in currentUser.Absences)
            {
                for (DateTime date = absence.StartDate; date <= absence.EndDate; date = date.AddDays(1))
                {
                    absenceDates.Add(date);
                }
            }
            absenceDates = absenceDates.Select(x => x.Date).Where(x => x.Date.Year == DateTime.Now.Year && !daysOffList.Contains(x.Date.DayOfWeek.ToString())).Distinct().ToList();
            var model = new AbsenceViewModel
            {
                Users = userAbsences,
                CurrentUser = currentUser,
                AbsenceDates = absenceDates
            };
            return View(model);
        }

        public async Task<ActionResult> EditAbsenceModal(long absenceId)
        {
            var users = (await _absenceAppService.GetAllUsers()).Items;
            var userAbsences = ObjectMapper.Map<List<AbsenceUserListModel>>(users);
            var absence = await _absenceAppService.Get(new EntityDto<long>(absenceId));
            var model = new EditAbsenceModalViewModel
            {
                Users = userAbsences,
                Absence = absence
            };
            return View("_EditAbsenceModal", model);
        }

    }
}
