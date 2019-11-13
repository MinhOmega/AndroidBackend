using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SWHRMS.Attendances.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.Attendances
{
    public interface IAttendanceApiService : IApplicationService
    {
        Task<ListResultDto<AttendanceDto>> GetAll();
        Task<AttendanceDto> Get(long? AttendanceId);
        Task<ListResultDto<AttendanceDto>> GetAllByUserId(long? UserId);
        Task<AttendanceDto> GetByUserId(long? UserId);
        Task<ListResultDto<AttendanceDto>> GetAllByUserIdBetweenDates(long? UserId, DateTime dateFrom, DateTime dateTo);
        Task<AttendanceDto> Create(CreateAttendanceDto input);
        Task<AttendanceDto> CreateScan(CreateScan input);
        Task<AttendanceDto> Update(AttendanceDto input);
        Task Delete(EntityDto<long> input);
    }
}
