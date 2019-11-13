using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.Branches.Dto
{
   
    public class PagedBranchResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool IsActive { get; set; }
    }
}
