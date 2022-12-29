using AutoMapper;
using CUDJobApiIdentity.Contracts;
using CUDJobApiIdentity.DTOs;
using CUDJobApiIdentity.Models;
using CUDJobAPiIdentity.Contracts;
using CUDJobAPiIdentity.Data;
using CUDJobAPiIdentity.Models;
using CUDJobAPiIdentity.VIewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Services
{
    public class StudentRepository : IStudentRepository
    {
        public readonly ApplicationDbContext _db;
        private readonly ISupportFunction _SupportFunctions;
        private readonly IMapper _Mapper;
        public readonly ILoggerService _Logger;
        private readonly IEmailConfig _emailConfig;

        public StudentRepository(ApplicationDbContext db, ISupportFunction SupportFunctions, IMapper Mapper, ILoggerService Logger, IEmailConfig emailConfig)
        {
            _db = db;
            _SupportFunctions = SupportFunctions;
            _Mapper = Mapper;
            _Logger = Logger;
            _emailConfig = emailConfig;
        }
        public async Task<StudentProfile> Create(StudentProfile entity)
        {
            var isexists = await _db.Students.AnyAsync(o => o.EmailID == entity.StudentPersonal.EmailID);
            StudentSeekers Studentdtls = new StudentSeekers();
            if (!isexists)
            {
                var StudentExperienece = _Mapper.Map<IList<StudentExperience>>(entity.StudentExperience);
                var StdEdu = _Mapper.Map<IList<StudentEducation>>(entity.StudentEducation);
                //IList<EducationDegree> StdDegree = _Mapper.Map<IList<EducationDegree>>(entity.StudentDegree);
                var StudentSeeker = _Mapper.Map<StudentSeekers>(entity.StudentPersonal);
                var StudAwards = _Mapper.Map<IList<Awards>>(entity.Awards);
                var StudMemberships = _Mapper.Map<IList<Memberships>>(entity.Memberships);
                var StudProjects = _Mapper.Map<IList<Projects>>(entity.projects);
                var languages = _Mapper.Map<IList<Languages>>(entity.languages);
                var StudentPortfolio = _Mapper.Map<IList<Studentportfolio>>(entity.Portfolio);
                var Studentworkavailbility = _Mapper.Map<IList<StudentWorkAvailability>>(entity.StudentWorkAvailability);
                //var languages = _Mapper.Map<IList<Languages>>(entity.languages);

                StudentEducation StudentEd = new StudentEducation
                {

                    Institution = StdEdu[0].Institution,
                    UpdatedTime = StdEdu[0].UpdatedTime,

                };
                //foreach (var item in StdDegree)
                //{
                //    StudentEd.EducationDegree.Add(item);
                //}               
              
                Studentdtls = StudentSeeker;
                if (StudentExperienece.FirstOrDefault() != null)
                {
                    foreach (var item in StudentExperienece)
                    {
                        Studentdtls.StudentExperiences.Add(item);
                    }
                }
                if (StdEdu.FirstOrDefault() != null)
                {
                    foreach (var item in StdEdu)
                    {
                        Studentdtls.StudentEducations.Add(item);
                    }
                }
                if (StudAwards.FirstOrDefault() != null)
                {
                    foreach (var items in StudAwards)
                    {
                        Studentdtls.Award.Add(items);
                    }
                }
                if (StudMemberships.FirstOrDefault() != null)
                {
                    foreach (var items in StudMemberships)
                    {
                        Studentdtls.Memberships.Add(items);
                    }
                }
                if (StudProjects.FirstOrDefault() != null)
                {
                    foreach (var items in StudProjects)
                    {
                        Studentdtls.Projects.Add(items);
                    }
                }
                if (languages.FirstOrDefault().LanguageID != 0 && languages.FirstOrDefault().LevelID != 0)
                {
                    foreach (var items in languages)
                    {
                        Studentdtls.Languages.Add(items);
                    }
                }
                if (StudentPortfolio.FirstOrDefault() != null)
                {
                    foreach (var items in StudentPortfolio)
                    {
                        Studentdtls.Studentportfolio.Add(items);
                    }
                }
                if (Studentworkavailbility.FirstOrDefault().DaysPerWeekID != 0 && Studentworkavailbility.FirstOrDefault().HoursPerWeekID != 0)
                {
                    foreach (var items in Studentworkavailbility)
                    {
                        Studentdtls.StudentWorkAvailability.Add(items);
                    }
                }

                if (!String.IsNullOrEmpty(entity.Hskills))
                {
                    List<int> HskillsIDs = entity.Hskills.Split(',').Select(int.Parse).ToList();
                    foreach (var items in HskillsIDs)
                    {
                        StudentHardSkills s = new StudentHardSkills { HardSkillID = items, CreatedDate = DateTime.Now };
                        Studentdtls.StudentHardSkills.Add(s);
                    }
                }
                if (!String.IsNullOrEmpty(entity.Sskills))
                {
                    List<int> SskillsIDs = entity.Sskills.Split(',').Select(int.Parse).ToList();
                    foreach (var items in SskillsIDs)
                    {
                        StudentSoftSkills s = new StudentSoftSkills { SoftSkillID = items, CreatedDate = DateTime.Now };
                        Studentdtls.StudentSoftSkills.Add(s);
                    }
                }
                if (!String.IsNullOrEmpty(entity.Cskills))
                {
                    //List<int> CskillsIDs = entity.Cskills.Split(',').Select(int.Parse).ToList();
                    //foreach (var items in CskillsIDs)
                    //{
                    //    StudentComputerSkills s = new StudentComputerSkills { ComputerSkillID = items };
                    //    Studentdtls.StudentComputerSkills.Add(s);
                    //}

                    StudentComputerSkills s = new StudentComputerSkills { ComputerSkill = entity.Cskills, CreatedDate = DateTime.Now };
                    Studentdtls.StudentComputerSkills.Add(s);
                }
                Address addresses = new Address();
                addresses = _Mapper.Map<Address>(entity.StudentAddress);

                StudentAddressCollection StuAddresscollection = new StudentAddressCollection { Address = addresses, Student = Studentdtls };

                await _db.Students.AddAsync(Studentdtls);
                await _db.Address.AddAsync(addresses);
                await _db.AddAsync(StuAddresscollection);
                var isSuccess = await Save();
                if (!isSuccess)
                {
                    _Logger.LogError($"Student Creation Failed.");
                    return entity;
                }                
            }

            
            var response = _SupportFunctions.ConvertToStudentProfile(Studentdtls);
            return response;
        }

        public async Task<bool> Delete(StudentProfile entity)
        {
            var StudentExperienece = _Mapper.Map<IList<StudentExperience>>(entity.StudentExperience);
            var StdEdu = _Mapper.Map<IList<StudentEducation>>(entity.StudentEducation);
            //var StdDegree = _Mapper.Map< IList<EducationDegree>>(entity.StudentDegree);
            var StudentSeeker = _Mapper.Map<StudentSeekers>(entity.StudentPersonal);
            var StudAwards = _Mapper.Map<IList<Awards>>(entity.Awards);
            var StudMemberships = _Mapper.Map<IList<Memberships>>(entity.Memberships);
            var StudProjects = _Mapper.Map<IList<Projects>>(entity.projects);
            var languages = _Mapper.Map<IList<Languages>>(entity.languages);

           
            //foreach (var item in StdDegree)
            //{
            //    StudentEd.EducationDegree.Add(item);
            //}
            StudentSeekers Studentdtls = new StudentSeekers();
            Studentdtls = StudentSeeker;
            foreach (var item in StudentExperienece)
            {
                Studentdtls.StudentExperiences.Add(item);
            }
            foreach (var item in StdEdu)
            {
                Studentdtls.StudentEducations.Add(item);
            }

            _db.Students.Remove(Studentdtls);
            return await Save();
        }

        //public Task<IList<StudentProfile>> FindAll()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<IList<StudentProfile>> FindAll()
        {
            var students = await _db.Students
                 .Include(a => a.StudentExperiences)
                .Include(a => a.StudentEducations)
                //.ThenInclude(a => a.EducationDegree)
                .Include(a=>a.programs)
                .Include(a => a.StudentAddresses).ThenInclude(a => a.Address).ToListAsync();
            var response = _SupportFunctions.ConvertToStudentProfileList(students);
            return response;
        }

            public async Task<StudentProfile> FindById(int id)
            {
                var student = _db.Students.AsNoTracking()
                    .Include(a => a.StudentExperiences).ThenInclude(a=>a.companycategory)
                    .Include(a => a.StudentEducations)
                    .Include(a=>a.VolunteerExperiences)
                    .Include(a=>a.StudentAddresses).ThenInclude(a=>a.Address).ThenInclude(a=>a.CountryCode)
                    .Include(a => a.Award)
                    .Include(a => a.Projects).Include(a=>a.programs)
                    .Include(a => a.Memberships)
                    //.Include(a=>a.Hardskills).Include(a=>a.Softskills)
                    .Include(a=>a.Languages).ThenInclude(a=>a.LanguageNames).Include(a=>a.Languages).ThenInclude(a=>a.LanguageLevels)
                    .Include(a=>a.StudentComputerSkills)
                    .Include(a=>a.StudentHardSkills).ThenInclude(a=>a.Hardskills)
                    .Include(a=>a.StudentSoftSkills).ThenInclude(a=>a.Softskills)
                    .Include(a=>a.Studentportfolio).Include(a=>a.StudentWorkAvailability).ThenInclude(a=>a.HoursPerWeekOptions).Include(a => a.StudentWorkAvailability).ThenInclude(a => a.DaysPerWeekOptions)
                     .Where(a => a.StudentID == id).FirstOrDefault();

                var studentProfile = _SupportFunctions.ConvertToStudentProfile(student);

                return studentProfile;
            }       

        public async Task<bool> isExists(int id)
        {
            return await _db.Students.AnyAsync(o => Convert.ToInt32(o.StudentID) == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(StudentProfile entity)
        {
            var StudentExperienece = _Mapper.Map<StudentExperience>(entity.StudentExperience);
            var StdEdu = _Mapper.Map<StudentEducation>(entity.StudentEducation);
            //var StdDegree = _Mapper.Map<EducationDegree>(entity.StudentDegree);
            var StudentSeeker = _Mapper.Map<StudentSeekers>(entity.StudentPersonal);
            StudentEducation StudentEd = new StudentEducation
            {
                Institution = StdEdu.Institution,
                UpdatedTime = StdEdu.UpdatedTime
            };
            //StudentEd.EducationDegree.Add(StdDegree);
            StudentSeekers Studentdtls = new StudentSeekers();
            Studentdtls = StudentSeeker;
            Studentdtls.StudentExperiences.Add(StudentExperienece);
            Studentdtls.StudentEducations.Add(StudentEd);

            Address addresses = new Address();
            addresses = _Mapper.Map<Address>(entity.StudentAddress);

            //StudentAddressCollection StuAddresscollection = new StudentAddressCollection { Address = addresses, Student = Studentdtls };

            _db.Students.Update(Studentdtls);
            _db.Address.Update(addresses);            
             return await Save();
        }

        public async Task<StudentProfile> FindBystring(string id)
        {
            StudentProfile studentProfile = new StudentProfile();
            var student = _db.Students.AsNoTracking()
                .Include(a => a.StudentExperiences).ThenInclude(a=>a.companycategory)
                .Include(a => a.StudentEducations)
                //.ThenInclude(a => a.EducationDegree)
                .Include(a => a.StudentAddresses).ThenInclude(a => a.Address)
                .Include(a => a.Award)
                .Include(a => a.Projects)
                .Include(a => a.Memberships)
                .Include(a => a.Languages)
                .Include(a => a.Studentportfolio).Include(a => a.StudentWorkAvailability).ThenInclude(a => a.HoursPerWeekOptions).Include(a => a.StudentWorkAvailability).ThenInclude(a => a.DaysPerWeekOptions)
                 .Where(a => a.EmailID == id).FirstOrDefault();

            if(student != null) { 
             studentProfile = _SupportFunctions.ConvertToStudentProfile(student);
            }            
            return studentProfile;
        }
        //private ObjectResult InternalError(string message)
        //{
        //    _Logger.LogError(message);
        //    return StatusCOde(500, "Somethin went wrong. Please contact the administrator.");
        //}

    }
}
