using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class JobOptions
    {
        [Key]
        public int JoboptionID { get; set; }
        //public int JobsID { get; set; }

        [MaxLength(10)]
        [Column(TypeName ="varchar(10)")]
        public string FlexibleHours { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Workfromoffice { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Paid { get; set; }

        [ForeignKey("ID")]
        public virtual Jobs Jobs { get; set; }
    }
}
