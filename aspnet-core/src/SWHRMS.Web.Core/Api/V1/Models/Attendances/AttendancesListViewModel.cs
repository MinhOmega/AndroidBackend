using SWHRMS.Attendances.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.Api.V1.Models.Attendances
{
    public class AttendancesListViewModel
    {
        public IReadOnlyList<AttendanceDto> Attendances { get; set; }

    }
}
