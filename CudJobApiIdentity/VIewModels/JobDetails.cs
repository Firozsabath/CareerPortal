using CUDJobApiIdentity.DTOs;
using CUDJobApiIdentity.Models;
using CUDJobAPiIdentity.DTOs;
using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.VIewModels
{
    public class JobDetails
    {
        public JobDetailsDTO JobDetail { get; set; }

        public CompanyDTO Company { get; set; }

        public CompanyContactDTO CompanyContact { get; set; }

        public AddressDTO CompanyAddress { get; set; }

        public IList<JobsWorkAvailability> JobsWorkAvail { get; set; }

        public DaysPerWeekOptions DaysPerWeek { get; set; }

        public HoursPerWeekOptions HoursPerWeek { get; set; }

        public JobCategoryDTO jobcategory { get; set; }

        public JobExperiences JobExperience { get; set; }

        public Jobtypes Jobtype { get; set; }

        public Job_durations Job_duration { get; set; }

        public JobOptionsDTO JobOption { get; set; }

        public Statuses jobstatus { get; set; }

        public StatusesNotes jobstatusNotes { get; set; }

        public string AppliedNumbers { get; set; }

        public string HiredApplicationNumbers { get; set; }


    }
    public class JobApplyDetails
    {
        public AppliedJobsDTO AppliedJob { get; set; }
        public StudentProfileDTO StudentPersonal { get; set; }
        public JobStatusesDTO JobStatus { get; set; }      


    }

    public class JobApplyDetailsBystudent
    {
        public AppliedJobsDTO AppliedJob { get; set; }
        public JobDetailsDTO JobDetails { get; set; }
        public CompanyDTO Company { get; set; }
        public JobStatusesDTO JobStatus { get; set; }

    }
}
