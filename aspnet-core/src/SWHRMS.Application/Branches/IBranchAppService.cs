using Abp.Application.Services;
using SWHRMS.Branches.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.Branches
{
    public interface IBranchAppService : IAsyncCrudAppService<BranchDto, int, PagedBranchResultRequestDto, CreateBranchDto, BranchDto>
    {
    }
}
