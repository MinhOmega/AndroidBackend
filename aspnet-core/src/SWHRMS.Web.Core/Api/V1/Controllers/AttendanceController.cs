using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Http;
using SWHRMS.Attendances;
using SWHRMS.Attendances.Dto;
using SWHRMS.Api.V1.Models.Attendances;
using SWHRMS.Authorization;
using SWHRMS.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.UI;

namespace SWHRMS.Api.V1.Controllers
{
    [ApiVersion("1")]
    [Route("api/v1/[controller]")]
    public class AttendancesController: SWHRMSControllerBase
    {
        private readonly IAttendanceApiService _AttendancesApiService;
        private readonly IAbpSession _abpSession;

        public AttendancesController(
            IAttendanceApiService AttendanceApiService,
            IAbpSession abpSession)
        {
            _AttendancesApiService = AttendanceApiService;
            _abpSession = abpSession;
        }

        //[HttpGet]
        //[AbpAuthorize]
        //[IgnoreAntiforgeryToken]
        //public async Task<AttendancesListViewModel> Index()
        //{
        //    var Attendances = (await _AttendancesApiService.GetAllByUserId(_abpSession.UserId)).Items; // Paging not implemented yet
        //    return new AttendancesListViewModel
        //    {
        //        Attendances = Attendances
        //    };
        //}

        //[HttpGet]
        //[AbpAuthorize]
        //[IgnoreAntiforgeryToken]
        //public async Task<AttendancesListViewModel> GetBetweenDates([FromQuery] DateTime dateFrom, [FromQuery]  DateTime dateTo)
        //{
        //    var Attendances = (await _AttendancesApiService.GetAllByUserIdBetweenDates(_abpSession.UserId, dateFrom, dateTo)).Items; // Paging not implemented yet
        //    return new AttendancesListViewModel
        //    {
        //        Attendances = Attendances
        //    };
        //}

        [HttpPost]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        [Route("[action]")]
        public async Task<AttendanceDto> Create([FromBody] CreateAttendanceModel model)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException(L("PleaseLogin"));
            }
            var AttendanceDto = new CreateAttendanceDto(model.Type, model.BranchId, _abpSession.UserId);
            return (await _AttendancesApiService.Create(AttendanceDto)); // Paging not implemented yet

            //return Json(new { Msg = L("CreateAttendanceSuccess") });
        }

        [HttpPost]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        [Route("[action]")]
        public async Task<AttendanceDto> Scan([FromBody] PunchInViewModel model)
        {
            try
            {
                if (_abpSession.UserId == null)
                {
                    throw new UserFriendlyException(L("PleaseLogin"));
                }
                var AttendanceDto = new CreateScan(model.QRCode, _abpSession.UserId);
                return (await _AttendancesApiService.CreateScan(AttendanceDto)); // Paging not implemented yet

            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(L(ex.Message));
            }
            //return Json(new { Msg = L("CreateAttendanceSuccess") });
        }

        //[HttpPost]
        //[AbpAuthorize]
        //[IgnoreAntiforgeryToken]
        //public async Task<AttendanceDto> UpdateAttendance([FromBody] UpdateAttendanceModel model)
        //{
        //    var AttendanceDto = new AttendanceDto(model.Id, model.Type, model.BranchId, model.UserId);
        //    return (await _AttendancesApiService.Update(AttendanceDto)); // Paging not implemented yet

        //    //return Json(new { Msg = L("UpdateAttendanceSuccess") });
        //}

        //[HttpPost]
        //[AbpAuthorize]
        //[IgnoreAntiforgeryToken]
        //public async Task<JsonResult> DeleteAttendance([FromBody] int AttendanceId)
        //{
        //    var AttendanceDto = new EntityDto<int>(AttendanceId);
        //    await _AttendancesApiService.Delete(AttendanceDto); // Paging not implemented yet

        //    return Json(new { Msg = L("DeleteAttendanceSuccess") });
        //}
    }
}
