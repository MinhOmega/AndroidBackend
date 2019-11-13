using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using SWHRMS.Levels.Dto;
using SWHRMS.Branches;
using SWHRMS.QRCodes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SWHRMS.Authorization.Users;
using Microsoft.EntityFrameworkCore;
using SWHRMS.Levels;
using SWHRMS.Users.Dto;

namespace SWHRMS.Levels
{
    public class LevelApiService : ApplicationService, ILevelApiService
    {
        private readonly IRepository<Level, int> _levelRepository;
        private readonly IRepository<User, long> _userRepository;

        public LevelApiService(
            IRepository<Level, int> levelRepository,
            IRepository<User, long> userRepository)
        {
            _levelRepository = levelRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get All Levels
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<LevelDto>> GetAll()
        {
            var Levels = await _levelRepository.GetAllListAsync();
            return new ListResultDto<LevelDto>(ObjectMapper.Map<List<LevelDto>>(Levels));
        }

        /// <summary>
        /// Get First Level by Id.
        /// </summary>
        /// <param name="LevelId"></param>
        /// <returns></returns>
        public async Task<LevelDto> Get(int? LevelId)
        {
            var Level = await _levelRepository.FirstOrDefaultAsync(x => x.Id == LevelId);

            if (Level == null)
            {
                return null;
            }

            return ObjectMapper.Map<LevelDto>(Level);
        }
        protected void MapToEntity(LevelDto input, Level user)
        {
            ObjectMapper.Map(input, user);
        }

        protected LevelDto MapToEntityDto(Level Level)
        {
            //var branch = _branchRepository.GetAllList().FirstOrDefault(b => user.BranchID == b.Id);
            //var userDto = base.MapToEntityDto(user);
            var LevelDto = ObjectMapper.Map<LevelDto>(Level);
            //userDto.BranchName = (branch!= null? branch.Name: "");
            return LevelDto;
        }

        public async Task<LevelDto> Create(CreateLevelDto input)
        {
            //CheckCreatePermission();
            var Level = ObjectMapper.Map<Level>(input);
            var LevelId = await _levelRepository.InsertAndGetIdAsync(Level);
            Level = await _levelRepository.GetAsync(LevelId);
            //CurrentUnitOfWork.SaveChanges();
            return MapToEntityDto(Level);
        }

        public async Task<LevelDto> Update(LevelDto input)
        {
            //CheckUpdatePermission();

            var Level = await _levelRepository.GetAsync(input.Id);

            MapToEntity(input, Level);

            await _levelRepository.UpdateAsync(Level);

            //return await Get(input);
            return ObjectMapper.Map<LevelDto>(Level);
        }

        public async Task<LevelDto> Delete(EntityDto<int> input)
        {
            var absence = new Level();
            try
            {
                absence = await _levelRepository.GetAsync(input.Id);
            }
            catch
            {
                return null;
            }
            await _levelRepository.DeleteAsync(absence);
            return ObjectMapper.Map<LevelDto>(absence);
        }
    }
}
