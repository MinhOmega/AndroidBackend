using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SWHRMS.Authorization.Users;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.Branches.Dto
{
    [AutoMapFrom(typeof(Branch))]
    public class BranchDto : EntityDto
    {
       
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Location { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }


        public DateTime CreationTime { get; set; }

        public virtual List<UserInBranch> Users { get; set; }

        public class UserInBranch
        {
            public long Id { get; set; }
            public string UserName { get; set; }
        }
    }
}
