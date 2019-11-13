using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.Api.V1.Models.Attendances
{
    public class PunchInViewModel
    {
            [Required]
            public string QRCode { get; set; }
            //[Required]
            //public int BranchId { get; set; }
            //[Required]
            //public long UserId { get; set; }
    }
}
