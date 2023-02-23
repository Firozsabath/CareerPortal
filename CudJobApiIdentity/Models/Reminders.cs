using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Models
{
    public class Reminders
    {
        [Key]
        public long ID { get; set; }
        public string Subject { get; set; }
        public string SendersEmail { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string SendStatus { get; set; }
        public string TemplateUrl { get; set; }
        public long ConfigID { get; set; }

        [ForeignKey("ConfigID")]
        public ReminderConfig ReminderConfig { get; set; }
    }

    public class ReminderConfig
    {
        [Key]
        public long ConfigID { get; set; }
        public string ConfigName { get; set; }
        public int Frequency { get; set; }

        public IEnumerable<Reminders> Reminders { get; set; }


    }
}
