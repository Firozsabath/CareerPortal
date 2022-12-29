using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.DTOs
{
    public class AppliedJobsDTO
    {
        public int ID { get; set; }
        public int jobID { get; set; }
        public int StudentID { get; set; }
        public int StatusID { get; set; }
        public string Description { get; set; }
        public DateTime AppliedDate { get; set; }
    }
}
