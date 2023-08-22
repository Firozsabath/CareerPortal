using CUDJobApiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Models
{
    public class Jobs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  Id { get; set; }
        
        [MaxLength(100)]
        public string Title { get; set; }

        public int JobCategoryId { get; set; }

        [MaxLength(5000)]
        public string  ReguiredSkills { get; set; }

        [MaxLength(5000)]
        public string Qualification { get; set; }

        [MaxLength(5000)]
        public string Experience { get; set; }

        public int ExperienceID { get; set; }

        [MaxLength(100)]
        public string  ExpectedSalary { get; set; }
        
        public string  Shortheading { get; set; }
        [MaxLength(5000)]
        public string  Description { get; set; }


        public DateTime  LastApplyDate { get; set; }


        public DateTime CreatedDate { get; set; } = DateTime.Now;


        public DateTime UpdatedDate { get; set; } = DateTime.Now;


        [MaxLength(100)]
        public string Applytype { get; set; }

        public int JobTypeID { get; set; }


        [MaxLength(500)]
        public string ExternalUrl { get; set; }

        public bool Approved { get; set; } = false;

        public int CompanyID { get; set; }

        public int? StatusIDs { get; set; }

        public int? NotesID { get; set; }

        public int? durationID { get; set; }

        [MaxLength(100)]
        public string Jobduration { get; set; }
        public string JobdocsPath { get; set; }

        [ForeignKey("CompanyID")]
        public virtual Companies Companies { get; set; }

        [ForeignKey("JobCategoryId")]
        public virtual JobCategory jobcategories { get; set; }

        [ForeignKey("JobTypeID")]
        public virtual Jobtypes Jobtypes { get; set; }

        [ForeignKey("durationID")]
        public virtual Job_durations Job_durations { get; set; }       
        

        [ForeignKey("StatusIDs")]
        public virtual Statuses Statuses { get; set; }

        // public ICollection<JobSkillset> Skillsets { get; } = new List<JobSkillset>();
        [ForeignKey("ExperienceID")]
        public virtual JobExperiences Experiences { get; set; }

        [ForeignKey("NotesID")]
        public StatusesNotes StatusesNotes { get; set; }
        public ICollection<AppliedJobs> AppliedJobs { get; } = new List<AppliedJobs>();
        public ICollection<JobsWorkAvailability> JobsWorkAvailability { get; } = new List<JobsWorkAvailability>();
        public JobOptions JobOptions { get; set; }

    }
}
