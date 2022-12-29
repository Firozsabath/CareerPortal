using CUDJobApiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Models
{
    public class StudentExperience
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int StudentID { get; set; }

        [MaxLength(100)]        
        [Column("Company_Name",TypeName = "Nvarchar(100)")]        
        public string CompanyName { get; set; }

        [Column("Country_Code")]
        public int CountryCode { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "Nvarchar(100)")]
        public string Location { get; set; }

        [MaxLength(100)]
        [Column("Job_Title", TypeName = "Nvarchar(100)")]
        public string JobTitle { get; set; }

        [MaxLength(5000)]
        [Column("Job_Description", TypeName = "Nvarchar(5000)")]
        public string JobDescription { get; set; }

        [MaxLength(200)]
        [Column("Company_website", TypeName = "Nvarchar(200)")]
        public string Companywebsite { get; set; }

        [MaxLength(100)]
        [Column("Department", TypeName = "Nvarchar(100)")]
        public string Department { get; set; }

        public int? CategoryID { get; set; }

        [Column("Start_Month")]       
        public DateTime? StartDate { get; set; }

        [Column("End_Date")]        
        public DateTime? EndDate { get; set; }
        
        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool current { get; set; }

        [ForeignKey("StudentID")]
        public virtual StudentSeekers StudentSeeker { get; set; }

        [ForeignKey("CategoryID")]
        public virtual CompanyCategory companycategory { get; set; }
    }

    public class VolunteerExperience
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VexpId { get; set; }
        public int StudentID { get; set; }

        [MaxLength(100)]
        [Column("Company_Name", TypeName = "Nvarchar(100)")]
        public string CompanyName { get; set; }

        [MaxLength(100)]
        [Column("Job_Title", TypeName = "Nvarchar(100)")]
        public string JobTitle { get; set; }

        [MaxLength(5000)]
        [Column("Job_Description", TypeName = "Nvarchar(4000)")]
        public string JobDescription { get; set; }

        public bool? current { get; set; }

        [Column("Start_Month")]
        public DateTime? StartDate { get; set; }

        [Column("End_Date")]
        public DateTime? EndDate { get; set; }

        public DateTime? Createdate { get; set; }

        public DateTime? Updatedate { get; set; }

        [ForeignKey("StudentID")]
        public StudentSeekers StudentSeeker { get; set; }
    }
}
