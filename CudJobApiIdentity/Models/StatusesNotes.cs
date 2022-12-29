using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class StatusesNotes
    {
        [Key]
        public int? NotesID { get; set; }

        [MaxLength(2000)]
        public string Notes { get; set; }

        public int StatusID { get; set; }
        
        public ICollection<Companies> Companies { get; } = new List<Companies>();

        public ICollection<Jobs> Jobs { get; } = new List<Jobs>();

    }
}
