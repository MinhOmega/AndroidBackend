using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using SWHRMS.Absences.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.Absences
{
    public class AbsenceApiService: ApplicationService, IAbsenceApiService
    {
        private readonly IRepository<Absence,long> _absenceRepository;

        public AbsenceApiService(
            IRepository<Absence,long> repository)
        {
            _absenceRepository = repository;
        }

        /// <summary>
        /// Get All Absences
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<AbsenceDto>> GetAll()
        {
            var absences = await _absenceRepository.GetAllListAsync();
            return new ListResultDto<AbsenceDto>(ObjectMapper.Map<List<AbsenceDto>>(absences));
        }

        /// <summary>
        /// Get First Absence by Id.
        /// </summary>
        /// <param name="AbsenceId"></param>
        /// <returns></returns>
        public async Task<AbsenceDto> Get(long? AbsenceId)
        {
            var absence = await _absenceRepository.FirstOrDefaultAsync(x => x.Id == AbsenceId);

            if (absence == null)
            {
                return null;
            }

            return ObjectMapper.Map<AbsenceDto>(absence);
        }

        /// <summary>
        /// Get All Absences
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<AbsenceDto>> GetAllByUserId(long? UserId)
        {
            var absences = await _absenceRepository.GetAllListAsync(x => x.UserId == UserId);
            return new ListResultDto<AbsenceDto>(ObjectMapper.Map<List<AbsenceDto>>(absences));
        }

        /// <summary>
        /// Get Absence by user Id.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<AbsenceDto> GetByUserId(long? UserId)
        {
            var absence = await _absenceRepository.FirstOrDefaultAsync(x => x.UserId == UserId);

            if (absence == null)
            {
                return null;
            }

            return ObjectMapper.Map<AbsenceDto>(absence);
        }

        /// <summary>
        /// Get All Absences
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<AbsenceDto>> GetAllByUserIdBetweenDates(long? UserId, DateTime dateFrom, DateTime dateTo)
        {
            var absences = await _absenceRepository.GetAllListAsync(x => x.UserId == UserId && x.StartDate >= dateFrom && x.EndDate <= dateTo) ;
            return new ListResultDto<AbsenceDto>(ObjectMapper.Map<List<AbsenceDto>>(absences));
        }

        protected void MapToEntity(AbsenceDto input, Absence user)
        {
            ObjectMapper.Map(input, user);
        }

        protected AbsenceDto MapToEntityDto(Absence absence)
        {
            //var branch = _branchRepository.GetAllList().FirstOrDefault(b => user.BranchID == b.Id);
            //var userDto = base.MapToEntityDto(user);
            var absenceDto = ObjectMapper.Map<AbsenceDto>(absence);
            //userDto.BranchName = (branch!= null? branch.Name: "");
            return absenceDto;
        }

        public async Task<AbsenceDto> Create(CreateAbsenceDto input)
        {
            //CheckCreatePermission();
            var absence = ObjectMapper.Map<Absence>(input);
            await _absenceRepository.InsertAsync(absence);
            //CurrentUnitOfWork.SaveChanges();
            return MapToEntityDto(absence);
        }

        public async Task<AbsenceDto> Update(AbsenceDto input)
        {
            //CheckUpdatePermission();

            var absence = await _absenceRepository.GetAsync(input.Id);

            MapToEntity(input, absence);

            await _absenceRepository.UpdateAsync(absence);

            //return await Get(input);
            return ObjectMapper.Map<AbsenceDto>(absence);
        }

        public async Task<AbsenceDto> Delete(EntityDto<long> input)
        {
            var absence = new Absence();
            try
            {
                absence = await _absenceRepository.GetAsync(input.Id);
            }
            catch
            {
                throw new UserFriendlyException(L("NoAbsenceFound"));
            }
            if (absence.Status == 1)
                throw new UserFriendlyException(L("AbsenceAlreadyApproved"));
            await _absenceRepository.DeleteAsync(absence);
            return ObjectMapper.Map<AbsenceDto>(absence);
        }

        public async Task<AbsenceDto> Cancel(EntityDto<long> input)
        {
            var absence = new Absence();
            try
            {
                absence = await _absenceRepository.GetAsync(input.Id);
            }
            catch
            {
                return null;
            }

            absence.Status = 3;

            await _absenceRepository.UpdateAsync(absence);
            return ObjectMapper.Map<AbsenceDto>(absence);
            //await _absenceRepository.DeleteAsync(absence);
        }

    }
}
