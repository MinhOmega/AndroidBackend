using Abp.AutoMapper;
using Abp.Runtime.Validation;
using SWHRMS.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.Branches.Dto
{
    [AutoMapTo(typeof(Branch))]
    public class CreateBranchDto : IShouldNormalize
    {
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Location { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        //public virtual List<User> Users { get; set; }


        public void Normalize()
        {

        }
    }
}
