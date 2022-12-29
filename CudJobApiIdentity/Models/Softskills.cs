using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class Softskills
    {
        [Key]
        public int SskillID { get; set; }
        public string SoftSkills { get; set; }
    }
}
