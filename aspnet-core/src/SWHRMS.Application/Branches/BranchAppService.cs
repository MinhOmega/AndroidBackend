using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using SWHRMS.Authorization;
using SWHRMS.Branches.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.Branches
{

    //[AbpAuthorize(PermissionNames.Pages_Branches)]
    public class BranchAppService : AsyncCrudAppService<Branch, BranchDto, int, PagedBranchResultRequestDto, CreateBranchDto, BranchDto>, IBranchAppService
    {
        //private readonly IRepository<Branch> _branchRepository;

        public BranchAppService(
            IRepository<Branch> repository)
            : base(repository)
        {

        }
    }
}
