using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class Awards
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AwardID { get; set; }

        public int StudentID { get; set; }

        [MaxLength(300)]
        public string AwardName { get; set; }

        public DateTime? AwardDate { get; set; }

        public DateTime? Createdate { get; set; }

        public DateTime? Updatedate { get; set; }

        [ForeignKey("StudentID")]
        public StudentSeekers StudentSeeker { get; set; }
    }
}
