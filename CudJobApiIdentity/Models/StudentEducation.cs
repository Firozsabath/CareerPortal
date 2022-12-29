using CUDJobApiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Models
{
    public class StudentEducation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EducationId { get; set; }
        public int StudentID { get; set; }

        [Column(TypeName = "Nvarchar(100)")]
        public string Institution { get; set; }

        public string Major { get; set; }
        [Column(TypeName = "Nvarchar(100)")]
        public string Degree { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CurrrentSalary { get; set; }

        [MaxLength(100)]
        [Column("Certificate_Diploma")]
        public string CertificateDiploma { get; set; }

        [Column("Start_Date")]
        public DateTime? StartDate { get; set; }
        [Column("Completion_Date")]
        public DateTime? CompletionDate { get; set; }
        [Column("Completion_Percent")]
        public Byte? CompletionPercent { get; set; }

        public DateTime? CreateddDate { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool? current { get; set; }

        //public ICollection<EducationDegree> EducationDegree { get; set; } = new List<EducationDegree>();

        [ForeignKey("StudentID")]
        public virtual StudentSeekers StudentSeeker { get; set; }
    }
}
