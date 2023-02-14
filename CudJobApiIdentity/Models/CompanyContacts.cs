using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class CompanyContacts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "Nvarchar(50)")]
        public string FirstName { get; set; }

        [Column(TypeName = "Nvarchar(50)")]
        public string LastName { get; set; }

        [Column(TypeName = "Nvarchar(50)")]
        public string EmailID { get; set; }

        [Column(TypeName = "Nvarchar(50)")]
        public string Contact_No { get; set; }

        public string ContactPosition { get; set; }
        
        public DateTime CreateddDate { get; set; }

        public DateTime UpdatedTime { get; set; }

        public int CompanyID { get; set; }

        [ForeignKey("CompanyID")]
        public virtual Companies Companies { get; set; }
    }
}
