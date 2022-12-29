using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class Hardskills
    {
        [Key]
        public int HskillID { get; set; }

        public string HardSkills { get; set; }

        public ICollection<StudentSeekers> StudentSeekers { get; } = new List<StudentSeekers>();
    }
}
