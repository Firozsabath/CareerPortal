using CUDJobApiIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.DTOs
{
    public class StudentSoftSkillsDTO
    {
        public int StudentSsID { get; set; }
        public int StudentID { get; set; }
        public int SoftSkillID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Softskills Softskills { get; set; }
    }

    public class StudentHardskillsDTO
    {
        public int StudentHsID { get; set; }
        public int StudentID { get; set; }
        public int HardSkillID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Hardskills Hardskills { get; set; }
      
    }

    public class StudentComputerSkillsDTO
    {
        public int StudentCsID { get; set; }
        public int StudentID { get; set; }        
        public string ComputerSkill { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Computerskills Computerskills { get; set; }
    }
}
