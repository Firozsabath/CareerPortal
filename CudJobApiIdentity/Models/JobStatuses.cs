using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class JobStatuses
    {
        [Key]
        public int StatusID { get; set; }


        [Column(TypeName = "Nvarchar(50)")]
        public string Status { get; set; }

        public ICollection<AppliedJobs> AppliedJobs { get; set; } = new List<AppliedJobs>();
    }
}
