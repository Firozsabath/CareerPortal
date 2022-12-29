using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class Memberships
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MembershipID { get; set; }

        public int StudentID { get; set; }

        [MaxLength(100)]
        public string Organisation { get; set; }

        [MaxLength(2000)]
        public string Role { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? Createdate { get; set; }

        public DateTime? Updatedate { get; set; }

        public bool? current { get; set; }

        [ForeignKey("StudentID")]
        public StudentSeekers StudentSeeker { get; set; }

    }
}
