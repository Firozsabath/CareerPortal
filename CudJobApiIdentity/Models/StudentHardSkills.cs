using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class StudentHardSkills
    {
        [Key]
        public int StudentHsID { get; set; }
        public int StudentID { get; set; }
        public int HardSkillID { get; set; }
        public DateTime? CreatedDate { get; set; }

        [ForeignKey("StudentID")]
        public virtual StudentSeekers StudentSeeker { get; set; }

        [ForeignKey("HardSkillID")]
        public Hardskills Hardskills { get; set; }
    }

    public class StudentSoftSkills
    {
        [Key]
        public int StudentSsID { get; set; }
        public int StudentID { get; set; }
        public int SoftSkillID { get; set; }
        public DateTime? CreatedDate { get; set; }

        [ForeignKey("StudentID")]
        public virtual StudentSeekers StudentSeeker { get; set; }

        [ForeignKey("SoftSkillID")]
        public Softskills Softskills { get; set; }
    }

    public class StudentComputerSkills
    {
        [Key]
        public int StudentCsID { get; set; }
        public int StudentID { get; set; }
        //public int? ComputerSkillID { get; set; }
        public string ComputerSkill { get; set; }
        public DateTime? CreatedDate { get; set; }

        [ForeignKey("StudentID")]
        public virtual StudentSeekers StudentSeeker { get; set; }

        //[ForeignKey("ComputerSkillID")]
        //public Computerskills Computerskills { get; set; }
    }
}
