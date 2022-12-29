using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class StudentWorkAvailability
    {
        [Key]
        public int AvailabilityID { get; set; }

        public int StudentID { get; set; }

        public int DaysPerWeekID { get; set; }

        public int HoursPerWeekID { get; set; }

        public DateTime? Createdate { get; set; }

        public DateTime? Updatedate { get; set; }

        [ForeignKey("StudentID")]
        public virtual StudentSeekers StudentSeeker { get; set; }

        [ForeignKey("DaysPerWeekID")]
        public virtual DaysPerWeekOptions DaysPerWeekOptions { get; set; } 

        [ForeignKey("HoursPerWeekID")]
        public virtual HoursPerWeekOptions HoursPerWeekOptions { get; set; }
    }

    public class DaysPerWeekOptions
    {
        [Key]
        public int DaysPerWeekID { get; set; }
        public string Options { get; set; }
    }

    public class HoursPerWeekOptions
    {
        [Key]
        public int HoursPerWeekID { get; set; }
        public string Options { get; set; }
    }
}
