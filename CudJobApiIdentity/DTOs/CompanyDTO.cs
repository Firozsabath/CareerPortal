using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.DTOs
{
    public class CompanyDTO
    {
        public int CompanyID { get; set; }

        public string UserEmailID { get; set; }

        public string CompanyName { get; set; }   
        
        public string Website { get; set; }

        public int CategoryID { get; set; }

        public int SectorID { get; set; }

        public string Certificatepath { get; set; }

        public string profileImgpath { get; set; }

        public int? StatusIDs { get; set; }

        public int? NotesID { get; set; }

        public bool Approved { get; set; }

        public DateTime LicenseExpiryDate { get; set; }
        
        public string CompanyStrength { get; set; }
       
        public string HealthMeasures { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
    public class CompanyContactDTO
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailID { get; set; }

        public string Contact_No { get; set; }

        public DateTime CreateddDate { get; set; }

        public int CompanyID { get; set; }
    }
}
