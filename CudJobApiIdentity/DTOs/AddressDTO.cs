using CUDJobApiIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.DTOs
{
    public class AddressDTO
    {
        public int AddressID { get; set; }

        public int AddressTypeID { get; set; }

        public string Address1 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int CountryID { get; set; }

        public int PinCode { get; set; }

        public DateTime CreateddDate { get; set; }

        public DateTime UpdatedTime { get; set; }

        public virtual CountryCode CountryCode { get; set; }
    }
}
