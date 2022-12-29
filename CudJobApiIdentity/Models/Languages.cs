using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class Languages
    {
        public int ID { get; set; }

        public int StudentID { get; set; }
       
        public int? LanguageID { get; set; }
        
        public int? LevelID { get; set; }

        public DateTime? Createdate { get; set; }

        public DateTime? Updatedate { get; set; }

        [ForeignKey("StudentID")]
        public StudentSeekers StudentSeeker { get; set; }

        [ForeignKey("LevelID")]
        public LanguageLevels LanguageLevels { get; set; }

        [ForeignKey("LanguageID")]
        public LanguageNames LanguageNames { get; set; }
    }

    public class LanguageLevels
    {
        [Key]
        public int LevelID { get; set; }

        public string Levels { get; set; }

        //public Languages Languages { get; set; }
    }

    public class LanguageNames
    {
        [Key]
        public int LanguageID { get; set; }

        public string Language { get; set; }

        //public Languages Languages { get; set; }
    }
}
