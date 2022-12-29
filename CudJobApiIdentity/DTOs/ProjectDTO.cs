using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.DTOs
{
    public class ProjectDTO
    {
        public int PorjectID { get; set; }
        public int StudentID { get; set; }
        public string Tittle { get; set; }
        public string Organisation { get; set; }
        public string Description { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public string Role { get; set; }
        public string Outcome { get; set; }
        public string Links { get; set; }
        public string Attachment { get; set; }
    }
}
