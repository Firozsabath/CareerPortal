using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.ViewModels
{
    public class Statuses
    {       
        public int? StatusIDs { get; set; }

        public string MyProperty { get; set; }
    }

    public class StatusesNotes
    {
        public int? NotesID { get; set; }

        public string Notes { get; set; }

        public int StatusID { get; set; }
    }
}
