using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.Api.V1.Models.Skills
{
    public class CreateSkillModel
    {
        [Required]
        public string SkillName { get; set; }
    }
}
