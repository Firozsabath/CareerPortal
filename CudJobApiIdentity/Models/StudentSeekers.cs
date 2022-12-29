using CUDJobApiIdentity.Models;
using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Models
{
    public class StudentSeekers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int StudentID { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "Nvarchar(100)")]
        public string FirstName { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "Nvarchar(100)")]
        public string LastName { get; set; }
        
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "Nvarchar(10)")]
        public string Gender { get; set; }

        [MaxLength(30)]
        [Column(TypeName = "Nvarchar(30)")]
        public string MobileNumber { get; set; }

        [MaxLength(30)]
        [Column(TypeName = "Nvarchar(30)")]
        public string MobileNumber2 { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "Nvarchar(50)")]
        public string EmailID { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "Nvarchar(50)")]
        public string EmailID2 { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "Nvarchar(50)")]
        public string VisaStatus { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "Nvarchar(50)")]
        public string Porgram { get; set; }

        [MaxLength(5)]
        [Column(TypeName = "Nvarchar(5)")]
        public string Intern { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "Nvarchar(10)")]
        public string Credihours { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "Nvarchar(50)")]
        public string MaritalStatus { get; set; }

        [MaxLength(1000)]
        [Column(TypeName = "Nvarchar(1000)")]
        public string Skillset { get; set; }


        [Column(TypeName = "Nvarchar(50)")]
        public string Password { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "Nvarchar(50)")]
        public string CudStudentID { get; set; }

        public int ProgramID { get; set; }

        public int? HskillID { get; set; }

        public int? SskillID { get; set; }

        public int? CskillID { get; set; }

        //public string ComputerSkills { get; set; }

        //[Column(TypeName = "Nvarchar(50)")]
        //public string Cud_EmailID { get; set; }

        [MaxLength(150)]
        [Column(TypeName = "Nvarchar(150)")]
        public string Resumepath { get; set; }

        [MaxLength(150)]
        [Column(TypeName = "Nvarchar(150)")]
        public string profileImgpath { get; set; }

        [MaxLength(300)]
        [Column(TypeName = "Nvarchar(300)")]
        public string Objective { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("ProgramID")]
        public programs programs { get; set; }

        //[ForeignKey("HskillID")]
        //public Hardskills Hardskills { get; set; }

        //[ForeignKey("SskillID")]
        //public Softskills Softskills { get; set; }

        //[ForeignKey("CskillID")]
        //public Computerskills Computerskill { get; set; }

        public ICollection<StudentExperience> StudentExperiences { get; set; } = new List<StudentExperience>();

        public ICollection<VolunteerExperience> VolunteerExperiences { get; set; } = new List<VolunteerExperience>();

        public ICollection<StudentEducation> StudentEducations { get; set; } = new List<StudentEducation>();

        public ICollection<StudentAddressCollection> StudentAddresses { get; set; } = new List<StudentAddressCollection>();

        public ICollection<AppliedJobs> AppliedJobs { get; } = new List<AppliedJobs>();

        public ICollection<Projects> Projects { get; } = new List<Projects>();

        public ICollection<Memberships> Memberships { get; } = new List<Memberships>();

        public ICollection<Awards> Award { get; } = new List<Awards>();

        public ICollection<Languages> Languages { get; set; } = new List<Languages>();

        public ICollection<Studentportfolio> Studentportfolio { get; set; } = new List<Studentportfolio>();

        public ICollection<StudentWorkAvailability> StudentWorkAvailability { get; set; } = new List<StudentWorkAvailability>();

        public ICollection<StudentHardSkills> StudentHardSkills { get; set; } = new List<StudentHardSkills>();

        public ICollection<StudentSoftSkills> StudentSoftSkills { get; set; } = new List<StudentSoftSkills>();

        public ICollection<StudentComputerSkills> StudentComputerSkills { get; set; } = new List<StudentComputerSkills>();


        //public ICollection<StudentResumes> StudentResumes { get; } = new List<StudentResumes>();
    }
}
