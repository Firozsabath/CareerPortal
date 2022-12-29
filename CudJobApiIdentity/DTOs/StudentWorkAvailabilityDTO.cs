using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.DTOs
{
    public class StudentWorkAvailabilityDTO
    {
        public int AvailabilityID { get; set; }
        public int StudentID { get; set; }        
        public int DaysPerWeekID { get; set; }        
        public int HoursPerWeekID { get; set; }
    }
}
