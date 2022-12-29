using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.ViewModels
{
    public class Address
    {
        public int AddressID { get; set; }

        public int AddressTypeID { get; set; }
        [Display(Name = "Address")]
        public string Address1 { get; set; }
        
        public string City { get; set; }
        
        public string State { get; set; }
        [Display(Name = "Country of Residence")]
        public int CountryID { get; set; }

        public int PinCode { get; set; }

        public DateTime CreateddDate { get; set; }

        public DateTime UpdatedTime { get; set; }

        public virtual CountryCode CountryCode { get; set; }
    }
}
