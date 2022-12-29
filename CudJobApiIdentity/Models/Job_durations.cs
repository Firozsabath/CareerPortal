using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class Job_durations
    {
        [Key]
        public int durationID { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Description { get; set; }

        public ICollection<Jobs> Jobs { get; } = new List<Jobs>();
    }
}
