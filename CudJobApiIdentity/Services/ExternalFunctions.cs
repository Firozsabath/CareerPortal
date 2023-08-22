using AutoMapper;
using CUDJobApiIdentity.Contracts;
using CUDJobApiIdentity.DTOs;
using CUDJobApiIdentity.Models;
using CUDJobApiIdentity.VIewModels;
using CUDJobAPiIdentity.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Services
{
    public class ExternalFunctions : IExternalFunctions
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ExternalFunctions(ApplicationDbContext db, IMapper Mapper)
        {
            _db = db;
            _mapper = Mapper;
        }
        public string ApplicationCount(int Jobid)
        {
            var recordset = _db.AppliedJobs.Where(e => e.jobID == Jobid).Count();           

            return recordset.ToString();

        }

        public async Task<List<ChartViewModel>> Getcompanyratio()
        {
            List<ChartViewModel> vm = new List<ChartViewModel>();
            var months_ago = DateTime.Now.AddMonths(-10);
            var company =  _db.Companies.Where(x=>x.CreatedDate>=months_ago).ToList();
            //IEnumerable<int> monthGrp1 = company.GroupBy(e => DateTime.Parse(e).Month).Select(e => e.Count());
            var monthGrp = company.GroupBy(s => s.CreatedDate.ToString("MMMM-yy"))
                .OrderBy(monthGroup => DateTime.ParseExact(monthGroup.Key,"MMMM-yy",new CultureInfo("en-US")))  //**Sorts based on the month**
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
            var orderedlist = vm.OrderBy(x => x.DimensionOne).ToList();
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

        public async Task<List<ChartViewModel>> GetJobratio()
        {
            List<ChartViewModel> vm = new List<ChartViewModel>();

            var months_ago = DateTime.Now.AddMonths(-10);
            var students = _db.Companies.Where(x=>x.CreatedDate >= months_ago).ToList();
            //IEnumerable<int> monthGrp1 = company.GroupBy(e => DateTime.Parse(e).Month).Select(e => e.Count());


            var monthGrp = students.GroupBy(s => s.CreatedDate.ToString("MMMM-yy"))
                .OrderBy(monthGroup => DateTime.ParseExact(monthGroup.Key, "MMMM-yy", new CultureInfo("en-US")))
                .Select(monthGroup => new
                {
                    Month = monthGroup.Key,
                    MonthCount = monthGroup.Count()
                }
                );

            foreach (var item in monthGrp)
            {
                vm.Add(new ChartViewModel
                {
                    DimensionOne = item.Month,
                    Quantity = item.MonthCount
                });
            }
            return vm;
        }

        public async Task<List<ChartViewModel>> GetStudentratio()
        {
            List<ChartViewModel> vm = new List<ChartViewModel>();
            var months_ago = DateTime.Now.AddMonths(-10);
            var students = _db.Students.Where(x=>x.CreatedDate>=months_ago).ToList();
            //IEnumerable<int> monthGrp1 = company.GroupBy(e => DateTime.Parse(e).Month).Select(e => e.Count());
            
            var monthGrp = students.GroupBy(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("MMMM-yy") : DateTime.Now.ToString("MMMM-yy"))
                .OrderBy(monthGroup => DateTime.ParseExact(monthGroup.Key, "MMMM-yy", new CultureInfo("en-US")))
                .Select(monthGroup => new
                {
                    Month = monthGroup.Key,
                    MonthCount = monthGroup.Count()
                }
                );

            foreach (var item in monthGrp)
            {
                vm.Add(new ChartViewModel
                {
                    DimensionOne = item.Month,
                    Quantity = item.MonthCount
                });
            }
            return vm;
        }

        public string HiredApplicationCount()
        {
            var recordset = _db.AppliedJobs.Where(e => e.StatusID == 5).Count();

            return recordset.ToString();

        }

        public async Task<List<ChartViewModel>> UserEngagementratio()
        {
            List<ChartViewModel> vm = new List<ChartViewModel>();
            var company = _db.Tbl_Userloginlogs.ToList();            
            var monthGrp = company.GroupBy(s => s.UserType)
                .OrderBy(monthGroup => monthGroup.Key)  //**Sorts based on the month**
                .Select(monthGroup => new
                {
                    User = monthGroup.Key,
                    EngagementCount = monthGroup.Count()
                }
                );

            foreach (var item in monthGrp)
            {
                vm.Add(new ChartViewModel
                {
                    DimensionOne = item.User,
                    Quantity = item.EngagementCount
                });
            }
            var orderedlist = vm.OrderBy(x => x.DimensionOne).ToList();
            return vm;
        }

        public async Task<bool> userLogsIn(Tbl_UserloginlogsDTO data)
        {
            var logs = _mapper.Map<Tbl_Userloginlogs>(data);
            await _db.Tbl_Userloginlogs.AddAsync(logs);
            var chaanges = await _db.SaveChangesAsync();
            return chaanges > 0;
        }
    }
}
