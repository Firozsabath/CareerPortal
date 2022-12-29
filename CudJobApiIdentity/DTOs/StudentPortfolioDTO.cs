using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.DTOs
{
    public class StudentPortfolioDTO
    {
        public int PortfolioID { get; set; }

        public int StudentID { get; set; }

        public string portfolio { get; set; }
        public DateTime? Createdate { get; set; }
        public DateTime? Updatedate { get; set; }
    }
}
