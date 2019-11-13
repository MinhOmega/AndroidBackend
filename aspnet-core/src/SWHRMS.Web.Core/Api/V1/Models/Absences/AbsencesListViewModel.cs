using SWHRMS.Absences.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWHRMS.Api.V1.Models.Absences
{
    public class AbsencesListViewModel
    {
        public IReadOnlyList<AbsenceDto> Absences { get; set; }

    }
}
