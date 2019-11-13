using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using SWHRMS.Absences.Dto;
using SWHRMS.Authorization;
using SWHRMS.Authorization.Users;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.Absences
{

    //[AbpAuthorize(PermissionNames.Pages_Branches)]
    public class AbsenceAppService : AsyncCrudAppService<Absence, AbsenceDto, long, PagedAbsenceResultRequestDto, CreateAbsenceDto, AbsenceDto>, IAbsenceAppService
    {
        private readonly IRepository<User,long> _userRepository;

        public AbsenceAppService(
            IRepository<Absence,long> repository,
            IRepository<User, long> userRepository)
            : base(repository)
        {
            _userRepository = userRepository;
        }

        protected override IQueryable<Absence> ApplySorting(IQueryable<Absence> query, PagedAbsenceResultRequestDto input)
        {
            return query.OrderByDescending(r => r.CreationTime);
        }

        protected async Task UpdateUser(long UserId)
        {
            var currentUser = await _userRepository.GetAll()
                .Include(a => a.UserMetaInfoDetails).ThenInclude(c => c.UserMetaInfo)
                .Include(a => a.Roles)
                .Include(a => a.Branch)
                .Include(a => a.Position)
                .Include(a => a.Absences)
                .FirstOrDefaultAsync(x => x.Id == UserId);
            if (currentUser == null)
            {
                return;
            }

            var daysOffList = new List<string> { "Sunday", "Saturday" };
            var absenceDates = new List<DateTime>();
            foreach (var absence in currentUser.Absences.Where(x => !x.IsDeleted))
            {
                for (DateTime date = absence.StartDate.Date; date <= absence.EndDate.Date; date = date.AddDays(1))
                {
                    absenceDates.Add(date);
                }
            }
            absenceDates = absenceDates.Select(x => x.Date).Where(x => x.Date.Year == DateTime.Now.Year && !daysOffList.Contains(x.Date.DayOfWeek.ToString())).Distinct().ToList();
            currentUser.DaysAbsent = absenceDates.Count;
            await _userRepository.UpdateAsync(currentUser);
        }

        public override async Task<AbsenceDto> Create(CreateAbsenceDto input)
        {
            CheckCreatePermission();

            var absenceInsert = ObjectMapper.Map<Absence>(input);
            await Repository.InsertAsync(absenceInsert);

            CurrentUnitOfWork.SaveChanges();

            await UpdateUser(input.UserId);

            return MapToEntityDto(absenceInsert);
        }

        public override async Task<AbsenceDto> Update(AbsenceDto input)
        {
            CheckUpdatePermission();

            var absenceUpdate = await Repository.GetAsync(input.Id);

            MapToEntity(input, absenceUpdate);

            await Repository.UpdateAsync(absenceUpdate);

            CurrentUnitOfWork.SaveChanges();

            await UpdateUser(input.UserId);

            return await Get(input);
        }

        public override async Task Delete(EntityDto<long> input)
        {
            var deleteAbsence = await Repository.GetAsync(input.Id);
            await Repository.DeleteAsync(deleteAbsence);
            var absences = await Repository.GetAllListAsync();

            CurrentUnitOfWork.SaveChanges();
            
            await UpdateUser(deleteAbsence.UserId);
        }

        public async Task<AbsenceDto> UpdateLimited(AbsenceDto input)
        {
            CheckUpdatePermission();

            var absenceUpdate = await Repository.GetAsync(input.Id);
            if (absenceUpdate.Status == 1)
            {
                throw new UserFriendlyException(L("UpdateDenied"));
            }

            MapToEntity(input, absenceUpdate);

            await Repository.UpdateAsync(absenceUpdate);

            CurrentUnitOfWork.SaveChanges();

            await UpdateUser(input.UserId);

            return await Get(input);
        }

        public async Task DeleteLimited(EntityDto<long> input)
        {
            var deleteAbsence = await Repository.GetAsync(input.Id);
            if (deleteAbsence.Status == 1)
            {
                throw new UserFriendlyException(L("DeletionDenied"));
            }
            await Repository.DeleteAsync(deleteAbsence);
            var absences = await Repository.GetAllListAsync();

            CurrentUnitOfWork.SaveChanges();

            await UpdateUser(deleteAbsence.UserId);
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAll()
                .Include(a => a.UserMetaInfoDetails).ThenInclude(c => c.UserMetaInfo)
                .Include(a => a.Roles)
                .Include(a => a.Branch)
                .Include(a => a.Position)
                .Include(a => a.Absences).ToListAsync();
            return new ListResultDto<UserDto>(ObjectMapper.Map<List<UserDto>>(users));
        }

        /// <summary>
        /// Get First User by Id.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<UserDto> GetUser(long UserId)
        {
            var user = await _userRepository.GetAll()
                .Include(a => a.UserMetaInfoDetails).ThenInclude(c => c.UserMetaInfo)
                .Include(a => a.Roles)
                .Include(a => a.Branch)
                .Include(a => a.Position)
                .Include(a => a.Absences)
                .FirstOrDefaultAsync(x => x.Id == UserId);

            if (user == null)
            {
                return null;
            }

            return ObjectMapper.Map<UserDto>(user);
        }

    }
}
