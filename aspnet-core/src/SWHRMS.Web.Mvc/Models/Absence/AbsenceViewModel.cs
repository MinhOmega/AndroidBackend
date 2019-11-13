using SWHRMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWHRMS.Web.Models.Absence
{
    public class AbsenceViewModel
    {
        public UserDto CurrentUser { get; set; }
        public IReadOnlyList<AbsenceUserListModel> Users { get; set; }
        public IReadOnlyList<DateTime> AbsenceDates { get; set; }
    }
}
