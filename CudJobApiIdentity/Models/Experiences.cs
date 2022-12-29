using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class JobExperiences
    {
        [Key]
        public int ExperienceID { get; set; }

        [MaxLength(100)]
        public string ExperienceType { get; set; }

        public ICollection<Jobs> Jobs { get; set; } = new List<Jobs>();
    }
}
