using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class JobsWorkAvailability
    {
        [Key]
        public int AvailabilityID { get; set; }

        public int JobID { get; set; }

        public int DaysPerWeekID { get; set; }

        public int HoursPerWeekID { get; set; }

        [ForeignKey("JobID")]
        public virtual Jobs Jobs { get; set; }

        public DateTime? Createdate { get; set; }

        public DateTime? Updatedate { get; set; }

        [ForeignKey("DaysPerWeekID")]
        public virtual DaysPerWeekOptions DaysPerWeek { get; set; }

        [ForeignKey("HoursPerWeekID")]
        public virtual HoursPerWeekOptions HoursPerWeek { get; set; }
    }
}
