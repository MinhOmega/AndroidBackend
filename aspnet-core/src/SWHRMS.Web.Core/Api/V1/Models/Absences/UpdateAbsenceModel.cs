using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SWHRMS.Api.V1.Models.Absences
{
    public class UpdateAbsenceModel
    {
        /// <summary>
        /// Id of absent user.
        /// </summary>
        [Required]
        public long UserId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Approval status.
        /// 0 - Freshly created. 1 - Approved. 2 - Not approved.
        /// </summary>
        [Required]
        public int Status { get; set; }
        /// <summary>
        /// Reason for absence.
        /// </summary>
        public string Details { get; set; }
    }
}
