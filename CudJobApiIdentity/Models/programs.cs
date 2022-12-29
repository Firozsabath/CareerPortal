using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class programs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProgramID { get; set; }

        [MaxLength(100)]
        public string ProgramName { get; set; }

        public ICollection<StudentSeekers> Students { get; set; } = new List<StudentSeekers>();
    }
}
