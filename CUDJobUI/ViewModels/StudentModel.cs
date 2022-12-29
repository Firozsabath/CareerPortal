using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.ViewModels
{
    public class StudentModel
    {
        public int StudentID { get; set; }
        
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        
        [Display(Name = "Date of birth")]
        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; }

        
        [Display(Name ="Phone Number")]
        public string MobileNumber { get; set; }

       
        [Display(Name = "Email ID")]
        public string EmailID { get; set; }

        [Display(Name = "Secondary Mobile Number")]
        public string MobileNumber2 { get; set; }

        [Display(Name = "Secondary Email ID")]
        public string EmailID2 { get; set; }

        [Display(Name = "Programs")]
        public int ProgramID { get; set; }

        [Display(Name = "Hard Skill")]
        public int? HskillID { get; set; }


        [Display(Name = "Soft Skill")]
        public int? SskillID { get; set; }


        [Display(Name = "Computer Skill")]
        public int? CskillID { get; set; }

        //[Display(Name = "Computer Skill")]
        //public string ComputerSkills { get; set; }

        public string CudStudentID { get; set; }

        public string VisaStatus { get; set; }

        public string MaritalStatus { get; set; }

        public string Resumepath { get; set; }

        public string profileImgpath { get; set; }

        [Display(Name = "profile summary")]
        public string Objective { get; set; }

        [Display(Name = "SKill Set")]
        public string Skillset { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }

    
}
