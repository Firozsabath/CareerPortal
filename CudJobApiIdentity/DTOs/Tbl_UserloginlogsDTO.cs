using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.DTOs
{
    public class Tbl_UserloginlogsDTO
    {
        public string Username { get; set; }
        public DateTime? LoginDate { get; set; }
        public string UserType { get; set; }
    }
}
