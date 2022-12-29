using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.DTOs
{
    public class EmailDTO
    {
        public string Recepient { get; set; }
        public string Subject { get; set; }
        public string CompanyName { get; set; }
        public string PostionName { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

    }
}
