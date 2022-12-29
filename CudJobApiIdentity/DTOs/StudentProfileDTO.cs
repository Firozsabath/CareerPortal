using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.DTOs
{
    public class StudentProfileDTO
    {
        public int StudentID { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }       

        public DateTime DateOfBirth { get; set; }
        
        public string Gender { get; set; }
        
        public string MobileNumber { get; set; }
        
        public string EmailID { get; set; }
        
        public string CudStudentID { get; set; }

        public int ProgramID { get; set; }

        public int? HskillID { get; set; } 

        public int? SskillID { get; set; } 

        public int? CskillID { get; set; }

        public string ComputerSkills { get; set; }

        public string VisaStatus { get; set; }
       
        public string MaritalStatus { get; set; }

        public string Skillset { get; set; }

        public string Resumepath { get; set; }

        public string profileImgpath { get; set; }

        public string Objective { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }       

        //public ICollection<StudentResumes> StudentResumes { get; set; }
    }
}
