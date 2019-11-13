using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Http;
using SWHRMS.Absences;
using SWHRMS.Absences.Dto;
using SWHRMS.Api.V1.Models.Absences;
using SWHRMS.Authorization;
using SWHRMS.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.Api.V1.Controllers
{
    [ApiVersion("1")]
    [Route("api/v1/[controller]")]
    public class AbsencesController : SWHRMSControllerBase
    {
        private readonly IAbsenceApiService _absencesApiService;
        private readonly IAbpSession _abpSession;

        public AbsencesController(
            IAbsenceApiService absenceApiService,
            IAbpSession abpSession)
        {
            _absencesApiService = absenceApiService;
            _abpSession = abpSession;
        }

        /// <summary>
        /// Get all absence booking infos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        [Route("")]
        public async Task<AbsencesListViewModel> Index()
        {
            var absences = (await _absencesApiService.GetAllByUserId(_abpSession.UserId)).Items; // Paging not implemented yet
            return new AbsencesListViewModel
            {
                Absences = absences
            };
        }

        /// <summary>
        /// Get absence booking infos between date range.
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        [HttpGet]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        [Route("{dateFrom:DateTime}/{dateTo:DateTime}")]
        public async Task<AbsencesListViewModel> GetBetweenDates([FromRoute] DateTime dateFrom, [FromRoute]  DateTime dateTo)
        {
            var absences = (await _absencesApiService.GetAllByUserIdBetweenDates(_abpSession.UserId, dateFrom, dateTo)).Items; // Paging not implemented yet
            return new AbsencesListViewModel
            {
                Absences = absences
            };
        }

        /// <summary>
        /// Create new absence booking.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        [Route("[action]")]
        public async Task<AbsenceDto> Create([FromBody] CreateAbsenceModel model)
        {
            var absenceDto = new CreateAbsenceDto(model.StartDate, model.EndDate, model.Details, 0, model.UserId);
            return (await _absencesApiService.Create(absenceDto)); 

            //return Json(new { Msg = L("CreateAbsenceSuccess") });
        }

        /// <summary>
        /// Update absence booking detail/Change status.
        /// </summary>
        /// <param name="absenceId">Absence Id to update.</param>
        /// <param name="model">Update model</param>
        /// <returns></returns>
        [HttpPut]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        [Route("{absenceId:long}/[action]")]
        public async Task<AbsenceDto> Update([FromRoute] long absenceId, [FromBody] UpdateAbsenceModel model)
        {
            var absenceDto = new AbsenceDto(absenceId, model.StartDate, model.EndDate, model.Details, model.Status, model.UserId);
            return (await _absencesApiService.Update(absenceDto)); 

            //return Json(new { Msg = L("UpdateAbsenceSuccess") });
        }

        /// <summary>
        /// Soft delete absence booking.
        /// </summary>
        /// <param name="absenceId"></param>
        /// <returns></returns>
        [HttpDelete]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        [Route("{absenceId:long}/[action]")]
        public async Task<JsonResult> Delete([FromRoute] long absenceId)
        {
            var absenceDto = new EntityDto<long>(absenceId);
            absenceDto = await _absencesApiService.Delete(absenceDto); 

            return Json(new { Msg = L("DeleteAbsenceSuccess") });
        }
    }
}
