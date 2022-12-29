using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]      

        public int AddressID { get; set; }             

        public int AddressTypeID { get; set; }


        [Column(TypeName = "Nvarchar(100)")]
        public string Address1 { get; set; }


        [Column(TypeName = "Nvarchar(50)")]
        public string City { get; set; }


        [Column(TypeName = "Nvarchar(50)")]
        public string State { get; set; }

        public int CountryID { get; set; }

        public int? Nationality { get; set; }

        public int PinCode { get; set; }

        public DateTime CreateddDate { get; set; }

        public DateTime UpdatedTime { get; set; }

        [ForeignKey("CountryID")]
        //[InverseProperty("CountryIDAddress")]
        public virtual CountryCode CountryCode { get; set; }

        [ForeignKey("Nationality")]
        //[InverseProperty("NationalityAddress")]
        public virtual CountryCode NationalityCode { get; set; }

        public ICollection<CompanyAddressCollection> CompanyAddresses { get; set; } = new List<CompanyAddressCollection>();

    }
}
