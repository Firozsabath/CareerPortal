using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class Jobtypes
    {
        [Key]
        public int JobTypeID { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "Varchar(50)")]
        public string Description { get; set; }

        public ICollection<Jobs> Jobs { get; } = new List<Jobs>();
    }
}
