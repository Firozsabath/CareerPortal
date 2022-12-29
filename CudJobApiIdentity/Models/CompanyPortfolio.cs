using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class CompanyPortfolio
    {
        [Key]
        public int PortfolioID { get; set; }

        public int CompanyID { get; set; }

        [MaxLength(200)]
        public string portfolio { get; set; }

        [ForeignKey("CompanyID")]
        public virtual Companies Companies { get; set; }
    }
}
