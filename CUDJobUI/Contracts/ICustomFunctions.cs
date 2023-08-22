using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.Contracts
{
    public interface ICustomFunctions
    {
        public string Converttolastupdated(DateTime updateddate);
        public string Lastupdated(DateTime startdate, DateTime enddate);
        public double TotalPercent(int Outof, int Total);
        Task<bool> isStudentCreated(string EmailID);
        Task<bool> setStudentState(int id);
    }
}
