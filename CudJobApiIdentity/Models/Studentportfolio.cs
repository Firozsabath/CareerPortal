using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class Studentportfolio
    {
        [Key]
        public int PortfolioID { get; set; }

        public int StudentID { get; set; }

        [MaxLength(300)]
        public string portfolio { get; set; }

        public DateTime? Createdate { get; set; }

        public DateTime? Updatedate { get; set; }

        [ForeignKey("StudentID")]
        public virtual StudentSeekers StudentSeeker { get; set; }
    }
}
