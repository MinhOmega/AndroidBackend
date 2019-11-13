using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.QRCodes.Dto
{
    [AutoMapTo(typeof(QRCode))]
    public class CreateQRCodeDto
    {
        [Required]
        public string CodeString { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
