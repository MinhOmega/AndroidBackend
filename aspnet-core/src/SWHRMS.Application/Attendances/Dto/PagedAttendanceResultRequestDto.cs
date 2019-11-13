using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.Attendances.Dto
{
   
    public class PagedAttendanceResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool IsActive { get; set; }
    }
}
