using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class CompanyCategory
    {
        [Key]
        public int CategoryID { get; set; }

        [MaxLength(100)]
        public string Category { get; set; }

        public ICollection<Companies> Companies { get; set; } = new List<Companies>();


    }
}
