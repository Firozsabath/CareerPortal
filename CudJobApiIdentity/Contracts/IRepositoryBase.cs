using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IList<T>> FindAll();
        Task<T> FindById(int id);
        Task<T> FindBystring(string id);
        Task<T> Create(T entity);
        Task<bool> isExists(int id);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();

    }
}
