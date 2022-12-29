using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class Projects
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PorjectID { get; set; }

        public int StudentID { get; set; }

        [MaxLength(150)]
        public string Tittle { get; set; }

        [MaxLength(150)]
        public string Organisation { get; set; }

        [MaxLength(5000)]
        public string Description { get; set; }

        public DateTime? Startdate { get; set; }

        public DateTime? Enddate { get; set; }

        public bool? current { get; set; }

        [MaxLength(300)]
        public string Role { get; set; }

        [MaxLength(2000)]
        public string Outcome { get; set; }

        [MaxLength(300)]
        public string Links { get; set; }

        [MaxLength(300)]
        public string Attachment { get; set; }

        public DateTime? Createdate { get; set; }

        public DateTime? Updatedate { get; set; }

        [ForeignKey("StudentID")]
        public StudentSeekers StudentSeeker { get; set; }
    }
}
