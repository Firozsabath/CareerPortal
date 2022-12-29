using CUDJobApiIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.DTOs
{
    public class JobDetailsDTO
    {
        public int Id { get; set; }
        public int CompanyID { get; set; }
        public string Title { get; set; }

        public int JobCategoryId { get; set; }

        public string ReguiredSkills { get; set; }

        public string Qualification { get; set; }

        public string Experience { get; set; }

        public int ExperienceID { get; set; }

        public string ExpectedSalary { get; set; }

        public string Description { get; set; }

        public string Shortheading { get; set; }

        public string Applytype { get; set; }

        public string ExternalUrl { get; set; }

        public int? StatusIDs { get; set; }

        public int? NotesID { get; set; }

        public int JobTypeID { get; set; }

        public int? durationID { get; set; }

        public string Jobduration { get; set; }

        public DateTime LastApplyDate { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }

        public IList<JobsWorkAvailabilityDTO> JobsWorkAvail { get; set; }

        public JobOptionsDTO JobOption { get; set; }

        public StatusesNotes StatusesNotes { get; set; }
    }

    public class JobOptionsDTO
    {
        public int JoboptionID { get; set; }
        public int JobID { get; set; }
        public string FlexibleHours { get; set; }
        public string Workfromoffice { get; set; }
        public string Paid { get; set; }
    }

    public class JobsWorkAvailabilityDTO
    {
        public int AvailabilityID { get; set; }

        public int JobID { get; set; }

        public int DaysPerWeekID { get; set; }

        public int HoursPerWeekID { get; set; }

        public virtual DaysPerWeekOptions DaysPerWeek { get; set; }
        
        public virtual HoursPerWeekOptions HoursPerWeek { get; set; }
    }

    public class JobCategoryDTO
    {
        public int JobCategoryId { get; set; }
        public string JobCategoryName { get; set; }
    }

    public class JobStatusesDTO
    {
        public int StatusID { get; set; }       
        public string Status { get; set; }
    }

    public class JobExperiencesDTO
    {
        public int ExperienceID { get; set; }
        public string ExperienceType { get; set; }
    }

    //public class JobReqSkillDTO
    //{
    //    public string Skill { get; set; }
    //    public int Skilllevel { get; set; }
    //}
}
