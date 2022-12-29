using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Models
{
    public class StudentResumes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int StudentID { get; set; }
        public Byte Resumes { get; set; }
        public DateTime? Created_date { get; set; }

        [ForeignKey("StudentID")]
        public virtual StudentSeekers StudentSeeker { get; set; }
    }
}
