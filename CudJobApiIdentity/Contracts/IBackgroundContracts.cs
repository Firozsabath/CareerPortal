using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Contracts
{
    public interface IBackgroundContracts
    {
        Task<List<Companies>> expiringcompanies(int days);
        Task<List<Jobs>> expiringjobs(int days);
    }
}
