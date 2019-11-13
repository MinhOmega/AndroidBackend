using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using SWHRMS.Controllers;
using SWHRMS.Skills;
using Abp.Runtime.Session;
using System.Threading.Tasks;
using Abp.UI;
using SWHRMS.Skills.Dto;
using System.Collections.Generic;
using SWHRMS.Web.Models.Skills;

namespace SWHRMS.Web.Controllers
{
    [AbpMvcAuthorize]
    public class SkillsController : SWHRMSControllerBase
    {
        private readonly ISkillAppService _skillAppService;
        private readonly IAbpSession _abpSession;

        public SkillsController(
            ISkillAppService SkillAppService,
            IAbpSession abpSession)
        {
            _skillAppService = SkillAppService;
            _abpSession = abpSession;
        }

        // GET: Skills
        public async Task<IActionResult> Index()
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException(L("PleaseLogin"));
            }
            var user = await _skillAppService.GetUser(_abpSession.UserId ?? default(long)); // Paging not implemented yet
            var skills = (await _skillAppService.GetAll(new PagedSkillResultRequestDto { MaxResultCount = int.MaxValue })).Items;
            var levels = (await _skillAppService.GetAllLevels()).Items;

            var userSkills = ObjectMapper.Map<SkillsUserListModel>(user);
            var model = new SkillsViewModel
            {
                User = userSkills,
                Skills = skills,
                Levels = levels
            };
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
