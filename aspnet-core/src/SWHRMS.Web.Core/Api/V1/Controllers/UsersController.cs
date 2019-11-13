using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Http;
using SWHRMS.Api.V1.Models.Users;
using SWHRMS.Authorization;
using SWHRMS.Controllers;
using SWHRMS.Users;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.Api.V1.Controllers
{
    [ApiVersion("1")]
    [Route("api/v1/[controller]")]
    public class UsersController: SWHRMSControllerBase
    {
        private readonly IUserApiService _userAppService;
        private readonly IAbpSession _abpSession;

        public UsersController(
            IUserApiService userAppService,
            IAbpSession abpSession
            )
        {
            _userAppService = userAppService;
            _abpSession = abpSession;
        }

        [HttpGet]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        [Route("")]
        public async Task<UserListViewModel> Index()
        {
            var users = (await _userAppService.GetAll()).Items; // Paging not implemented yet
            var roles = (await _userAppService.GetRoles()).Items;
            return new UserListViewModel
            {
                Users = users,
                Roles = roles
            };
        }

        [HttpPost]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        [Route("[action]")]
        public async Task<UserDto> Create([FromBody] CreateUserModel input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException(L("PleaseLogin"));
            }
            var createUserDto = ObjectMapper.Map<CreateUserDto>(input);
            return (await _userAppService.Create(createUserDto)); // Paging not implemented yet

        }

        [HttpPut]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        [Route("{userId:long}/[action]")]
        public async Task<UserDto> Update([FromRoute] long userId,[FromBody] UpdateUserModel input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException(L("PleaseLogin"));
            }
            var userDto = ObjectMapper.Map<UserDto>(input);
            userDto.Id = userId;
            return (await _userAppService.Update(userDto)); // Paging not implemented yet

        }

        [HttpDelete]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        [Route("{userId:long}/[action]")]
        public async Task<JsonResult> Delete([FromRoute] long userId)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException(L("PleaseLogin"));
            }
            var userDto = new EntityDto<long>(userId);
            userDto = (await _userAppService.Delete(userDto)); // Paging not implemented yet
            if (userDto == null)
                throw new UserFriendlyException(L("NoAbsenceFound"));

            return Json(new { Msg = L("DeleteAbsenceSuccess") });
        }
    }
}
