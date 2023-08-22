using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.ViewModels
{
    public class JobDetails
    {
        public int Id { get; set; }

        public int CompanyID { get; set; }
        [Display(Name ="Job Title")]
        public string Title { get; set; }
        [Display(Name = "Job Category")]
        public int JobCategoryId { get; set; }
        [Display(Name = "Required Skills")] 
        public string ReguiredSkills { get; set; }

        [Display(Name = "Required Qualification")]
        public string Qualification { get; set; }
        [Display(Name = "Required Experience")]
        public string Experience { get; set; }
        [Display(Name = "Required Experience")]
        public int ExperienceID { get; set; }
        [Display(Name = "Expected Salary")]
        public string ExpectedSalary { get; set; }

        public string Shortheading { get; set; }

        [Display(Name = "Job Description")]
        public string Description { get; set; }
        [Display(Name = "Apply Type")]
        public string Applytype { get; set; }
        [Display(Name = "External Url")]
        public string ExternalUrl { get; set; }

        public int? StatusIDs { get; set; }

        public int? NotesID { get; set; }

        [Display(Name = "Contract Type")]
        public int JobTypeID { get; set; }
        
        [Display(Name = "Job Duration")]
        public int? durationID { get; set; }

        [Display(Name = "Duration")]
        public string Jobduration { get; set; }       
        public string JobdocsPath { get; set; }

        public string Type { get; set; }       


        [Display(Name = "Job Expiry")]     
        public DateTime LastApplyDate { get; set; }
        
        [Display(Name = "Job Creation")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; } = DateTime.Now;


        public string[] Applytypes = new[] { "Through CUD Career Portal", "Through the employer's portal" };
        
        public IList<JobsWorkAvailability> JobsWorkAvail { get; set; }

        public JobOptions JobOption { get; set; }
        public StatusesNotes StatusesNotes { get; set; }

    }

    public class JobCategory
    {
        public string JobCategoryId { get; set; }
        public string JobCategoryName { get; set; }
    }

    public class Jobtypes
    {
        public string JobTypeID { get; set; }
        public string Description { get; set; }
    }

    public class Job_durations
    {
        public int durationID { get; set; }
        public string Description { get; set; }
    }

    public class JobOptions
    {
        public int JoboptionID { get; set; }
        public int JobID { get; set; }

        [Display(Name = "Flexible Hours")]
        public string FlexibleHours { get; set; }

        [Display(Name = "Work From Office")]
        public string Workfromoffice { get; set; }

        [Display(Name = "Paid")]
        public string Paid { get; set; }
    }

    public class JobExperiences
    {
        public string ExperienceID { get; set; }
        public string ExperienceType { get; set; }
    }

    public class JobsWorkAvailability
    {
        public int AvailabilityID { get; set; }

        public int JobID { get; set; }

        [Display(Name = "Days Per Week")]
        public int DaysPerWeekID { get; set; }

        [Display(Name = "Hours Per Week")]
        public int HoursPerWeekID { get; set; }

        public virtual DaysPerWeekOptions DaysPerWeek { get; set; }

        public virtual HoursPerWeekOptions HoursPerWeek { get; set; }

    }

    public class AppliedJobs
    {
        public int ID { get; set; }
        public int jobID { get; set; }
        public int StudentID { get; set; }
        public int StatusID { get; set; }
        public string Description { get; set; }
        public DateTime AppliedDate { get; set; } = DateTime.Now;
       
    }
    public class JobViewModel
    {
        public JobDetails JobDetail { get; set; }

        public CompanyDetails Company { get; set; }

        public CompanyContact CompanyContact { get; set; }

        public Address CompanyAddress { get; set; }

        public IList<JobsWorkAvailability> JobsWorkAvail { get; set; }

        public DaysPerWeekOptions DaysPerWeek { get; set; }

        public HoursPerWeekOptions HoursPerWeek { get; set; }

        public JobCategory jobcategory { get; set; }

        public JobExperiences JobExperience { get; set; }

        public Jobtypes Jobtype { get; set; }

        public Job_durations Job_duration { get; set; }

        public JobOptions JobOption { get; set; }

        public Statuses jobstatus { get; set; }

        public StatusesNotes jobstatusNotes { get; set; }

        public string AppliedNumbers { get; set; }

        public string HiredApplicationNumbers { get; set; }

    }
    public class JobApplyDetails
    {
        public AppliedJobs AppliedJob { get; set; }
        public StudentModel StudentPersonal { get; set; }
        public JobStatuses JobStatus { get; set; }

    }
    public class JobApplyDetailsBystudent
    {
        public AppliedJobs AppliedJob { get; set; }
        public JobDetails JobDetails { get; set; }
        public CompanyDetails Company { get; set; }
        public JobStatuses JobStatus { get; set; }

    }
}
