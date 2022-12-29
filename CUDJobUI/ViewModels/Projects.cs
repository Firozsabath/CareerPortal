using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.ViewModels
{
    public class Projects
    {
        public int PorjectID { get; set; }

        public int StudentID { get; set; }

        public string Tittle { get; set; }

        public string Organisation { get; set; }

        public string Description { get; set; }

        [Display(Name ="Start Date")]
        public DateTime? Startdate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? Enddate { get; set; }

        public string Role { get; set; }

        [Display(Name = "Out Come")]
        public string Outcome { get; set; }

        public string Links { get; set; }

        public string Attachment { get; set; }
    }

    public class Memberships
    {
        public int MembershipID { get; set; }

        public int StudentID { get; set; }

        public string Organisation { get; set; }

        public string Role { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? EndDate { get; set; }
    }

    public class Awards
    {
        public int AwardID { get; set; }

        public int StudentID { get; set; }

        [Display(Name = "Award Name")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public string AwardName { get; set; }

        [Display(Name = "Award Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? AwardDate { get; set; }
    }

    public class Languages
    {
        public int ID { get; set; }        
        public int StudentID { get; set; }

        [Display(Name = "Language")]
        public int? LanguageID { get; set; }
        [Display(Name = "Level")]
        public int? LevelID { get; set; }
        public virtual LanguageNames LanguageNames { get; set; }
        public virtual LanguageLevels LanguageLevels { get; set; }
    }

    public class LanguageNames
    {   
      
        public int LanguageID { get; set; }

        public string Language { get; set; }

    }

    public class LanguageLevels
    {
        
        public int LevelID { get; set; }

        public string Levels { get; set; }

    }
}
