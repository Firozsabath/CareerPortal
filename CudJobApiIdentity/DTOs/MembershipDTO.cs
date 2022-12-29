using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.DTOs
{
    public class MembershipDTO
    {
        public int MembershipID { get; set; }

        public int StudentID { get; set; }

        public string Organisation { get; set; }

        public string Role { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
