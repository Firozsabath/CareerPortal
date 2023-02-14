using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.ViewModels
{
    public class StudentProfile
    {
        public StudentModel StudentPersonal { get; set; }

        public IList<StudentEducation> StudentEducation { get; set; }

        public IList<StudentExperienceModel> StudentExperience { get; set; }

        public IList<VolunteerExperience> VolunteerExperience { get; set; }

        //public IList<StudentDegree> StudentDegree { get; set; }       

        public Address StudentAddress { get; set; }

        public IList<Awards> Awards { get; set; }

        public IList<Memberships> Memberships { get; set; }

        public IList<Projects> projects { get; set; }

        public IList<Languages> languages { get; set; }

        public IList<StudentPortfolio> Portfolio { get; set; }
        
        public IList<StudentWorkAvailabilityVM> StudentWorkAvailability { get; set; }

        public IList<StudentHardSkills> StudentHardSkills { get; set; }

        public IList<StudentSoftSkills> StudentSoftSkills { get; set; }

        public IList<StudentComputerSkills> StudentComputerSkills { get; set; }

        public DaysPerWeekOptions DaysPerWeek { get; set; }

        public HoursPerWeekOptions HoursPerWeek { get; set; }

        public CompanyCategory ExperienceCompanyCategory { get; set; }

        public programs programs { get; set; }

        //public List<Hardskills> Hardskills { get; set; }

        //public List<Softskills> Softskills { get; set; }

        //public Computerskills Computerskills { get; set; }

        public string Hskills { get; set; }
        public string Sskills { get; set; }
        public string Cskills { get; set; }

    }

    public class StudentPersonalview
    {
        public StudentModel StudentPersonal { get; set; }

        public Address StudentAddress { get; set; }
    }

    public class StudentEduandExp
    {
        public IList<StudentEducation> StudentEducation { get; set; }

        public IList<StudentExperienceModel> StudentExperience { get; set; }

        //public StudentDegree StudentDegree { get; set; }
    }

    public class programs
    {
        public int ProgramID { get; set; }
        
        public string ProgramName { get; set; }
    }

    public class StudentAssets
    {
        public IList<Awards> Awards { get; set; }

        public IList<Memberships> Memberships { get; set; }

        public IList<Projects> projects { get; set; }
    }


    public class studentProfilephase1
    {
        public StudentModel studentprofile { get; set; }
        public IList<StudentEducation> studentEdu { get; set; }
    }
}
