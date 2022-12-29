using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Models
{
    public class JobCategory
    {
        [Key]        
        public int JobCategoryId { get; set; }
        [MaxLength(100)]
        public string JobCategoryName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public ICollection<Jobs> Jobs { get; } = new List<Jobs>();
    }
}
