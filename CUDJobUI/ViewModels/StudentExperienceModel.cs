using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.ViewModels
{
    public class StudentExperienceModel
    {
        public int Id { get; set; }

        public int StudentID { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Select Country")]
        public int CountryCode { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }

        [Display(Name = "Company Website")]
        public string Companywebsite { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Company Sector")]
        public int? CategoryID { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? updatedDate { get; set; }

        public bool current { get; set; }
        public virtual CompanyCategory companycategory { get; set; }
    }

    public class VolunteerExperience
    {      

        public int VexpId { get; set; }
        public int StudentID { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [Display(Name = "Description")]
        public string JobDescription { get; set; }
        public bool current { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? EndDate { get; set; }

        public DateTime? Createdate { get; set; }
        public DateTime? Updatedate { get; set; }
        
    }
}
