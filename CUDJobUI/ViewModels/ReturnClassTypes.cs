using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.ViewModels
{
    public class ReturnClassTypes
    {
    }

    public class Companydt
    {
        public int CompanyID { get; set; }
    }

    public class Studentdt
    {
        public int StudentID { get; set; }
    }

    public class ErroList
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
