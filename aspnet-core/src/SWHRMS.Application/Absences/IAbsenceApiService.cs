using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SWHRMS.Absences.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.Absences
{
    public interface IAbsenceApiService : IApplicationService
    {
        Task<ListResultDto<AbsenceDto>> GetAll();
        Task<AbsenceDto> Get(long? AbsenceId);
        Task<ListResultDto<AbsenceDto>> GetAllByUserId(long? UserId);
        Task<AbsenceDto> GetByUserId(long? UserId);
        Task<ListResultDto<AbsenceDto>> GetAllByUserIdBetweenDates(long? UserId, DateTime dateFrom, DateTime dateTo);
        Task<AbsenceDto> Create(CreateAbsenceDto input);
        Task<AbsenceDto> Update(AbsenceDto input);
        Task<AbsenceDto> Delete(EntityDto<long> input);
        Task<AbsenceDto> Cancel(EntityDto<long> input);
    }
}
