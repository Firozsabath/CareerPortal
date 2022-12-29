using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.Contracts
{
    public interface IStaticEndPoints
    {
        public string EndPoint(string Enp);
        public string Cams_EndPoint(string Enp);

        public string CompanyEndpoints { get; }
        public string AccountEndpoints { get; }
        public string StudentEndpoints { get; }
        public string JobEndpoints { get; }
        public string DatatoformEndpoints { get; }
        public string jobApplicationEndpoints { get; }
        public string StudentPersonalEndpoints { get; }
        public string Cams_Integration { get; }
    }
}
