using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class CompanyOptional
    {
        [Key]
        public int OptionalID { get; set; }

        public int CompanyID { get; set; }

        [MaxLength(10)]
        public string Fulltimeoffer { get; set; }

        [MaxLength(10)]
        public string FlexibleHours_forFulltime { get; set; }

        [MaxLength(10)]
        public string Workingfromoffice_forFulltime { get; set; }

        [MaxLength(10)]
        public string Parttimeoffer { get; set; }

        [MaxLength(10)]
        public string FlexibleHours_forParttime { get; set; }

        [MaxLength(10)]
        public string Workingfromoffice_forParttime { get; set; }

        [MaxLength(10)]
        public string Internshipoffer { get; set; }

        [MaxLength(10)]
        public string FlexibleHours_forInternship { get; set; }

        [MaxLength(10)]
        public string Workingfromoffice_forInternship { get; set; }

        [MaxLength(10)]
        public string PaidInternship { get; set; }

        [MaxLength(10)]
        public string Onemonth_Internship { get; set; }

        [MaxLength(10)]
        public string Morethan_Onemonth_Internship { get; set; }

        [MaxLength(10)]
        public string Partcipate_CUDAnnualcareerfair { get; set; }

        [MaxLength(10)]
        public string Partcipateorsponsor_CUDEvents { get; set; }

        [MaxLength(10)]
        public string Workshops_tostudent { get; set; }

        [MaxLength(10)]
        public string CUD_Incubator_Project { get; set; }

        [MaxLength(10)]
        public string Share_ContactDetails { get; set; }

        [MaxLength(10)]
        public string Do_you_cover_incaseof_work_accidents { get; set; }

        [ForeignKey("CompanyID")]
        public Companies Companies { get; set; }

        //[ForeignKey("Fulltimeoffer")]
        //public MultiOptions MultiOptions { get; set; }



    }
}
