using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.ViewModels
{
    public class CompanyOptional
    {
        public int OptionalID { get; set; }

        public int CompanyID { get; set; }

        [Display(Name = "Are you Offering full-time Jobs for fresh graduates")]
        public string Fulltimeoffer { get; set; }

        [Display(Name = "Flexible Hours")]
        public string FlexibleHours_forFulltime { get; set; }

        [Display(Name = "Working From Office")]
        public string Workingfromoffice_forFulltime { get; set; }

        [Display(Name = "Are you Offering Part-time Jobs for fresh graduates")]
        public string Parttimeoffer { get; set; }

        [Display(Name = "Flexible Hours")]
        public string FlexibleHours_forParttime { get; set; }

        [Display(Name = "Working From Office")]
        public string Workingfromoffice_forParttime { get; set; }

        [Display(Name = "Are you Offering Internships:")]
        public string Internshipoffer { get; set; }

        [Display(Name = "Flexible Hours")]
        public string FlexibleHours_forInternship { get; set; }

        [Display(Name = "Working From Office")]
        public string Workingfromoffice_forInternship { get; set; }

        [Display(Name = "Paid")]
        public string PaidInternship { get; set; }

        [Display(Name = "Duration is up to one month")]
        public string Onemonth_Internship { get; set; }

        [Display(Name = "Duration is more than one month")]
        public string Morethan_Onemonth_Internship { get; set; }

        [Display(Name = "Would you like to participate in CUD annual career fair?")]
        public string Partcipate_CUDAnnualcareerfair { get; set; }

        [Display(Name = "Would you like to participate or sponsor CUD events")]
        public string Partcipateorsponsor_CUDEvents { get; set; }

        [Display(Name = "Would you like to offer employability workshops to students")]
        public string Workshops_tostudent { get; set; }

        [Display(Name = "Would you like to be part of CUD Incubator Project to encourage young leaders and entrepreneurs")]
        public string CUD_Incubator_Project { get; set; }

        [Display(Name = "Would you mind sharing your work contact details with 'QS top universities ranking' for an employability survey")]
        public string Share_ContactDetails { get; set; }

        [Display(Name = "Do you cover interns and part-timers in case of work accidents")]
        public string Do_you_cover_incaseof_work_accidents { get; set; }
    }
}
