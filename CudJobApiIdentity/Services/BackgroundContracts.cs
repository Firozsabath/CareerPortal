using CUDJobApiIdentity.Contracts;
using CUDJobApiIdentity.Models;
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

        public Task<List<ReminderConfig>> ReminderConfigList()
        {
            throw new NotImplementedException();
        }

        public Task<List<Reminders>> ReminderList()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateReminderConfig(ReminderConfig data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateReminders(Reminders data)
        {
            throw new NotImplementedException();
        }
    }
}
