using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using SWHRMS.Attendances.Dto;
using SWHRMS.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.Attendances
{

    //[AbpAuthorize(PermissionNames.Pages_Branches)]
    public class AttendanceAppService : AsyncCrudAppService<Attendance, AttendanceDto, long, PagedAttendanceResultRequestDto, CreateAttendanceDto, AttendanceDto>, IAttendanceAppService
    {
        //private readonly IRepository<Attendance> _AttendanceRepository;

        public AttendanceAppService(
            IRepository<Attendance,long> repository)
            : base(repository)
        {

        }
    }
}
