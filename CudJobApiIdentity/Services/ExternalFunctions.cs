using CUDJobApiIdentity.Contracts;
using CUDJobApiIdentity.DTOs;
using CUDJobApiIdentity.VIewModels;
using CUDJobAPiIdentity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Services
{
    public class ExternalFunctions : IExternalFunctions
    {
        private readonly ApplicationDbContext _db;
        public ExternalFunctions(ApplicationDbContext db)
        {
            _db = db;
        }
        public string ApplicationCount(int Jobid)
        {
            var recordset = _db.AppliedJobs.Where(e => e.jobID == Jobid).Count();           

            return recordset.ToString();

        }

        public async Task<List<ChartViewModel>> Getcompanyratio()
        {
            List<ChartViewModel> vm = new List<ChartViewModel>();
            var company =  _db.Companies.ToList();
            //IEnumerable<int> monthGrp1 = company.GroupBy(e => DateTime.Parse(e).Month).Select(e => e.Count());
            var monthGrp = company.GroupBy(s => s.CreatedDate.ToString("MMMM"))
                .OrderBy(monthGroup => monthGroup.Key)    
                .Select(monthGroup => new
                {
                    Month = monthGroup.Key,
                    MonthCount = monthGroup.Count()
                }               
                );
            
            foreach(var item in monthGrp)
            {
                vm.Add(new ChartViewModel { 
                    DimensionOne = item.Month,
                    Quantity = item.MonthCount
                });
            }
            return vm;
        }

        public async Task<DashbaordKeyDetails> GetDashboardDetails()
        {
            DashbaordKeyDetails DBDetails = new DashbaordKeyDetails();
            DBDetails.CompanyCount = _db.Companies.Count();
            DBDetails.RejectedCompanyCount = _db.Companies.Where(c=>c.StatusIDs==3).Count();
            DBDetails.JobCount = _db.JobModel.Count();
            DBDetails.ExpiredjobCount = _db.JobModel.Where(x => x.LastApplyDate < DateTime.Now).Count();
            DBDetails.StudentCount = _db.Students.Count();
            DBDetails.HiredStudentCount = _db.AppliedJobs.Where(e => e.StatusID == 5 || e.StatusID == 7).Count();
            return DBDetails;
        }

        public string HiredApplicationCount()
        {
            var recordset = _db.AppliedJobs.Where(e => e.StatusID == 5).Count();

            return recordset.ToString();

        }

    }
}
