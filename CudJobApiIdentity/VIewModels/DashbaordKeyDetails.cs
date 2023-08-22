using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.VIewModels
{
    public class DashbaordKeyDetails
    {
        public int StudentCount { get; set; }
        public int HiredStudentCount { get; set; }
        public int CompanyCount { get; set; }
        public int RejectedCompanyCount { get; set; }
        public int JobCount { get; set; }
        public int ExpiredjobCount { get; set; }

    }

    public class ChartViewModel
    {
        public string? DimensionOne { get; set; }
        public int Quantity { get; set; }
    }

    public class DashboardViewModel
    {
        public DashbaordKeyDetails DboardKeyDetails { get; set; }
        public List<ChartViewModel> CompanyRatio { get; set; }
        public List<ChartViewModel> StudentRatio { get; set; }
        public List<ChartViewModel> JobRatio { get; set; }
        public List<ChartViewModel> UserEngagementRatio { get; set; }
    }
}
