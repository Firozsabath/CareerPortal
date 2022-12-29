using CUDJobApiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.DTOs
{
    public class StudentExperienceDTO
    {
        public int Id { get; set; }

        public int StudentID { get; set; }
        
        public string CompanyName { get; set; }
        
        public int CountryCode { get; set; }        
        
        public string Location { get; set; }
        
        public string JobTitle { get; set; }

        public string Companywebsite { get; set; } 
        
        public string Department { get; set; }

        public int? CategoryID { get; set; }

        public string JobDescription { get; set; }        
        public DateTime? StartDate { get; set; }        
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual CompanyCategory companycategory { get; set; }
    }

    public class StudentVolunteerExperienceDTO
    {
        public int VexpId { get; set; }
        public int StudentID { get; set; }      
        public string CompanyName { get; set; }        
        public string JobTitle { get; set; }       
        public string JobDescription { get; set; }
        public bool? current { get; set; }       
        public DateTime? StartDate { get; set; }      
        public DateTime? EndDate { get; set; }
        public DateTime? Createdate { get; set; }
        public DateTime? Updatedate { get; set; }
    }
}
