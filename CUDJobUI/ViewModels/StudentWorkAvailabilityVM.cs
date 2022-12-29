using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.ViewModels
{
    public class StudentWorkAvailabilityVM
    {
        public int AvailabilityID { get; set; }

        public int StudentID { get; set; }
        [Display(Name ="Days Per Week")]
        public int DaysPerWeekID { get; set; }
        [Display(Name = "Hours Per Day")]
        public int HoursPerWeekID { get; set; }           
        public virtual DaysPerWeekOptions DaysPerWeekOptions { get; set; }
        public virtual HoursPerWeekOptions HoursPerWeekOptions { get; set; }
    }

    public class DaysPerWeekOptions
    {
        
        public string DaysPerWeekID { get; set; }
        public string Options { get; set; }
    }

    public class HoursPerWeekOptions
    {
        
        public string HoursPerWeekID { get; set; }
        public string Options { get; set; }
    }
}
