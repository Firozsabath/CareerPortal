using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.ViewModels
{
    public class StudentDetails_Cams
    {
        public Student_Cams studentdetails { get; set; }

        public StudentEduCams studentEducation { get; set; }

        public StudentAddress address { get; set; }
    }

    public class Student_Cams
    {
        public string CudStudentID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string MobileNumber { get; set; }

        public string EmailID { get; set; }
    }

    public class StudentAddress
    {
        public string City { get; set; }

        public string State { get; set; }

        public int CountryID { get; set; }

        public int PinCode { get; set; }
    }

    public class StudentEduCams
    {
        public string Institution { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public Byte? CompletionPercent { get; set; }
    }
}
