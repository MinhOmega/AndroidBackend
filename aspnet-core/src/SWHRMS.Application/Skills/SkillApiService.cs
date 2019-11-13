using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using SWHRMS.Skills.Dto;
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

namespace SWHRMS.Skills
{
    public class SkillApiService : ApplicationService, ISkillApiService
    {
        private readonly IRepository<Skill, long> _skillRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<UserSkill, long> _userSkillRepository;
        private readonly IRepository<Level> _levelRepository;

        public SkillApiService(
            IRepository<Skill, long> skillRepository,
            IRepository<User, long> userRepository,
            IRepository<UserSkill, long> userSkillRepository,
            IRepository<Level> levelRepository)
        {
            _skillRepository = skillRepository;
            _userRepository = userRepository;
            _userSkillRepository = userSkillRepository;
            _levelRepository = levelRepository;
        }

        /// <summary>
        /// Get All Skills
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<SkillDto>> GetAll()
        {
            var Skills = await _skillRepository.GetAllListAsync();
            return new ListResultDto<SkillDto>(ObjectMapper.Map<List<SkillDto>>(Skills));
        }

        /// <summary>
        /// Get First Skill by Id.
        /// </summary>
        /// <param name="SkillId"></param>
        /// <returns></returns>
        public async Task<SkillDto> Get(long? SkillId)
        {
            var Skill = await _skillRepository.FirstOrDefaultAsync(x => x.Id == SkillId);

            if (Skill == null)
            {
                return null;
            }

            return ObjectMapper.Map<SkillDto>(Skill);
        }

        /// <summary>
        /// Get All Skills
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<SkillDto>> GetAllSkillsByUserId(long UserId)
        {
            var user = await _userRepository.GetAll()
                   .Include(a => a.UserSkills).ThenInclude(c => c.Skill)
                   .FirstOrDefaultAsync(x => x.Id == UserId);
            var skills = new ListResultDto<Skill>(ObjectMapper.Map<List<Skill>>(user.UserSkills));
            if (skills == null || skills.Items.Count == 0)
            {
                return new ListResultDto<SkillDto>();
            }

            return new ListResultDto<SkillDto>(ObjectMapper.Map<List<SkillDto>>(skills));
        }

        /// <summary>
        /// Get First User by Id.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<UserDto> GetUser(long UserId)
        {
            var user = await _userRepository.GetAll()
                .Include(a => a.UserSkills).ThenInclude(us => us.Skill)
                .Include(a => a.UserSkills).ThenInclude(us => us.Level)
                .FirstOrDefaultAsync(x => x.Id == UserId);

            if (user == null)
            {
                return null;
            }

            return ObjectMapper.Map<UserDto>(user);
        }

        protected void MapToEntity(SkillDto input, Skill user)
        {
            ObjectMapper.Map(input, user);
        }

        protected SkillDto MapToEntityDto(Skill Skill)
        {
            //var branch = _branchRepository.GetAllList().FirstOrDefault(b => user.BranchID == b.Id);
            //var userDto = base.MapToEntityDto(user);
            var SkillDto = ObjectMapper.Map<SkillDto>(Skill);
            //userDto.BranchName = (branch!= null? branch.Name: "");
            return SkillDto;
        }

        public async Task<SkillDto> Create(CreateSkillDto input)
        {
            //CheckCreatePermission();
            var Skill = ObjectMapper.Map<Skill>(input);
            var SkillId = await _skillRepository.InsertAndGetIdAsync(Skill);
            Skill = await _skillRepository.GetAsync(SkillId);
            //CurrentUnitOfWork.SaveChanges();
            return MapToEntityDto(Skill);
        }

        public async Task<SkillDto> Update(SkillDto input)
        {
            //CheckUpdatePermission();

            var Skill = await _skillRepository.GetAsync(input.Id);

            MapToEntity(input, Skill);

            await _skillRepository.UpdateAsync(Skill);

            //return await Get(input);
            return ObjectMapper.Map<SkillDto>(Skill);
        }

        public async Task<SkillDto> Delete(EntityDto<long> input)
        {
            var absence = new Skill();
            try
            {
                absence = await _skillRepository.GetAsync(input.Id);
            }
            catch
            {
                return null;
            }
            await _skillRepository.DeleteAsync(absence);
            return ObjectMapper.Map<SkillDto>(absence);
        }


        public async Task<ListResultDto<UserSkillDto>> UpdateUserSkills(UserSkillUpdateDto userDto)
        {
            await SetUserSkills(userDto);
            var userSkills = await _userSkillRepository.GetAllListAsync(us => us.UserId == userDto.Id && !us.IsDeleted);
            return new ListResultDto<UserSkillDto>(ObjectMapper.Map<List<UserSkillDto>>(userSkills));
        }


        public async Task SetUserSkills(UserSkillUpdateDto userDto)
        {
            foreach (var skill in userDto.UserSkills)
            {
                var userSkill = await _userSkillRepository.GetAll().FirstOrDefaultAsync(us => us.UserId == userDto.Id && us.SkillId == int.Parse(skill.SkillId));
                if (userSkill == null)
                {
                    if (skill.LevelId != "")
                    {
                        var userSkillInsert = new UserSkill();
                        userSkillInsert.UserId = userDto.Id;
                        userSkillInsert.LevelId = int.Parse(skill.LevelId);
                        userSkillInsert.SkillId = int.Parse(skill.SkillId);
                        await _userSkillRepository.InsertAsync(userSkillInsert);
                    }
                }
                else
                {
                    if (skill.LevelId == "")
                    {
                        await _userSkillRepository.DeleteAsync(userSkill);
                    }
                    else
                    {
                        userSkill.LevelId = int.Parse(skill.LevelId);
                        await _userSkillRepository.UpdateAsync(userSkill);
                    }
                }
            }
        }
    }
}
