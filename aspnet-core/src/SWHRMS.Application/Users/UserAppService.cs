using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using SWHRMS.Authorization;
using SWHRMS.Authorization.Accounts;
using SWHRMS.Authorization.Roles;
using SWHRMS.Authorization.Users;
using SWHRMS.Roles.Dto;
using SWHRMS.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SWHRMS.Branches.Dto;
using SWHRMS.Branches;
using SWHRMS.Positions.Dto;
using SWHRMS.Positions;
using SWHRMS.Authorization.Users.Meta_Info;
using SWHRMS.UserMetaInfos.Dto;

namespace SWHRMS.Users
{
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IRepository<Position> _positionRepository;
        private readonly IRepository<UserMetaInfo> _userMetaRepository;
        private readonly IRepository<UserMetaInfoDetail> _userMetaDetailRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IRepository<Branch> branchRepository,
            IRepository<Position> positionRepository,
            IRepository<UserMetaInfo> userMetaRepository,
            IRepository<UserMetaInfoDetail> userMetaDetailRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager)
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _branchRepository = branchRepository;
            _positionRepository = positionRepository;
            _userMetaRepository = userMetaRepository;
            _userMetaDetailRepository = userMetaDetailRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
        }

        public override async Task<UserDto> Create(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));
            user = await Repository.FirstOrDefaultAsync(u => u.UserName == input.UserName);

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }
            if (input.MetaInfos != null)
            {
                await SetMetaInfos(user.Id, input.MetaInfos);
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        public override async Task<UserDto> Update(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }
            if (input.MetaInfos != null)
            {
                await SetMetaInfos(input.Id,input.MetaInfos);
            }

            return await Get(input);
        }


        public async Task<UserDto> UpdateLimited(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));
            if (input.MetaInfos != null)
            {
                await SetMetaInfos(input.Id, input.MetaInfos);
            }

            return await Get(input);
        }

        public override async Task Delete(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ListResultDto<UserMetaInfoDto>> GetMetaInfos()
        {
            var userMetaInfos = await _userMetaRepository.GetAllListAsync();
            return new ListResultDto<UserMetaInfoDto>(ObjectMapper.Map<List<UserMetaInfoDto>>(userMetaInfos));
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task<ListResultDto<BranchDto>> GetBranches()
        {
            var branches = await _branchRepository.GetAllListAsync();
            return new ListResultDto<BranchDto>(ObjectMapper.Map<List<BranchDto>>(branches));
        }

        public async Task<ListResultDto<PositionDto>> GetPositions()
        {
            var positions = await _positionRepository.GetAllListAsync();
            return new ListResultDto<PositionDto>(ObjectMapper.Map<List<PositionDto>>(positions));
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roles = _roleManager.Roles.Where(r => user.Roles.Any(ur => ur.RoleId == r.Id)).Select(r => r.NormalizedName);
            //var metainfodetails = _userMetaDetailRepository.GetAllList(m => user.Id == m.UserId);
            //var branch = _branchRepository.GetAllList().FirstOrDefault(b => user.BranchID == b.Id);
            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();
            //userDto.BranchName = (branch!= null? branch.Name: "");
            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles, x => x.Branch, x => x.Position, x=>x.Absences)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            //var user = await Repository.GetAllIncluding(x => x.Roles, x => x.Branch, x => x.Position, x => x.UserMetaInfoDetails).FirstOrDefaultAsync(x => x.Id == id);
            var user = await Repository.GetAll()
                .Include(a => a.UserMetaInfoDetails).ThenInclude(c => c.UserMetaInfo)
                .Include(a => a.Roles)
                .Include(a => a.Branch)
                .Include(a => a.Position)
                .Include(a => a.Absences)
                .Include(a => a.UserSkills).ThenInclude(us => us.Skill)
                .Include(a => a.UserSkills).ThenInclude(us => us.Level)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }


        public async Task SetMetaInfos(long userId, List<MetaInfoStringDto> MetaInfos)
        {
            foreach (var metaInfo in MetaInfos)
            {
                var meta = await _userMetaRepository.GetAll().FirstOrDefaultAsync(m => m.InfoName == metaInfo.UserMetaInfoName);
                var metaId = 
                    meta == null ? await _userMetaRepository.InsertAndGetIdAsync(new UserMetaInfo(metaInfo.UserMetaInfoName)) :
                    meta.Id;

                var userMetaInfoDetail = await _userMetaDetailRepository.GetAll().FirstOrDefaultAsync(us => us.UserId == userId && us.UserMetaInfoId == metaId);
                if (userMetaInfoDetail == null)
                {
                    if (metaInfo.InfoDetail != "")
                    {
                        var userSkillInsert = new UserMetaInfoDetail();
                        userSkillInsert.UserId = userId;
                        userSkillInsert.InfoDetail = metaInfo.InfoDetail;
                        userSkillInsert.UserMetaInfoId = metaId;
                        await _userMetaDetailRepository.InsertAsync(userSkillInsert);
                    }
                }
                else
                {
                    if (metaInfo.InfoDetail == "")
                    {
                        await _userMetaDetailRepository.DeleteAsync(userMetaInfoDetail);
                    }
                    else
                    {
                        userMetaInfoDetail.InfoDetail = metaInfo.InfoDetail;
                        await _userMetaDetailRepository.UpdateAsync(userMetaInfoDetail);
                    }
                }
            }
        }
        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to change password.");
            }
            long userId = _abpSession.UserId.Value;
            var user = await _userManager.GetUserByIdAsync(userId);
            var loginAsync = await _logInManager.LoginAsync(user.UserName, input.CurrentPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Existing Password' did not match the one on record.  Please try again or contact an administrator for assistance in resetting your password.");
            }
            //if (!new Regex(AccountAppService.PasswordRegex).IsMatch(input.NewPassword))
            //{
            //    throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
            //}
            user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
            CurrentUnitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to reset password.");
            }
            long currentUserId = _abpSession.UserId.Value;
            var currentUser = await _userManager.GetUserByIdAsync(currentUserId);
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }
            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }
            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                CurrentUnitOfWork.SaveChanges();
            }

            return true;
        }

    }
}

