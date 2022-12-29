using AutoMapper;
using CUDJobApiIdentity.Contracts;
using CUDJobApiIdentity.DTOs;
using CUDJobApiIdentity.Models;
using CUDJobApiIdentity.VIewModels;
using CUDJobAPiIdentity.Data;
using CUDJobAPiIdentity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Services
{
    public class jobApplicationRepository : IjobApplicationRespository
    {
        public jobApplicationRepository(ApplicationDbContext db, IMapper Mapper, ISupportFunction supportFunction)
        {
            _db = db;
            _Mapper = Mapper;
            SupportFunction = supportFunction;
        }

        public ApplicationDbContext _db { get; }
        public IMapper _Mapper { get; }
        public ISupportFunction SupportFunction { get; }

        public async Task<AppliedJobsDTO> Create(AppliedJobsDTO entity)
        {
            var AppliedJobs = _Mapper.Map<AppliedJobs>(entity);
            await _db.AppliedJobs.AddAsync(AppliedJobs);
            var isSuccess = await Save();
            if (!isSuccess)
            {
                return entity;
            }
            var response = entity;
            return response;
        }

        public Task<bool> Delete(AppliedJobsDTO entity)
        {
            throw new NotImplementedException();
        }

        //public Task<IList<AppliedJobsDTO>> FindAll()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<IList<JobApplyDetails>> FindById(int id)
        {
            List<AppliedJobs> AppliedJobList = new List<AppliedJobs>();
            AppliedJobList = await _db.AppliedJobs.Include(a=>a.JobStatuses).Include(a=>a.Student).Where(a => a.jobID == id).ToListAsync();
            var Appliedjobs = SupportFunction.ConvertToApplyJobViewModelList(AppliedJobList);
            return Appliedjobs;
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.AppliedJobs.AnyAsync(a => a.ID == id);
        }

        public async Task<bool> isApplied(int id,int stdid)
        {
            return await _db.AppliedJobs.AnyAsync(a => a.jobID == id && a.StudentID == stdid);
        }

        public async Task<bool> Save()
        {
            var chaanges = await _db.SaveChangesAsync();
            return chaanges > 0;
        }

        //public Task<bool> Update(AppliedJobsDTO entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
