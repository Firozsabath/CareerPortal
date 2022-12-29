using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.DTOs
{
    public class StudentEducationDTO
    {
        public int EducationId { get; set; }
        public int StudentID { get; set; }
        public string Institution { get; set; }
        public string Major { get; set; }

        public string Degree { get; set; }

        public decimal? CurrrentSalary { get; set; }

        public string CertificateDiploma { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public Byte? CompletionPercent { get; set; }
        public DateTime? CreateddDate { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
    //public class StudentDegreeDTO
    //{
    //    public int ID { get; set; }
    //    public string Major { get; set; }
       
    //    public string Degree { get; set; }
        
    //    public decimal? CurrrentSalary { get; set; }
        
    //    public string CertificateDiploma { get; set; }
        
    //    public DateTime? StartDate { get; set; }
        
    //    public DateTime? CompletionDate { get; set; }
        
    //    public Byte? CompletionPercent { get; set; }

    //    public DateTime? CreateddDate { get; set; }

    //    public DateTime? UpdatedTime { get; set; }
    //}
}
