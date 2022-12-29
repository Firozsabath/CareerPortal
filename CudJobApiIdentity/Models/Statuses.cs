using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class Statuses
    {
        [Key]
        public int? StatusIDs { get; set; }

        [MaxLength(100)]
        public string MyProperty { get; set; }

        public ICollection<Companies> Companies { get; set; } = new List<Companies>();

        public ICollection<Jobs> Jobs { get; set; } = new List<Jobs>();
    }
}
