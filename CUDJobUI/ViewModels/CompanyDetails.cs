using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.ViewModels
{
    public class CompanyDetails
    {
        public int CompanyID { get; set; }

        public string UserEmailID { get; set; }

        //[Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
       
        public string Website { get; set; }
        [Display(Name = "Company Category")]
        public int CategoryID { get; set; }

        [Display(Name = "Company Sector")]
        public int SectorID { get; set; }

        public string Certificatepath { get; set; }

        public string profileImgpath { get; set; }
        public int? StatusIDs { get; set; }
        public int? NotesID { get; set; }

        public bool Approved { get; set; }

        [Display(Name = "License Expiry Date")]
        public DateTime LicenseExpiryDate { get; set; }

        [Display(Name = "Company Size")]
        public string CompanyStrength { get; set; }

        [Display(Name = "Safety Measures")]
        public string HealthMeasures { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        // public string[] Optionsss = new[] { "Yes", "No" };

    }
    public class CompanyContact
    {
        public int ID { get; set; }

        //[Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

       
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //[Required]
        [Display(Name = "Email ID")]
        public string EmailID { get; set; }

        //[Required]
        [Display(Name = "Contact Number")]
        public string Contact_No { get; set; }

        public DateTime CreateddDate { get; set; } = DateTime.Now;

        public int CompanyID { get; set; }
    }

    public enum Options
    {        
        No,
        Yes
    }

    public enum Options2
    {
        No = 0,
        yes = 1,      
        Sometimes = 2,
        Other = 3
    }

    public enum Options3
    {
        No = 0,
        yes = 1,        
        Maybe = 2
    }

    public class CompanyCategory
    {        
        public string CategoryID { get; set; }

        public string Category { get; set; } 

    }

    public class CompanySectors
    {      
        public string SectorID { get; set; }
       
        public string Category { get; set; }
       
    }

    public class CompanyViewModel
    {
        //public User CompanyUserName { get; set; }
        public CompanyDetails Companies { get; set; }

        public CompanyContact CompanyContacts { get; set; }

        public Address CompanyAddress { get; set; }

        public CompanyOptional CompanyOptional { get; set; }

        public CompanyCategory Companycategory { get; set; }

        public CompanySectors CompanySectors { get; set; }

        public CountryCode Country { get; set; }
    }
}
