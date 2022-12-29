using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.DTOs
{
    public class AwardsDTO
    {
        public int AwardID { get; set; }

        public int StudentID { get; set; }

        public string AwardName { get; set; }

        public DateTime? AwardDate { get; set; }
    }
}
