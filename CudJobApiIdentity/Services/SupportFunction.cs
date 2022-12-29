using AutoMapper;
using CUDJobApiIdentity.Contracts;
using CUDJobApiIdentity.DTOs;
using CUDJobApiIdentity.Models;
using CUDJobApiIdentity.VIewModels;
using CUDJobAPiIdentity.DTOs;
using CUDJobAPiIdentity.Models;
using CUDJobAPiIdentity.VIewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Services
{
    public class SupportFunction : ISupportFunction
    {
        private readonly IMapper _Mapper;
        private readonly IExternalFunctions _externalFunctions;
        public SupportFunction(IMapper Mapper, IExternalFunctions externalFunctions)
        {
            _Mapper = Mapper;
            _externalFunctions = externalFunctions;
        }
        public StudentProfile ConvertToStudentProfile(StudentSeekers entity)
        {
            List<StudentWorkAvailability> StudentList = new List<StudentWorkAvailability>();
            foreach (var items in entity.StudentWorkAvailability)
            {
                StudentList.Add(new StudentWorkAvailability
                {
                    AvailabilityID = items.AvailabilityID,
                    DaysPerWeekID = items.DaysPerWeekID,
                    DaysPerWeekOptions = entity.StudentWorkAvailability.FirstOrDefault().DaysPerWeekOptions,
                    HoursPerWeekOptions = entity.StudentWorkAvailability.FirstOrDefault().HoursPerWeekOptions,
                    HoursPerWeekID = items.HoursPerWeekID,
                    StudentID = items.StudentID,
                });
             }

            //List<StudentComputerSkills> StCompSkills = new List<StudentComputerSkills>();
            //foreach (var items in entity.StudentComputerSkills)
            //{
            //    StCompSkills.Add(new StudentComputerSkills
            //    {
            //        StudentCsID = items.StudentCsID,
            //        ComputerSkillID = items.ComputerSkillID,
            //        Computerskills = entity.StudentComputerSkills.FirstOrDefault().Computerskills,
            //        CreatedDate = items.CreatedDate,                   
            //        StudentID = items.StudentID,
            //    });
            //}

            //List<StudentSoftSkills> StsoftSkills = new List<StudentSoftSkills>();
            //foreach (var items in entity.StudentSoftSkills)
            //{
            //    StsoftSkills.Add(new StudentSoftSkills
            //    {
            //        StudentSsID = items.StudentSsID,
            //        CreatedDate = items.CreatedDate,
            //        SoftSkillID = items.SoftSkillID,
            //        //Softskills = items.Softskills.SoftSkills,                    
            //        StudentID = items.StudentID,
            //    });
            //}

            //List<StudentHardSkills> StHardSkills = new List<StudentHardSkills>();
            //foreach (var items in entity.StudentHardSkills)
            //{
            //    StHardSkills.Add(new StudentHardSkills
            //    {
            //        StudentHsID = items.StudentHsID,
            //        CreatedDate = items.CreatedDate,
            //        HardSkillID = items.HardSkillID,
            //        Hardskills = entity.StudentHardSkills.FirstOrDefault().Hardskills,                    
            //        StudentID = items.StudentID,
            //    });
            //}

            List<Languages> Languagesspk = new List<Languages>();
            foreach (var items in entity.Languages)
            {
                Languagesspk.Add(new Languages
                {
                    ID = items.ID,
                    LanguageID = items.LanguageID,
                    LevelID = items.LevelID,
                    LanguageNames = entity.Languages.FirstOrDefault().LanguageNames,
                    LanguageLevels = entity.Languages.FirstOrDefault().LanguageLevels,
                    StudentID = items.StudentID,
                });
            }
            Address staddress = new Address
            {
                Address1 = entity.StudentAddresses.FirstOrDefault().Address.Address1,
                State = entity.StudentAddresses.FirstOrDefault().Address.State,
                CountryCode = entity.StudentAddresses.FirstOrDefault().Address.CountryCode,
                CountryID = entity.StudentAddresses.FirstOrDefault().Address.CountryID,
                Nationality = entity.StudentAddresses.FirstOrDefault().Address.Nationality,
                NationalityCode = entity.StudentAddresses.FirstOrDefault().Address.NationalityCode,
                City = entity.StudentAddresses.FirstOrDefault().Address.City,
                AddressID = entity.StudentAddresses.FirstOrDefault().Address.AddressID,
                AddressTypeID = entity.StudentAddresses.FirstOrDefault().Address.AddressTypeID,
                CreateddDate = entity.StudentAddresses.FirstOrDefault().Address.CreateddDate,
                UpdatedTime = entity.StudentAddresses.FirstOrDefault().Address.UpdatedTime,
                CompanyAddresses = entity.StudentAddresses.FirstOrDefault().Address.CompanyAddresses,
                PinCode = entity.StudentAddresses.FirstOrDefault().Address.PinCode
            };

            List<StudentExperience> StudentExperience = new List<StudentExperience>();
            foreach (var items in entity.StudentExperiences)
            {
                StudentExperience.Add(new StudentExperience
                {
                    CategoryID = items.CategoryID,
                    companycategory = entity.StudentExperiences.FirstOrDefault().companycategory,
                    CompanyName = items.CompanyName,
                    Companywebsite = items.Companywebsite,
                    StartDate = items.StartDate,
                    EndDate = items.EndDate,
                    Department = items.Department,
                    JobDescription = items.JobDescription,
                    JobTitle = items.JobTitle,
                    Id = items.Id,
                    StudentID = items.StudentID,
                    CreatedDate = items.CreatedDate,
                    Location = items.Location                    
                });
            }
            StudentProfile student = new StudentProfile
            {
                StudentPersonal = _Mapper.Map<StudentProfileDTO>(entity),               
                StudentExperience = _Mapper.Map<IList<StudentExperienceDTO>>(StudentExperience),
                VolunteerExperience = _Mapper.Map<IList<StudentVolunteerExperienceDTO>>(entity.VolunteerExperiences),
                StudentEducation = _Mapper.Map<IList<StudentEducationDTO>>(entity.StudentEducations.ToList()),
                Awards = _Mapper.Map<IList<AwardsDTO>>(entity.Award),
                Memberships = _Mapper.Map<IList<MembershipDTO>>(entity.Memberships.ToList()),
                projects = _Mapper.Map<IList<ProjectDTO>>(entity.Projects.ToList()),
                //languages = _Mapper.Map<IList<LanguagesDTO>>(Languagesspk),
                languages = _Mapper.Map<IList<LanguagesDTO>>(entity.Languages),
                //StudentAddress = _Mapper.Map<AddressDTO>(entity.StudentAddresses.FirstOrDefault().Address),
                StudentAddress = _Mapper.Map<AddressDTO>(staddress),
                Portfolio = _Mapper.Map<IList<StudentPortfolioDTO>>(entity.Studentportfolio.ToList()),                
                StudentWorkAvailability = _Mapper.Map<IList<StudentWorkAvailability>>(StudentList),
                programs = entity.programs,
                StudentHardSkills = _Mapper.Map<IList<StudentHardskillsDTO>>(entity.StudentHardSkills),
                StudentSoftSkills = _Mapper.Map<IList<StudentSoftSkillsDTO>>(entity.StudentSoftSkills),
                StudentComputerSkills = _Mapper.Map<IList<StudentComputerSkillsDTO>>(entity.StudentComputerSkills),
                //Hardskills = entity.Hardskills,
                //Softskills = entity.Softskills,
                //Computerskills = entity.Computerskills
              
            };
            
            return student;
        }
        public IList<StudentProfile> ConvertToStudentProfileList(List<StudentSeekers> entity)
        {
            List<StudentProfile> StudentList = new List<StudentProfile>();
            foreach (var item in entity)
            {
                StudentList.Add(new StudentProfile
                {
                    StudentPersonal = _Mapper.Map<StudentProfileDTO>(item),
                    //StudentExperience = _Mapper.Map<StudentExperienceDTO>(item.StudentExperiences.FirstOrDefault()),
                    //StudentEducation = _Mapper.Map<StudentEducationDTO>(item.StudentEducations.FirstOrDefault()),
                    //StudentDegree = _Mapper.Map<StudentDegreeDTO>(item.StudentEducations.Select(a => a.EducationDegree).FirstOrDefault().FirstOrDefault()),
                    StudentAddress = _Mapper.Map<AddressDTO>(item.StudentAddresses.FirstOrDefault().Address)
                    ,programs = item.programs
                });
            }

            return StudentList;
        }

        public JobDetails ConvertJobDetais(Jobs entity)
        {
            List<JobsWorkAvailability> Availability = new List<JobsWorkAvailability>();
            foreach (var items in entity.JobsWorkAvailability)
            {
                Availability.Add(new JobsWorkAvailability
                {
                    AvailabilityID = items.AvailabilityID,
                    DaysPerWeekID = items.DaysPerWeekID,
                    DaysPerWeek = entity.JobsWorkAvailability.FirstOrDefault().DaysPerWeek,
                    HoursPerWeek = entity.JobsWorkAvailability.FirstOrDefault().HoursPerWeek,
                    HoursPerWeekID = items.HoursPerWeekID,
                    JobID = items.JobID,
                });
            }            
            JobDetails job = new JobDetails
            {

                JobDetail = _Mapper.Map<JobDetailsDTO>(entity),                
                Company = _Mapper.Map<CompanyDTO>(entity.Companies),
                CompanyContact = _Mapper.Map<CompanyContactDTO>(entity.Companies.CompanyContacts.FirstOrDefault()),
                CompanyAddress = _Mapper.Map<AddressDTO>(entity.Companies.addresses.FirstOrDefault().Address),
                jobcategory = _Mapper.Map<JobCategoryDTO>(entity.jobcategories),                
                JobsWorkAvail = _Mapper.Map<IList<JobsWorkAvailability>>(Availability),
                JobExperience = entity.Experiences,
                JobOption = _Mapper.Map<JobOptionsDTO>(entity.JobOptions),
                jobstatusNotes = _Mapper.Map<StatusesNotes>(entity.StatusesNotes),
                Jobtype = entity.Jobtypes,
                Job_duration = entity.Job_durations

            };
            return job;
        }

        public IList<JobDetails> ConvertJobDetailsList(IList<Jobs> entity)
        {
            List<JobDetails> Jobs = new List<JobDetails>();

            foreach (var item in entity)
            {
                Jobs.Add(new JobDetails()
                {
                    JobDetail = _Mapper.Map<JobDetailsDTO>(item),
                    Company = _Mapper.Map<CompanyDTO>(item.Companies),
                    CompanyContact = _Mapper.Map<CompanyContactDTO>(item.Companies.CompanyContacts.FirstOrDefault()),
                    CompanyAddress = _Mapper.Map<AddressDTO>(item.Companies.addresses.FirstOrDefault().Address),
                    jobcategory = _Mapper.Map<JobCategoryDTO>(item.jobcategories),
                    Jobtype = _Mapper.Map<Jobtypes>(item.Jobtypes),
                    jobstatus = _Mapper.Map<Statuses>(item.Statuses),
                    JobExperience = item.Experiences,
                    JobOption = _Mapper.Map<JobOptionsDTO>(item.JobOptions),
                    AppliedNumbers = _externalFunctions.ApplicationCount(item.Id),
                    HiredApplicationNumbers = _externalFunctions.HiredApplicationCount()
                });
            }
            var joblist = Jobs.OrderByDescending(x => x.JobDetail.UpdatedDate).ToList();
            return joblist;
        }      

        public CompanyViewModel ConvertToCompanyViewModel(Companies entity)
        {
            CompanyViewModel CompanyViewModel = new CompanyViewModel
            {
                Companies = _Mapper.Map<CompanyDTO>(entity),
                CompanyContacts = _Mapper.Map<CompanyContactDTO>(entity.CompanyContacts.FirstOrDefault()),
                CompanyAddress = _Mapper.Map<AddressDTO>(entity.addresses.FirstOrDefault().Address),
                Companycategory = entity.companycategory,
                CompanyOptional = _Mapper.Map<CompanyOptionalDTO>(entity.CompanyOptional),
                Country = entity.addresses.FirstOrDefault().Address.CountryCode,
                CompanySectors = entity.CompanySectors
            };

            return CompanyViewModel;
        }


        public IList<CompanyViewModel> ConvertToCompanyViewModelList(List<Companies> entity)
        {
            List<CompanyViewModel> CompanyList = new List<CompanyViewModel>();
            foreach (var item in entity)
            {
                CompanyList.Add(new CompanyViewModel
                {
                    Companies = _Mapper.Map<CompanyDTO>(item),
                    CompanyContacts = _Mapper.Map<CompanyContactDTO>(item.CompanyContacts.FirstOrDefault()),
                    CompanyAddress = _Mapper.Map<AddressDTO>(item.addresses.FirstOrDefault().Address),
                    Companycategory = item.companycategory,
                    CompanySectors = item.CompanySectors,
                    Country = item.addresses.FirstOrDefault().Address.CountryCode
                });
            }
            return CompanyList;
        }

        public IList<JobApplyDetails> ConvertToApplyJobViewModelList(List<AppliedJobs> entity)
        {
            List<JobApplyDetails> AppliedJobs = new List<JobApplyDetails>();
            foreach (var item in entity)
            {
                AppliedJobs.Add(new JobApplyDetails
                {
                    AppliedJob = _Mapper.Map<AppliedJobsDTO>(item),
                    StudentPersonal = _Mapper.Map<StudentProfileDTO>(item.Student),
                    JobStatus = _Mapper.Map<JobStatusesDTO>(item.JobStatuses)
                });
            }
            return AppliedJobs;
        }

        public IList<JobApplyDetailsBystudent> ConvertToApplyJobViewByStudent(List<AppliedJobs> entity)
        {
            List<JobApplyDetailsBystudent> Appliedjobsbystudent = new List<JobApplyDetailsBystudent>();

            foreach (var item in entity)
            {
                Appliedjobsbystudent.Add(new JobApplyDetailsBystudent
                {
                    AppliedJob = _Mapper.Map<AppliedJobsDTO>(item),
                    JobDetails = _Mapper.Map<JobDetailsDTO>(item.job),
                    Company = _Mapper.Map<CompanyDTO>(item.job.Companies),
                    JobStatus = _Mapper.Map<JobStatusesDTO>(item.JobStatuses)
                });
            }
            return Appliedjobsbystudent;
        }

        public StudentPersonalview ConvertStudentPersonal(StudentSeekers entity)
        {
            StudentPersonalview student = new StudentPersonalview
            {
                StudentPersonal = _Mapper.Map<StudentProfileDTO>(entity),
                StudentAddress = _Mapper.Map<AddressDTO>(entity.StudentAddresses.FirstOrDefault().Address)
            };
            return student;
        }

        
    }
}
