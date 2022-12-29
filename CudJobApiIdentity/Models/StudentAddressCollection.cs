using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class StudentAddressCollection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int StudentID { get; set; }

        [ForeignKey("StudentID")]
        public virtual StudentSeekers Student { get; set; }        

        public int AddressID { get; set; }

        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; }
    }
}
