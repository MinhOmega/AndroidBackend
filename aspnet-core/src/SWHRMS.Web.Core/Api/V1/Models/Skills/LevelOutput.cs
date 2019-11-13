using Abp.AutoMapper;
using SWHRMS.Levels.Dto;

namespace SWHRMS.Api.V1.Models.Skills
{
    [AutoMapFrom(typeof(LevelDto))]
    public class LevelOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}