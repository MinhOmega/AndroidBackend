using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.Positions.Dto
{
   
    public class PagedPositionResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool IsActive { get; set; }
    }
}
