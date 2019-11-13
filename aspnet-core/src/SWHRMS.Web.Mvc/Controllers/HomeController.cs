using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using SWHRMS.Controllers;
using SWHRMS.Users;
using Abp.Application.Services.Dto;
using Abp.Runtime.Session;
using SWHRMS.Web.Models.Home;
using System.Threading.Tasks;
using SWHRMS.Skills;
using SWHRMS.Web.Models.Skills;
using SWHRMS.Skills.Dto;

namespace SWHRMS.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : SWHRMSControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly ISkillAppService _skillAppService;
        private readonly IAbpSession _abpSession;

        public HomeController(
            IUserAppService userAppService,
            ISkillAppService skillAppService,
            IAbpSession abpSession)
        {
            _userAppService = userAppService;
            _skillAppService = skillAppService;
            _abpSession = abpSession;
        }
        public async Task<ActionResult> Index()
        {
            var user = await _userAppService.Get(new EntityDto<long>(_abpSession.UserId ?? default(long)));
            var roles = (await _userAppService.GetRoles()).Items;
            var branches = (await _userAppService.GetBranches()).Items;
            var positions = (await _userAppService.GetPositions()).Items;
            var metaInfos = (await _userAppService.GetMetaInfos()).Items;



            var model = new HomeViewModel
            {
                User = user,
                Branches = branches,
                Positions = positions,
                Roles = roles,
                UserMetaInfos = metaInfos
            };
            //return View("_EditUserModal", model);
            return View(model);
        }

        public async Task<ActionResult> EditUserSkillModal(long userId)
        {
            var user = await _skillAppService.GetUser(userId);
            var userSkill = ObjectMapper.Map<SkillsUserListModel>(user);
            var skills = (await _skillAppService.GetAll(new PagedSkillResultRequestDto { MaxResultCount = int.MaxValue })).Items;
            var levels = (await _skillAppService.GetAllLevels()).Items;
            var model = new EditUserSkillModalViewModel
            {
                User = userSkill,
                Skills = skills,
                Levels = levels
            };
            return View("_EditUserSkillModal", model);
        }
    }
}
