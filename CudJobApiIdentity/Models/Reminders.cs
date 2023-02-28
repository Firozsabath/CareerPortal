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
        public DateTime? ScheduledDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string SendStatus { get; set; }
        public string TemplateUrl { get; set; }
        public long ConfigID { get; set; }
        public bool Active { get; set; }

        [ForeignKey("ConfigID")]
        public ReminderConfig ReminderConfig { get; set; }

        public IEnumerable<ReminderLog> ReminderLog { get; set; }
    }

    public class ReminderConfig
    {
        [Key]
        public long ConfigID { get; set; }
        public string ConfigName { get; set; }
        public string PriorDays { get; set; }
        public int Frequency { get; set; }
        public IEnumerable<Reminders> Reminders { get; set; }

    }

    public class ReminderLog
    {
        [Key]
        public long ID { get; set; }
        public long ReminderID { get; set; }
        public string SendTO { get; set; }
        public DateTime? InsertedTime { get; set; }
        public bool SendStatus { get; set; }

        [ForeignKey("ReminderID")]
        public Reminders Reminders { get; set; }
    }
}
