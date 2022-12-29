using CudJobUI.Contracts;
using CudJobUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CudJobUI.Controllers
{
    public class AdminController : Controller
    {

        public AdminController(IStaticEndPoints endPoints, ICustomFunctions customFunctions)
        {
            _endPoints = endPoints;
            _customFunctions = customFunctions;
        }

        public IStaticEndPoints _endPoints { get; }
        public ICustomFunctions _customFunctions { get; }

        public async Task<IActionResult> Index()
        {
            DashboardViewModel details = new DashboardViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/Getdashboarddetails"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    details = JsonConvert.DeserializeObject<DashboardViewModel>(apiResponse);
                }
            }
            ViewBag.rejectedcompanypercent = _customFunctions.TotalPercent(Convert.ToInt32(details.DboardKeyDetails.RejectedCompanyCount), Convert.ToInt32(details.DboardKeyDetails.CompanyCount));
            ViewBag.ExperiredJobPercent = _customFunctions.TotalPercent(Convert.ToInt32(details.DboardKeyDetails.ExpiredjobCount), Convert.ToInt32(details.DboardKeyDetails.JobCount));
            ViewBag.HiredStudentPercent = _customFunctions.TotalPercent(Convert.ToInt32(details.DboardKeyDetails.HiredStudentCount), Convert.ToInt32(details.DboardKeyDetails.StudentCount));
            return View(details);
        }
    }
}
