using AutoMapper;
using CUDJobApiIdentity.DTOs;
using CUDJobApiIdentity.Models;
using CUDJobAPiIdentity.DTOs;
using CUDJobAPiIdentity.Models;
using CUDJobAPiIdentity.VIewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<StudentSeekers, StudentProfileDTO>().ReverseMap();
            CreateMap<StudentEducation, StudentEducationDTO>().ReverseMap();
            CreateMap<StudentExperience, StudentExperienceDTO>().ReverseMap();
            //CreateMap<EducationDegree, StudentDegreeDTO>().ReverseMap();
            CreateMap<Jobs, JobDetailsDTO>().ReverseMap();
            //CreateMap<JobSkillset, JobReqSkillDTO>().ReverseMap();
            CreateMap<Companies, CompanyDTO>().ReverseMap();
            CreateMap<CompanyContacts, CompanyContactDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<AppliedJobs, AppliedJobsDTO>().ReverseMap();
            CreateMap<Awards, AwardsDTO>().ReverseMap();
            CreateMap<Memberships, MembershipDTO>().ReverseMap();
            CreateMap<Projects, ProjectDTO>().ReverseMap();
            CreateMap<Languages, LanguagesDTO>().ReverseMap();
            CreateMap<JobCategory, JobCategoryDTO>().ReverseMap();
            CreateMap<JobStatuses, JobStatusesDTO>().ReverseMap();
            CreateMap<JobExperiences, JobExperiencesDTO>().ReverseMap();
            CreateMap<Studentportfolio, StudentPortfolioDTO>().ReverseMap();
            CreateMap<StudentWorkAvailability, StudentWorkAvailabilityDTO>().ReverseMap();
            CreateMap<CompanyOptional, CompanyOptionalDTO>().ReverseMap();
            CreateMap<JobsWorkAvailability, JobsWorkAvailabilityDTO>().ReverseMap();
            CreateMap<StudentComputerSkills, StudentComputerSkillsDTO>().ReverseMap();
            CreateMap<StudentSoftSkills, StudentSoftSkillsDTO>().ReverseMap();
            CreateMap<StudentHardSkills, StudentHardskillsDTO>().ReverseMap();
            CreateMap<JobOptions, JobOptionsDTO>().ReverseMap();
            CreateMap<VolunteerExperience, StudentVolunteerExperienceDTO>().ReverseMap();
        }
      
    }
}
