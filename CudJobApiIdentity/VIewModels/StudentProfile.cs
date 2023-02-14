using CUDJobApiIdentity.DTOs;
using CUDJobApiIdentity.Models;
using CUDJobApiIdentity.VIewModels;
using CUDJobAPiIdentity.DTOs;
using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.VIewModels
{
    public class StudentProfile
    {
        public StudentProfileDTO StudentPersonal { get; set; }

        public IList<StudentEducationDTO> StudentEducation { get; set; }

        public IList<StudentExperienceDTO> StudentExperience { get; set; }
        public IList<StudentVolunteerExperienceDTO> VolunteerExperience { get; set; }

        //public IList<StudentDegreeDTO> StudentDegree { get; set; }

        public AddressDTO StudentAddress { get; set; }

        public IList<AwardsDTO> Awards { get; set; }

        public IList<MembershipDTO> Memberships { get; set; }

        public IList<ProjectDTO> projects { get; set; }

        public IList<LanguagesDTO> languages { get; set; }

        public IList<StudentPortfolioDTO> Portfolio { get; set; }

        public IList<StudentWorkAvailability> StudentWorkAvailability { get; set; }

        public IList<StudentHardskillsDTO> StudentHardSkills { get; set; }

        public IList<StudentSoftSkillsDTO> StudentSoftSkills { get; set; }

        public IList<StudentComputerSkillsDTO> StudentComputerSkills { get; set; }

        public DaysPerWeekOptions DaysPerWeek { get; set; }

        public HoursPerWeekOptions HoursPerWeek { get; set; }   

        public CompanyCategory ExperienceCompanyCategory { get; set; }

        public programs programs { get; set; }
        //public Hardskills Hardskills { get; set; }
        //public Softskills Softskills { get; set; }
        //public Computerskills Computerskills { get; set; }

        public string Hskills { get; set; }
        public string Sskills { get; set; }
        public string Cskills { get; set; }
    }

    public class StudentPersonalview
    {
        public StudentProfileDTO StudentPersonal { get; set; }

        public AddressDTO StudentAddress { get; set; }
    }

    public class StudentEduandExp
    {
        public IList<StudentEducation> StudentEducation { get; set; }

        public IList<StudentExperienceDTO> StudentExperience { get; set; }

        //public StudentDegreeDTO StudentDegree { get; set; }
    }

    public class StudentAssets
    {
        public IList<AwardsDTO> Awards { get; set; }

        public IList<MembershipDTO> Memberships { get; set; }

        public IList<ProjectDTO> projects { get; set; }
    }

    public class SkillSetVM
    {
        public int StudentID { get; set; }
        public string SkillID { get; set; }
        public string SkillType { get; set; }
    }

    public class studentProfilephase1
    {
        public StudentProfileDTO studentprofile { get; set; }
        public IList<StudentEducationDTO> studentEdu { get; set; }
    }
}
