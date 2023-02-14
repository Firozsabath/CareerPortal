using CUDJobApiIdentity.Contracts;
using CUDJobAPiIdentity.Data;
using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Services
{
    public class BackgroundContracts : IBackgroundContracts
    {
        private readonly ApplicationDbContext _db;

        public BackgroundContracts(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<Companies>> expiringcompanies(int days)
        {
            DateTime ExpiydateBound = DateTime.UtcNow.Date.AddDays(days);
            var companies = _db.Companies.Where(x => x.LicenseExpiryDate == ExpiydateBound).ToList();
            return companies;
        }

        public async Task<List<Jobs>> expiringjobs(int days)
        {
            DateTime ExpiydateBound = DateTime.UtcNow.Date.AddDays(days);
            var jobs = _db.JobModel.Where(x => x.LastApplyDate == ExpiydateBound).ToList();
            return jobs;
        }
    }
}
