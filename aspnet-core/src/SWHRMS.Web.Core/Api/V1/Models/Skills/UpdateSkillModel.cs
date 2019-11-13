using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.Api.V1.Models.Skills
{
    public class UpdateSkillModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string SkillName { get; set; }
    }
}
