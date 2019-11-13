using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using SWHRMS.Skills.Dto;
using SWHRMS.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using SWHRMS.Users.Dto;
using SWHRMS.Authorization.Users;
using Microsoft.EntityFrameworkCore;
using SWHRMS.Levels.Dto;
using SWHRMS.Levels;
using System.Linq;
using Abp.Domain.Entities;

namespace SWHRMS.Skills
{

    //[AbpAuthorize(PermissionNames.Pages_Branches)]
    public class SkillAppService : AsyncCrudAppService<Skill, SkillDto, long, PagedSkillResultRequestDto, CreateSkillDto, SkillDto>, ISkillAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<UserSkill, long> _userSkillRepository;
        private readonly IRepository<Level> _levelRepository;

        public SkillAppService(
            IRepository<Skill, long> repository,
            IRepository<User, long> userRepository,
            IRepository<UserSkill, long> userSkillRepository,
             IRepository<Level> levelRepository)
            : base(repository)
        {
            _userRepository = userRepository;
            _userSkillRepository = userSkillRepository;
            _levelRepository = levelRepository;
        }

        protected override IQueryable<Skill> CreateFilteredQuery(PagedSkillResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.UserSkills);
        }

        protected override async Task<Skill> GetEntityByIdAsync(long id)
        {
            //var user = await Repository.GetAllIncluding(x => x.Roles, x => x.Branch, x => x.Position, x => x.UserMetaInfoDetails).FirstOrDefaultAsync(x => x.Id == id);
            var skill = await Repository.GetAll()
                .Include(a => a.UserSkills)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (skill == null)
            {
                throw new EntityNotFoundException(typeof(Skill), id);
            }

            return skill;
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
                .Include(a => a.UserSkills).ThenInclude(us => us.Skill)
                .Include(a => a.UserSkills).ThenInclude(us => us.Level)
                .ToListAsync();
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
                .Include(a => a.UserSkills).ThenInclude(us => us.Skill)
                .Include(a => a.UserSkills).ThenInclude(us => us.Level)
                .FirstOrDefaultAsync(x => x.Id == UserId);

            if (user == null)
            {
                return null;
            }

            return ObjectMapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Get all available level of ability.
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<LevelDto>> GetAllLevels()
        {
            var levels = await _levelRepository.GetAll()
                .Include(l => l.UserSkills)
                .ToListAsync();

            return new ListResultDto<LevelDto>(ObjectMapper.Map<List<LevelDto>>(levels));
        }

        public async Task<ListResultDto<UserSkillDto>> UpdateUserSkills(UserSkillUpdateDto userDto)
        {
            await SetUserSkills(userDto);
            var userSkills = await _userSkillRepository.GetAllListAsync(us => us.UserId == userDto.Id);
            return new ListResultDto<UserSkillDto>(ObjectMapper.Map<List<UserSkillDto>>(userSkills));
        }


        public async Task SetUserSkills(UserSkillUpdateDto userDto)
        {
            foreach (var skill in userDto.UserSkills)
            {
                var userSkill = await _userSkillRepository.GetAll().FirstOrDefaultAsync(us => us.UserId == userDto.Id && us.SkillId == int.Parse(skill.SkillId));
                if (userSkill == null )
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
