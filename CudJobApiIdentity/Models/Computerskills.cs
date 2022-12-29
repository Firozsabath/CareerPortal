using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class Computerskills
    {
        [Key]
        public int CskillID { get; set; }
        public string SoftSkills { get; set; }
    }
}
