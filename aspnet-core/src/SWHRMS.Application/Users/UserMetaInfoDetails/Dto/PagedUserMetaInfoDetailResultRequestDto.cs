using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.UserMetaInfoDetails.Dto
{
   
    public class PagedUserMetaInfoDetailResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool IsActive { get; set; }
    }
}
