using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.ViewModels
{
    public class StudentEducation
    {
        public int EducationId { get; set; }

        public int StudentID { get; set; }

        [Display(Name = "College/University Name")]
        public string Institution { get; set; }
        
        public string Major { get; set; }
        
        public string Degree { get; set; }

        [Display(Name = "Currrent Salary")]
        public decimal? CurrrentSalary { get; set; }

        public string CertificateDiploma { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Completion Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? CompletionDate { get; set; }

        [Display(Name = "Completion Percent")]
        public Byte? CompletionPercent { get; set; }

        public DateTime? CreateddDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedTime { get; set; } = DateTime.Now;

        public bool current { get; set; }
    }
    //public class StudentDegree
    //{
    //    public int ID { get; set; }
    //    //[Required]
    //    public string Major { get; set; }
    //    //[Required]
    //    public string Degree { get; set; }
    //    public decimal? CurrrentSalary { get; set; }
    //    public string CertificateDiploma { get; set; }
    //    public DateTime? StartDate { get; set; }
    //    public DateTime? CompletionDate { get; set; }
    //    public Byte? CompletionPercent { get; set; }
    //    public DateTime? CreateddDate { get; set; }
    //    public DateTime? UpdatedTime { get; set; }
   // }
}
