using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Models
{
    public class JobSkillset
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
       // public int JobId { get; set; }
        [Column(TypeName = "Nvarchar(50)")]
        public string Skill { get; set; }
        public int Skilllevel { get; set; }
    }
}
