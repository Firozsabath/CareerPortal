using CUDJobApiIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.DTOs
{
    public class LanguagesDTO
    {
        public int ID { get; set; }

        public int StudentID { get; set; }

        public int LanguageID { get; set; }

        public int LevelID { get; set; }

        public LanguageLevels LanguageLevels { get; set; }

        public LanguageNames LanguageNames { get; set; }
    }
}
