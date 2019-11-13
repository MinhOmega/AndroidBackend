using Abp.AutoMapper;
using SWHRMS.Skills.Dto;
using System;

namespace SWHRMS.Api.V1.Models.Skills
{
    [AutoMapFrom(typeof(SkillDto))]
    public class SkillOutput
    {
        public long Id { get; set; }
        public string SkillName { get; set; }
        public DateTime CreationTime { get; set; }
    }
}