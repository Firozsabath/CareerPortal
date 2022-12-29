using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.DTOs
{
    public class CompanyOptionalDTO
    {
        public int OptionalID { get; set; }

        public int CompanyID { get; set; }

        public string Fulltimeoffer { get; set; }

        public string FlexibleHours_forFulltime { get; set; }

        public string Workingfromoffice_forFulltime { get; set; }

        public string Parttimeoffer { get; set; }

        public string FlexibleHours_forParttime { get; set; }

        public string Workingfromoffice_forParttime { get; set; }

        public string Internshipoffer { get; set; }

        public string FlexibleHours_forInternship { get; set; }

        public string Workingfromoffice_forInternship { get; set; }

        public string PaidInternship { get; set; }

        public string Onemonth_Internship { get; set; }

        public string Morethan_Onemonth_Internship { get; set; }

        public string Partcipate_CUDAnnualcareerfair { get; set; }

        public string Partcipateorsponsor_CUDEvents { get; set; }

        public string Workshops_tostudent { get; set; }

        public string CUD_Incubator_Project { get; set; }

        public string Share_ContactDetails { get; set; }

        public string Do_you_cover_incaseof_work_accidents { get; set; }
    }
}
