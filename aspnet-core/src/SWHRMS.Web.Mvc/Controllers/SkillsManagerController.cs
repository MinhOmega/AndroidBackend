using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using SWHRMS.Controllers;
using Abp.Runtime.Session;
using SWHRMS.Skills;
using Abp.UI;
using System.Threading.Tasks;
using System.Collections.Generic;
using SWHRMS.Web.Models.Skills;
using SWHRMS.Skills.Dto;
using SWHRMS.Web.Models.Common;
using System.Linq;
using Abp.Application.Services.Dto;

namespace SWHRMS.Web.Controllers
{
    [AbpMvcAuthorize]
    public class SkillsManagerController : SWHRMSControllerBase
    {
        private readonly ISkillAppService _skillAppService;
        private readonly IAbpSession _abpSession;

        public SkillsManagerController(
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
            var users = (await _skillAppService.GetAllUsers()).Items; // Paging not implemented yet
            var skills = (await _skillAppService.GetAll(new PagedSkillResultRequestDto { MaxResultCount = int.MaxValue })).Items;
            var levels = (await _skillAppService.GetAllLevels()).Items;

            var userSkills = ObjectMapper.Map<List<SkillsUserListModel>>(users);
            var model = new SkillsManagerViewModel
            {
                Users = userSkills,
                Skills = skills,
                Levels = levels
            };
            return View(model);
        }
        // GET: Donut Chart
        public async Task<MorrisChartResult> DonutChart(long absenceId, int status)
        {
            var datas = new List<MorrisDonutChartDataModel>();
            var colorCodes = new List<string>();
            var skills = (await _skillAppService.GetAll(new PagedSkillResultRequestDto { MaxResultCount = int.MaxValue })).Items;
            foreach(var skill in skills)
            {
                var data = new MorrisDonutChartDataModel(skill.SkillName,skill.UserSkills.Select(us => us.UserId).Distinct().Count());
                colorCodes.Add(skill.ColorCode);
                datas.Add(data);
            }
            return new MorrisChartResult(datas,colorCodes);
        }

        public async Task<ActionResult> EditSkillModal(long skillId)
        {
            var skill = await _skillAppService.Get(new EntityDto<long>(skillId));
            var model = new EditSkillModalViewModel
            {
                Skill = skill
            };
            return View("_EditSkillModal", model);
        }

        public ActionResult CreateSkillModal()
        {
            var model = new EditSkillModalViewModel();
            return View("_CreateSkillModal", model);
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
