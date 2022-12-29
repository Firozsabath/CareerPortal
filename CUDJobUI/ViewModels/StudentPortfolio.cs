using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.ViewModels
{
    public class StudentPortfolio
    {
        public int PortfolioID { get; set; }

        public int StudentID { get; set; }

        public string portfolio { get; set; }
        public DateTime? Createddate { get; set; }
        public DateTime? Updatedate { get; set; }
    }

    public enum Gender
    {
        Male = 1 ,
        FeMale = 2
    }
}
