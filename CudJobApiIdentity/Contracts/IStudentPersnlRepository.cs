using CUDJobApiIdentity.DTOs;
using CUDJobApiIdentity.Models;
using CUDJobAPiIdentity.Contracts;
using CUDJobAPiIdentity.DTOs;
using CUDJobAPiIdentity.Models;
using CUDJobAPiIdentity.VIewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Contracts
{
    public interface IStudentPersnlRepository
    {
        //Below Section is for adding sections to the Student profile

        Task<bool> IstudentCreated(string emailID); //This will be used by front end to check a student exists based on email ID

        Task<bool> AddStudentPersonal(StudentPersonalview entity);

        Task<bool> AddEducation(IList<StudentEducationDTO> entity);

        Task<bool> AddStudentExperience(IList<StudentExperienceDTO> entity);
        Task<bool> AddVolunteerExperience(IList<StudentVolunteerExperienceDTO> entity);
        Task<bool> Addportfolio(IList<StudentPortfolioDTO> entity);
        Task<bool> AddStudentMemberships(IList<MembershipDTO> entity);

        Task<bool> AddStudentProjectss(IList<ProjectDTO> entity);

        Task<bool> AddStudentAwards(IList<AwardsDTO> entity);

        Task<bool> AddStudentLanguages(IList<LanguagesDTO> entity);

        Task<bool> AddStudentAvailability(IList<StudentWorkAvailabilityDTO> entity);
        Task<bool> AddStudentSkills(SkillSetVM entity);

        //Below Section is for updating the Student profile
        Task<bool> UpdateStudentPersonal(StudentPersonalview entity);

        Task<bool> UpdateEducation(IList<StudentEducationDTO> entity);

        Task<bool> UpdateStudentExperience(IList<StudentExperienceDTO> entity);

        Task<bool> UpdateVolunteerExperience(IList<StudentVolunteerExperienceDTO> entity);
        Task<bool> Updateportfolio(IList<StudentPortfolioDTO> entity);

        Task<bool> UpdateStudentMemberships(IList<MembershipDTO> entity);

        Task<bool> UpdateStudentProjectss(IList<ProjectDTO> entity);
        
        Task<bool> UpdateStudentAwards(IList<AwardsDTO> entity);
        
        Task<bool> UpdateStudentLanguages(IList<LanguagesDTO> entity);        
        Task<bool> UpdateStudentAvailability(IList<StudentWorkAvailabilityDTO> entity);
        Task<bool> UpdateStudentComputerSkill(StudentComputerSkillsDTO entity);

        Task<bool> DeleteStudentPersonal(StudentPersonalview entity);

        Task<bool> DeleteEducation(StudentEducationDTO entity);

        Task<bool> DeleteStudentExperience(StudentExperienceDTO entity);

        Task<bool> DeleteStudentMemberships(IList<MembershipDTO> entity);

        Task<bool> DeleteStudentProjectss(ProjectDTO entity);

        Task<bool> DeleteStudentAwards(AwardsDTO entity);

        Task<bool> DeleteStudentLanguages(LanguagesDTO entity);

        Task<bool> DeleteStudentAvailability(StudentWorkAvailabilityDTO entity);     
        Task<bool> DeleteStudentSkills(int id, string Type);

        Task<bool> isExists(int id);
        Task<bool> isSkillExists(int id, string Type);

        Task<StudentEducationDTO> FindbyEduID(int id);
        //Task<StudentEducationDTO> FindbySkillID(int id, string Type);
        Task<StudentExperienceDTO> FindbyExpID(int id);
        Task<IList<MembershipDTO>> FindbyMembershipID(int id);
        Task<AwardsDTO> FindbyAwardID(int id);
        Task<ProjectDTO> FindbyProjectsID(int id);
        Task<LanguagesDTO> FindbyLanguageID(int id);
        Task<StudentWorkAvailabilityDTO> FindbyAvailabilityID(int id);

        Task<bool> Save();
    }
}
