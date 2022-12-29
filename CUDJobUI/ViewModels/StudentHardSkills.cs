using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.ViewModels
{
    public class StudentHardSkills
    {
        [Key]
        public int StudentHsID { get; set; }
        public int StudentID { get; set; }
        public int HardSkillID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Hardskills Hardskills { get; set; }

    }

    public class StudentSoftSkills
    {
        [Key]
        public int StudentSsID { get; set; }
        public int StudentID { get; set; }
        public int SoftSkillID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Softskills Softskills { get; set; }
        
    }

    public class StudentComputerSkills
    {
        [Key]
        public int StudentCsID { get; set; }
        public int StudentID { get; set; }       
        public string ComputerSkill { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Computerskills Computerskills { get; set; }

    }

    public class SkillSetVM
    {
        public int StudentID { get; set; }
        public string SkillID { get; set; }
        public string SkillType { get; set; }
        public string studCmpSkill { get; set; }
    }
}
