using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Contracts
{
    public interface IJobRepository : IRepositoryBase<Jobs>
    {
        Task<List<Jobs>> Jobliststudent();
        Task<List<Jobs>> LatestJoblist();
    }
}
