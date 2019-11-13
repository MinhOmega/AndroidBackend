using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Http;
using SWHRMS.Skills;
using SWHRMS.Skills.Dto;
using SWHRMS.Api.V1.Models.Skills;
using SWHRMS.Authorization;
using SWHRMS.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SWHRMS.Users.Dto;
using SWHRMS.Levels;

namespace SWHRMS.Api.V1.Controllers
{
    [ApiVersion("1")]
    [Route("api/v1/[controller]")]
    public class SkillsController: SWHRMSControllerBase
    {
        private readonly ISkillApiService _skillsApiService;
        private readonly ILevelApiService _levelsApiService;
        private readonly IAbpSession _abpSession;

        public SkillsController(
            ISkillApiService SkillApiService,
            ILevelApiService levelApiService,
            IAbpSession abpSession)
        {
            _skillsApiService = SkillApiService;
            _levelsApiService = levelApiService;
            _abpSession = abpSession;
        }

        /// <summary>
        /// Get all skills.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        [Route("")]
        public async Task<SkillsListModel> Index()
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException(L("PleaseLogin"));
            }
            var skills = (await _skillsApiService.GetAll()).Items;
            return new SkillsListModel
            {
                Skills = ObjectMapper.Map<List<SkillOutput>>(skills)
            };
        }

        /// <summary>
        /// Get all skill levels.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        [Route("[action]")]
        public async Task<LevelsListModel> Levels()
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException(L("PleaseLogin"));
            }
            var levels = (await _levelsApiService.GetAll()).Items;
            return new LevelsListModel
            {
                Levels = ObjectMapper.Map<List<LevelOutput>>(levels)
            };
        }

        /// <summary>
        /// Get all skills belonging to currently logged-in user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        [Route("User")]
        public async Task<SkillsUserListModel> UserSkills()
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException(L("PleaseLogin"));
            }
            var user = await _skillsApiService.GetUser(_abpSession.UserId ?? default(long)); // Paging not implemented yet

            var userSkills = ObjectMapper.Map<SkillsUserListModel>(user);
            if (userSkills.UserSkills == null || userSkills.UserSkills.Count == 0)
                throw new UserFriendlyException(L("NoSkillFound"));
            return userSkills;
        }

        /// <summary>
        /// Update/Insert/Update skills for currently logged-in user.
        /// Input is an array of {string  levelId, string skillId}.
        /// To delete: levelId = "", skill Id = Id for one of user's current skills.
        /// To update/Insert: levelId != Any level Id from Levels table, skillId = Any skill Id from Skills table.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        [Route("User/Set")]
        public async Task<List<UserSkillOutput>> SetUserSkills([FromBody] SetUserSkillsModel model)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException(L("PleaseLogin"));
            }
            var setUserSkillsModel = new UserSkillUpdateDto();
            setUserSkillsModel.UserSkills = model.UserSkills;
            setUserSkillsModel.Id = _abpSession.UserId ?? default(long);

            return ObjectMapper.Map<List<UserSkillOutput>>((await _skillsApiService.UpdateUserSkills(setUserSkillsModel)).Items);
            //return Json(new { Msg = L("CreateSkillSuccess") });
        }

        //[HttpPost]
        //[AbpAuthorize]
        //[IgnoreAntiforgeryToken]
        //public async Task<SkillDto> CreateSkill([FromBody] CreateSkillModel model)
        //{
        //    var skillDto = new CreateSkillDto();
        //    skillDto.SkillName = model.SkillName;
        //    return (await _skillsApiService.Create(skillDto)); // Paging not implemented yet

        //    //return Json(new { Msg = L("CreateSkillSuccess") });
        //}

        //[HttpPost]
        //[AbpAuthorize]
        //[IgnoreAntiforgeryToken]
        //public async Task<SkillDto> UpdateSkill([FromBody] UpdateSkillModel model)
        //{
        //    var skillDto = new SkillDto();
        //    skillDto.Id = model.Id;
        //    skillDto.SkillName = model.SkillName;
        //    return (await _skillsApiService.Update(skillDto)); // Paging not implemented yet

        //    //return Json(new { Msg = L("UpdateSkillSuccess") });
        //}

        //[HttpPost]
        //[AbpAuthorize]
        //[IgnoreAntiforgeryToken]
        //public async Task<JsonResult> DeleteSkill([FromBody] long skillId)
        //{
        //    var skillDto = new EntityDto<long>(skillId);
        //    skillDto = await _skillsApiService.Delete(skillDto); // Paging not implemented yet

        //    if (skillDto == null)
        //        throw new UserFriendlyException(L("NoSkillFound"));

        //    return Json(new { Msg = L("DeleteSkillSuccess") });
        //}
    }
}
