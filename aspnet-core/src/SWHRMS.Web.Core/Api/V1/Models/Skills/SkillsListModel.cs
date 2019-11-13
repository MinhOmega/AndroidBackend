using Abp.Authorization.Users;
using Abp.AutoMapper;
using SWHRMS.Branches;
using SWHRMS.Branches.Dto;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SWHRMS.Skills.Dto;
using SWHRMS.Levels.Dto;

namespace SWHRMS.Api.V1.Models.Skills
{
    public class SkillsListModel
    {
        public virtual List<SkillOutput> Skills { get; set; }

    }
}
