using Abp.AutoMapper;
using Abp.Runtime.Validation;
using SWHRMS.Authorization.Users;
using SWHRMS.Positions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.Positions.Dto
{
    [AutoMapTo(typeof(Position))]
    public class CreatePositionDto : IShouldNormalize
    {
        [StringLength(250)]
        public string PositionName { get; set; }
        public string Description { get; set; }

        public void Normalize()
        {

        }
    }
}
