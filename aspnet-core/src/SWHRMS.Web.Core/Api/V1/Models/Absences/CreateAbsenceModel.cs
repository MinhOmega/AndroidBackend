using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.Api.V1.Models.Absences
{
    public class CreateAbsenceModel
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        //[Required]
        //public int Status { get; set; }
        public string Details { get; set; }
    }
}
