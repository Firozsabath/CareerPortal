using CudJobUI.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.Services
{
    public class StaticEndPoints : IStaticEndPoints
    {
        private readonly IConfiguration _config;
        public StaticEndPoints(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string EndPoint(string Enp)
        {
            string Endpoint = _config.GetValue<string>("APIEndpoint");

            string controllerEndpoint = Enp;

            return $"{Endpoint}{controllerEndpoint}";
        }
        string IStaticEndPoints.CompanyEndpoints { get => EndPoint("Company"); }
        string IStaticEndPoints.StudentEndpoints { get => EndPoint("StudentProfile"); }
        string IStaticEndPoints.JobEndpoints { get => EndPoint("Jobs"); }
        string IStaticEndPoints.AccountEndpoints { get => EndPoint("Users"); }
        string IStaticEndPoints.DatatoformEndpoints { get => EndPoint("DatatoForms"); }
        string IStaticEndPoints.jobApplicationEndpoints { get => EndPoint("JobApplication"); }
        string IStaticEndPoints.StudentPersonalEndpoints { get => EndPoint("StudentPerson"); }

        public string Cams_EndPoint(string Enp)
        {
            string Endpoint = _config.GetValue<string>("APIEndpoint_Cams");

            string controllerEndpoint = Enp;

            return $"{Endpoint}{controllerEndpoint}";
        }

        string IStaticEndPoints.Cams_Integration { get => Cams_EndPoint("Student"); }
       
    }
}
