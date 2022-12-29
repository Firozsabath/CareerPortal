using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class AppliedJobs
    {
        public int ID { get; set; }
        public int jobID { get; set; }
        public int StudentID { get; set; }
        public int StatusID { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }
        public DateTime AppliedDate { get; set; }

        [ForeignKey("StudentID")]
        public virtual StudentSeekers Student { get; set; }

        [ForeignKey("jobID")]
        public virtual Jobs job { get; set; }

        [ForeignKey("StatusID")]
        public virtual JobStatuses JobStatuses { get; set; }
    }
}
