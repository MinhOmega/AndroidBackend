using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SWHRMS.Authorization.Users;
using SWHRMS.Positions;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.Positions.Dto
{
    [AutoMapFrom(typeof(Position))]
    public class PositionDto : EntityDto
    {

        [StringLength(250)]
        public string PositionName { get; set; }
        public string Description { get; set; }

        public DateTime CreationTime { get; set; }
        public virtual List<UserInPosition> Users { get; set; }

        public class UserInPosition
        {
            public long Id { get; set; }
            public string UserName { get; set; }
        }
    }
}
