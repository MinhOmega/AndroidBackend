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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SWHRMS.Authorization;
using SWHRMS.Authorization.Accounts;
using SWHRMS.Authorization.Roles;
using SWHRMS.Authorization.Users;
using SWHRMS.Authorization.Users.Meta_Info;
using SWHRMS.Branches;
using SWHRMS.Branches.Dto;
using SWHRMS.Positions;
using SWHRMS.Positions.Dto;
using SWHRMS.Roles.Dto;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SWHRMS.Users
{
    public class UserApiService : ApplicationService, IUserApiService //ApplicationService
    {
        private readonly IRepository<User, long> _userRepository;

        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IRepository<Position> _positionRepository;
        private readonly IRepository<UserMetaInfoDetail> _userMetaDetailRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;

        public UserApiService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IRepository<Branch> branchRepository,
            IRepository<Position> positionRepository,
            IRepository<UserMetaInfoDetail> userMetaDetailRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager)
        {
            _userRepository = repository;
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _branchRepository = branchRepository;
            _positionRepository = positionRepository;
            _userMetaDetailRepository = userMetaDetailRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
        }

        public async Task<User> FindByPhoneNumber(string phone)
        {
            var user = await _userRepository.FirstOrDefaultAsync(x => x.PhoneNumber == phone);

            if (user == null)
            {
                return null;
            }
            return user;
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAll()
                .Include(a => a.UserMetaInfoDetails).ThenInclude(c => c.UserMetaInfo)
                .Include(a => a.Roles)
                .Include(a => a.Branch)
                .Include(a => a.Position).ToListAsync();
            return new ListResultDto<UserDto>(ObjectMapper.Map<List<UserDto>>(users));
        }

        /// <summary>
        /// Get First User by Id.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<UserDto> Get(long UserId)
        {
            var user = await _userRepository.GetAll()
                .Include(a => a.UserMetaInfoDetails).ThenInclude(c => c.UserMetaInfo)
                .Include(a => a.Roles)
                .Include(a => a.Branch)
                .Include(a => a.Position)
                .FirstOrDefaultAsync(x => x.Id == UserId);

            if (user == null)
            {
                return null;
            }

            return ObjectMapper.Map<UserDto>(user);
        }

        public async Task<UserDto> Create(CreateUserDto input)
        {
            //CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        public async Task<UserDto> Update(UserDto input)
        {
            //CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            //return await Get(input);
            return ObjectMapper.Map<UserDto>(user); 
        }

        public async Task<UserDto> Delete(EntityDto<long> input)
        {

            var user = new User();
            try
            {
                user = await _userManager.GetUserByIdAsync(input.Id);
            }
            catch
            {
                return null;
            }
            await _userManager.DeleteAsync(user);
            return ObjectMapper.Map<UserDto>(user);
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

        protected User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected UserDto MapToEntityDto(User user)
        {
            var roles = _roleManager.Roles.Where(r => user.Roles.Any(ur => ur.RoleId == r.Id)).Select(r => r.NormalizedName);
            //var branch = _branchRepository.GetAllList().FirstOrDefault(b => user.BranchID == b.Id);
            //var userDto = base.MapToEntityDto(user);
            var userDto = ObjectMapper.Map<UserDto>(user);
            userDto.RoleNames = roles.ToArray();
            //userDto.BranchName = (branch!= null? branch.Name: "");
            return userDto;
        }

        protected IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
        {
            return _userRepository.GetAllIncluding(x => x.Roles, x => x.Branch)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await _userRepository.GetAllIncluding(x => x.Roles, x => x.Branch).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        protected IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        //public async Task<bool> ChangePassword(ChangePasswordDto input)
        //{
        //    if (_abpSession.UserId == null)
        //    {
        //        throw new UserFriendlyException("Please log in before attemping to change password.");
        //    }
        //    long userId = _abpSession.UserId.Value;
        //    var user = await _userManager.GetUserByIdAsync(userId);
        //    var loginAsync = await _logInManager.LoginAsync(user.UserName, input.CurrentPassword, shouldLockout: false);
        //    if (loginAsync.Result != AbpLoginResultType.Success)
        //    {
        //        throw new UserFriendlyException("Your 'Existing Password' did not match the one on record.  Please try again or contact an administrator for assistance in resetting your password.");
        //    }
        //    if (!new Regex(AccountAppService.PasswordRegex).IsMatch(input.NewPassword))
        //    {
        //        throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
        //    }
        //    user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
        //    CurrentUnitOfWork.SaveChanges();
        //    return true;
        //}

        //public async Task<bool> ResetPassword(ResetPasswordDto input)
        //{
        //    if (_abpSession.UserId == null)
        //    {
        //        throw new UserFriendlyException("Please log in before attemping to reset password.");
        //    }
        //    long currentUserId = _abpSession.UserId.Value;
        //    var currentUser = await _userManager.GetUserByIdAsync(currentUserId);
        //    var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
        //    if (loginAsync.Result != AbpLoginResultType.Success)
        //    {
        //        throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
        //    }
        //    if (currentUser.IsDeleted || !currentUser.IsActive)
        //    {
        //        return false;
        //    }
        //    var roles = await _userManager.GetRolesAsync(currentUser);
        //    if (!roles.Contains(StaticRoleNames.Tenants.Admin))
        //    {
        //        throw new UserFriendlyException("Only administrators may reset passwords.");
        //    }

        //    var user = await _userManager.GetUserByIdAsync(input.UserId);
        //    if (user != null)
        //    {
        //        user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
        //        CurrentUnitOfWork.SaveChanges();
        //    }

        //    return true;
        //}

    }
}
