using Abp.Application.Services;
using SWHRMS.Attendances.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.Attendances
{
    public interface IAttendanceAppService : IAsyncCrudAppService<AttendanceDto, long, PagedAttendanceResultRequestDto, CreateAttendanceDto, AttendanceDto>
    {
    }
}
