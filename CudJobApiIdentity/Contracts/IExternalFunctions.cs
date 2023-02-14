using CUDJobApiIdentity.DTOs;
using CUDJobApiIdentity.VIewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Contracts
{
    public interface IExternalFunctions
    {
        public string ApplicationCount(int Jobid);
        string HiredApplicationCount();
        Task<DashbaordKeyDetails> GetDashboardDetails();
        Task<List<ChartViewModel>> Getcompanyratio();
        Task<List<ChartViewModel>> GetStudentratio();
        Task<List<ChartViewModel>> GetJobratio();
        Task<bool> userLogsIn(Tbl_UserloginlogsDTO data);
    }
}
