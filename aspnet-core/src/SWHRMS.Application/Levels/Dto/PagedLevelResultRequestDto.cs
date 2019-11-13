using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.Levels.Dto
{
   
    public class PagedLevelResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool IsActive { get; set; }
    }
}
