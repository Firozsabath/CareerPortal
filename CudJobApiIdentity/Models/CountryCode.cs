using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class CountryCode
    {
        [Key]
        public int CountryID { get; set; }

        //public int CountrycudID { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public ICollection<Address> address { get; set; } = new List<Address>();

        public ICollection<Address> Nationalityaddress { get; set; } = new List<Address>();
    }
}
