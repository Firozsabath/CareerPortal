using AutoMapper;
using CUDJobApiIdentity.Contracts;
using CUDJobApiIdentity.DTOs;
using CUDJobApiIdentity.Models;
using CUDJobAPiIdentity.Contracts;
using CUDJobAPiIdentity.Data;
using CUDJobAPiIdentity.DTOs;
using CUDJobAPiIdentity.Models;
using CUDJobAPiIdentity.VIewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Services
{
    public class StudentPersnlRepository : IStudentPersnlRepository
    {
        public readonly ApplicationDbContext _db;
        private readonly ISupportFunction _SupportFunctions;
        private readonly IMapper _Mapper;
        public readonly ILoggerService _Logger;
        public StudentPersnlRepository(ApplicationDbContext db, ISupportFunction SupportFunctions, IMapper Mapper, ILoggerService Logger)
        {
            _db = db;
            _SupportFunctions = SupportFunctions;
            _Mapper = Mapper;
            _Logger = Logger;
        }

        public async Task<bool> UpdateStudentPersonal(StudentPersonalview entity)
        {
            var StdPersonal = _Mapper.Map<StudentSeekers>(entity.StudentPersonal);
            var address = _Mapper.Map<Address>(entity.StudentAddress);

            _db.Students.Update(StdPersonal);
            _db.Address.Update(address);
            return await Save();
        }

        public async Task<bool> UpdateEducation(IList<StudentEducationDTO> entity)
        {
            var StdEdu = _Mapper.Map<IList<StudentEducation>>(entity);
            IList<StudentEducation> StdEducation = new List<StudentEducation>();
            //StudentEducation StdEducation = new StudentEducation();
            foreach (var item in StdEdu)
            {
                StdEducation.Add(item);
            }

            _db.Studenteducation.UpdateRange(StdEducation);

            return await Save();
        }

        public async Task<bool> UpdateStudentExperience(IList<StudentExperienceDTO> entity)
        {
            var StdExperience = _Mapper.Map<IList<StudentExperience>>(entity);

            _db.StudentExperience.UpdateRange(StdExperience);
            return await Save();
        }

        public async Task<bool> UpdateStudentMemberships(IList<MembershipDTO> entity)
        {
            var StdMemberships = _Mapper.Map<IList<Memberships>>(entity);

            _db.Memberships.UpdateRange(StdMemberships);
            return await Save();


        }

        public async Task<bool> UpdateStudentProjectss(IList<ProjectDTO> entity)
        {
            var StdProjects = _Mapper.Map<IList<Projects>>(entity);

            _db.Projects.UpdateRange(StdProjects);
            return await Save();
        }

        public async Task<bool> UpdateStudentAwards(IList<AwardsDTO> entity)
        {
            var StdAwards = _Mapper.Map<IList<Awards>>(entity);

            _db.Awards.UpdateRange(StdAwards);
            return await Save();
        }

        public async Task<bool> UpdateStudentLanguages(IList<LanguagesDTO> entity)
        {
            var StdLanguages = _Mapper.Map<IList<Languages>>(entity);

            _db.Languages.UpdateRange(StdLanguages);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.Students.AnyAsync(o => Convert.ToInt32(o.StudentID) == id);
        }

        public async Task<bool> AddStudentPersonal(StudentPersonalview entity)
        {
            var StdPersonal = _Mapper.Map<StudentSeekers>(entity.StudentPersonal);
            await _db.Students.AddAsync(StdPersonal);
            return await Save();
        }

        public async Task<bool> AddEducation(IList<StudentEducationDTO> entity)
        {
            var StdEdu = _Mapper.Map<IList<StudentEducation>>(entity);
            foreach (var Edu in StdEdu)
            {
                await _db.Studenteducation.AddAsync(Edu);
            }
            return await Save();
        }

        public async Task<bool> AddStudentExperience(IList<StudentExperienceDTO> entity)
        {
            var StdEExp = _Mapper.Map<IList<StudentExperience>>(entity);
            foreach (var Exp in StdEExp)
            {
                await _db.StudentExperience.AddAsync(Exp);
            }
            return await Save();
        }

        public async Task<bool> AddStudentMemberships(IList<MembershipDTO> entity)
        {
            var StdMemberships = _Mapper.Map<IList<Memberships>>(entity);
            foreach (var Membership in StdMemberships)
            {
                await _db.Memberships.AddAsync(Membership);
            }
            return await Save();
        }

        public async Task<bool> AddStudentProjectss(IList<ProjectDTO> entity)
        {
            var StdProject = _Mapper.Map<IList<Projects>>(entity);
            foreach (var Project in StdProject)
            {
                await _db.Projects.AddAsync(Project);
            }
            return await Save();
        }

        public async Task<bool> AddStudentAwards(IList<AwardsDTO> entity)
        {
            var StdAwards = _Mapper.Map<IList<Awards>>(entity);
            foreach (var Awards in StdAwards)
            {
                await _db.Awards.AddAsync(Awards);
            }
            return await Save();
        }

        public async Task<bool> AddStudentLanguages(IList<LanguagesDTO> entity)
        {
            var StdLanguage = _Mapper.Map<IList<Languages>>(entity);
            foreach (var Lang in StdLanguage)
            {
                await _db.Languages.AddAsync(Lang);
            }
            return await Save();
        }

        public async Task<bool> AddStudentAvailability(IList<StudentWorkAvailabilityDTO> entity)
        {
            var StdAvailability = _Mapper.Map<IList<StudentWorkAvailability>>(entity);
            foreach (var Availability in StdAvailability)
            {
                await _db.StudentWorkAvailability.AddAsync(Availability);
            }
            return await Save();
        }

        public async Task<bool> UpdateStudentAvailability(IList<StudentWorkAvailabilityDTO> entity)
        {
            var StdAvailability = _Mapper.Map<IList<StudentWorkAvailability>>(entity);

            _db.StudentWorkAvailability.UpdateRange(StdAvailability);
            return await Save();
        }

       

        public async Task<bool> DeleteEducation(StudentEducationDTO entity)
        {
            var StdEdu = _Mapper.Map<StudentEducation>(entity);
            _db.Studenteducation.Remove(StdEdu);
            return await Save();
        }

        public async Task<bool> DeleteStudentExperience(StudentExperienceDTO entity)
        {
            var StdExperience = _Mapper.Map<StudentExperience>(entity);
            _db.StudentExperience.Remove(StdExperience);
            return await Save();
        }

        public async Task<bool> DeleteStudentMemberships(IList<MembershipDTO> entity)
        {
           //var mem = entity;
            var StdMembership = _Mapper.Map<IList<Memberships>>(entity);
            _db.Memberships.RemoveRange(StdMembership);
            return await Save();
        }

        public async Task<bool> DeleteStudentProjectss(ProjectDTO entity)
        {
            var StdProjects = _Mapper.Map<Projects>(entity);
            _db.Projects.Remove(StdProjects);
            return await Save();
        }

        public async Task<bool> DeleteStudentAwards(AwardsDTO entity)
        {
            var StdAwards = _Mapper.Map<Awards>(entity);
            _db.Awards.Remove(StdAwards);
            return await Save();
        }

        public async Task<bool> DeleteStudentLanguages(LanguagesDTO entity)
        {
            var StdLanguages = _Mapper.Map<Languages>(entity);
            _db.Languages.Remove(StdLanguages);
            return await Save();
        }

        public async Task<bool> DeleteStudentAvailability(StudentWorkAvailabilityDTO entity)
        {
            var StdStudentWorkAvailability = _Mapper.Map<StudentWorkAvailability>(entity);
            _db.StudentWorkAvailability.Remove(StdStudentWorkAvailability);
            return await Save();
        }

        public async Task<StudentEducationDTO> FindbyEduID(int id)
        {           
           var std =  _db.Studenteducation.Where(s => s.EducationId == id).FirstOrDefault();
            var student = _Mapper.Map<StudentEducationDTO>(std);
            return student;
        }

        public Task<bool> DeleteStudentPersonal(StudentPersonalview entity)
        {
            throw new NotImplementedException();
        }

        public async Task<StudentExperienceDTO> FindbyExpID(int id)
        {
            
            var std = _db.StudentExperience.Where(s => s.Id == id).FirstOrDefault();
            var student = _Mapper.Map<StudentExperienceDTO>(std);
            return student;
        }

        public async Task<IList<MembershipDTO>> FindbyMembershipID(int id)
        {
           
            var std = _db.Memberships.Where(s => s.MembershipID == id).ToList();
            var student = _Mapper.Map<IList<MembershipDTO>>(std);
            return student;
        }

        public async Task<AwardsDTO> FindbyAwardID(int id)
        {
            
            var std = _db.Awards.Where(s => s.AwardID == id).FirstOrDefault();
            var student = _Mapper.Map<AwardsDTO>(std);
            return student;
        }

        public async Task<ProjectDTO> FindbyProjectsID(int id)
        {
            
            var std = _db.Projects.Where(s => s.PorjectID == id).FirstOrDefault();
            var student = _Mapper.Map<ProjectDTO>(std);
            return student;
        }

        public async Task<LanguagesDTO> FindbyLanguageID(int id)
        {
            
            var std = _db.Languages.Where(s => s.ID == id).FirstOrDefault();
            var student = _Mapper.Map<LanguagesDTO>(std);
            return student;
        }

        public async Task<StudentWorkAvailabilityDTO> FindbyAvailabilityID(int id)
        {
            
           var std = _db.StudentWorkAvailability.Where(s => s.AvailabilityID == id).FirstOrDefault();
            var student = _Mapper.Map<StudentWorkAvailabilityDTO>(std);
            return student;
        }

        public async Task<bool> AddStudentSkills(SkillSetVM entity)
        {
            List<int> skillsIDs = entity.SkillID.Split(',').Select(int.Parse).ToList();
            if(entity.SkillType == "HardSkill")
            {
                foreach (var items in skillsIDs)
                {
                    StudentHardSkills s = new StudentHardSkills { HardSkillID = items, StudentID = entity.StudentID };
                    await _db.StudentHardSkills.AddAsync(s);
                }
            }
            else if(entity.SkillType == "SoftSkill")
            {
                foreach (var items in skillsIDs)
                {
                    StudentSoftSkills s = new StudentSoftSkills { SoftSkillID = items, StudentID = entity.StudentID };
                    await _db.StudentSoftSkills.AddAsync(s);
                }
            }
            //else if(entity.SkillType == "ComputerSkill")
            //{
            //    foreach (var items in skillsIDs)
            //    {
            //        StudentComputerSkills s = new StudentComputerSkills { ComputerSkillID = items, StudentID = entity.StudentID };
            //        await _db.StudentComputerSkills.AddAsync(s);
            //    }
            //}
            
            return await Save();
        }

        public async Task<bool> DeleteStudentSkills(int id, string Type)
        {
            if (Type == "HardSkill")
            {
                var skill = _db.StudentHardSkills.Where(x => x.StudentHsID == id).FirstOrDefault();
                _db.StudentHardSkills.Remove(skill);                
            }
            else if (Type == "SoftSkill")
            {
                var skill = _db.StudentSoftSkills.Where(x => x.StudentSsID == id).FirstOrDefault();
                _db.StudentSoftSkills.Remove(skill);
            }
            else if (Type == "ComputerSkill")
            {
                var skill = _db.StudentComputerSkills.Where(x => x.StudentCsID == id).FirstOrDefault();
                _db.StudentComputerSkills.Remove(skill);
            }
            return await Save();
        }

        public async Task<bool> isSkillExists(int id, string Type)
        {
            if (Type == "HardSkill")
            {
                return await _db.StudentHardSkills.AnyAsync(o => o.StudentHsID == id);
            }
            else if (Type == "SoftSkill")
            {
                return await _db.StudentSoftSkills.AnyAsync(o => o.StudentSsID == id);
            }
            else if (Type == "ComputerSkill")
            {
                return await _db.StudentComputerSkills.AnyAsync(o => o.StudentCsID == id);
            }
            else
            {
                return false;
            }
            
        }

        public async Task<bool> UpdateStudentComputerSkill(StudentComputerSkillsDTO entity)
        {
            var stdCmpSkill = _Mapper.Map<StudentComputerSkills>(entity);
            _db.StudentComputerSkills.Update(stdCmpSkill);
            return await Save();            
        }

        public async Task<bool> AddVolunteerExperience(IList<StudentVolunteerExperienceDTO> entity)
        {
            var Vexperience = _Mapper.Map<IList<VolunteerExperience>>(entity);
            foreach(var exp in Vexperience)
            {
              await _db.VolunteerExperience.AddAsync(exp);
            }            
            return await Save();
        }

        public async Task<bool> UpdateVolunteerExperience(IList<StudentVolunteerExperienceDTO> entity)
        {
            var Vexperience = _Mapper.Map<IList<VolunteerExperience>>(entity);
            _db.VolunteerExperience.UpdateRange(Vexperience);
            return await Save();
        }

        public async Task<bool> Addportfolio(IList<StudentPortfolioDTO> entity)
        {
            var StudentPortfolio = _Mapper.Map<IList<Studentportfolio>>(entity);
            foreach (var portfolio in StudentPortfolio)
            {
                await _db.Studentportfolio.AddAsync(portfolio);
            }
            return await Save();
        }

        public async Task<bool> Updateportfolio(IList<StudentPortfolioDTO> entity)
        {
            var portfolio = _Mapper.Map<IList<Studentportfolio>>(entity);
            _db.Studentportfolio.UpdateRange(portfolio);
            return await Save();
        }

        public async Task<bool> IstudentCreated(string emailID)
        {
            return await _db.Students.AnyAsync(o => o.EmailID == emailID);
        }
    }
}
