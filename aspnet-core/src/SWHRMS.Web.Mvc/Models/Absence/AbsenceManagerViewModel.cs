using SWHRMS.Absences.Dto;
using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWHRMS.Web.Models.Absence
{
    public class AbsenceManagerViewModel
    {
        public IReadOnlyList<AbsenceUserListModel> Users { get; set; }
        public IReadOnlyList<AbsenceDto> Absences { get; set; }
    }
}
