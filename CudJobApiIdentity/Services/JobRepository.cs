using CUDJobAPiIdentity.Contracts;
using CUDJobAPiIdentity.Data;
using CUDJobAPiIdentity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Services
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext _db;
        public JobRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Jobs> Create(Jobs entity)
        {
            Jobs Job = entity;
            Job.StatusesNotes = new Models.StatusesNotes();
            await _db.JobModel.AddAsync(Job);
            var isSuccess = await Save();
            if(!isSuccess)
            {
                return entity;
            }
            var response = entity;
            return response;
        }

        public async Task<bool> Delete(Jobs entity)
        {
            _db.JobModel.Remove(entity);
            return await Save();
        }

        public async Task<IList<Jobs>> FindAll()
        {
            var Jobs = await _db.JobModel
                .Include(e=>e.jobcategories)
                .Include(e => e.Experiences)
                .Include(e => e.Companies.CompanyContacts).Include(e=>e.JobOptions).Include(e => e.Jobtypes).Include(e=>e.Statuses)
                .Include(e=>e.Companies.addresses).ThenInclude(e=>e.Address)
                .Include(a => a.JobsWorkAvailability).ThenInclude(a => a.HoursPerWeek).Include(a => a.JobsWorkAvailability).ThenInclude(a => a.DaysPerWeek)
                .OrderByDescending(a=>a.UpdatedDate).ToListAsync();
            return Jobs;
        }

        public async Task<Jobs> FindById(int id)
        {
            var job = _db.JobModel
                .Include(e => e.jobcategories)
                .Include(e => e.Experiences)
                .Include(e => e.Companies.CompanyContacts).Include(e => e.JobOptions).Include(e => e.Jobtypes).Include(e => e.Job_durations)
                .Include(a=>a.Companies.addresses).ThenInclude(a=>a.Address)
                .Include(a => a.JobsWorkAvailability).ThenInclude(a => a.HoursPerWeek).Include(a => a.JobsWorkAvailability).ThenInclude(a => a.DaysPerWeek)
                .Include(a=>a.StatusesNotes)
                .Where(a => a.Id == id).FirstOrDefault();
            return job;
        }

        public Task<Jobs> FindBystring(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.JobModel.AnyAsync(o => o.Id == id);
        }

        public async Task<List<Jobs>> Jobliststudent()
        {
            var Jobs = await _db.JobModel
               .Include(e => e.jobcategories)
               .Include(e => e.Experiences)
               .Include(e => e.Companies.CompanyContacts).Include(e => e.JobOptions).Include(e => e.Jobtypes).Include(e => e.Statuses)
               .Include(e => e.Companies.addresses).ThenInclude(e => e.Address)
               .Include(a => a.JobsWorkAvailability).ThenInclude(a => a.HoursPerWeek).Include(a => a.JobsWorkAvailability).ThenInclude(a => a.DaysPerWeek)
               .Where(a => a.StatusIDs == 2 && a.LastApplyDate >= DateTime.Now && a.Companies.LicenseExpiryDate >= DateTime.Now).OrderByDescending(a => a.UpdatedDate).ToListAsync();
            return Jobs;
        }

        public async Task<List<Jobs>> LatestJoblist()
        {
            var Jobs = await _db.JobModel
               .Include(e => e.jobcategories)
               .Include(e => e.Experiences)
               .Include(e => e.Companies.CompanyContacts).Include(e => e.JobOptions).Include(e => e.Jobtypes).Include(e => e.Statuses)
               .Include(e => e.Companies.addresses).ThenInclude(e => e.Address)
               .Include(a => a.JobsWorkAvailability).ThenInclude(a => a.HoursPerWeek).Include(a => a.JobsWorkAvailability).ThenInclude(a => a.DaysPerWeek)
               .Where(a => a.StatusIDs == 2 && a.LastApplyDate >= DateTime.Now && a.Companies.LicenseExpiryDate >= DateTime.Now).OrderByDescending(a => a.UpdatedDate).Take(5).ToListAsync();
            return Jobs;
        }

        public async Task<bool> Save()
        {
            var chaanges = await _db.SaveChangesAsync();
            return chaanges > 0;
        }

        public async Task<bool> Update(Jobs entity)
        {
            _db.JobModel.Update(entity);
            return await Save();
        }
       
    }
}
