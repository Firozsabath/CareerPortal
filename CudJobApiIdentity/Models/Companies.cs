using CUDJobApiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Models
{
    public class Companies
    {
        public Companies()
        {
            this.Jobs = new HashSet<Jobs>();
            this.CompanyContacts = new HashSet<CompanyContacts>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyID { get; set; }

        [MaxLength(100)]
        public string UserEmailID { get; set; }

        [Column(TypeName = "Nvarchar(100)")]
        public string CompanyName { get; set; }
        
        [Column(TypeName = "Nvarchar(100)")]
        public string Website { get; set; }

        public int CategoryID { get; set; }

        public int SectorID { get; set; }

        [MaxLength(200)]
        public string Certificatepath { get; set; }

        [MaxLength(200)]
        public string profileImgpath { get; set; }

        public bool Approved { get; set; } = false;

        public DateTime LicenseExpiryDate { get; set; }

        [Column(TypeName = "Nvarchar(50)")]
        public string CompanyStrength { get; set; }

        [Column(TypeName = "Nvarchar(200)")]
        public string HealthMeasures { get; set; }

        public int? StatusIDs { get; set; }

        public int? NotesID { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [ForeignKey("CategoryID")]       
        public virtual CompanyCategory companycategory { get; set; }

        [ForeignKey("SectorID")]
        public virtual CompanySectors CompanySectors { get; set; }

        [ForeignKey("StatusIDs")]
        public virtual Statuses Statuses { get; set; }

        [ForeignKey("NotesID")] 
        public StatusesNotes StatusesNotes { get; set; }

        public CompanyOptional CompanyOptional { get; set; }

        public ICollection<CompanyContacts> CompanyContacts { get; set; } = new List<CompanyContacts>();

        public ICollection<CompanyAddressCollection> addresses { get; set; } = new List<CompanyAddressCollection>();

        public ICollection<CompanyPortfolio> CompanyPortfolio { get; set; }

        public ICollection<Jobs> Jobs { get; set; } = new List<Jobs>();

       // public ICollection<CompanyPortfolio> CompanyPortfolios { get; set; } = new List<CompanyPortfolio>();
    }
}
