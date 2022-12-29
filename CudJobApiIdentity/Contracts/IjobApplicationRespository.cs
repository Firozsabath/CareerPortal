using CUDJobApiIdentity.DTOs;
using CUDJobApiIdentity.Models;
using CUDJobApiIdentity.VIewModels;
using CUDJobAPiIdentity.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Contracts
{
    public interface IjobApplicationRespository
    {
       // Task<IList<AppliedJobsDTO>> FindAll();
        Task<IList<JobApplyDetails>> FindById(int id);
        Task<AppliedJobsDTO> Create(AppliedJobsDTO entity);
        Task<bool> isExists(int id);
        Task<bool> isApplied(int id,int stdid);
        
       // Task<bool> Update(T entity);
        //Task<bool> Delete(T entity);
        Task<bool> Save();
    }
}
