using CUDJobAPiIdentity.Models;
using CUDJobAPiIdentity.VIewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Contracts
{
    public interface IStudentRepository : IRepositoryBase<StudentProfile>
    {
       // public bool studentprofilecreatetest(studentProfilephase1 data);
    }
}
