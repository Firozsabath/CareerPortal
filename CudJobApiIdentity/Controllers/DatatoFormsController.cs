using CUDJobApiIdentity.Contracts;
using CUDJobApiIdentity.VIewModels;
using CUDJobAPiIdentity.Contracts;
using CUDJobAPiIdentity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatatoFormsController : ControllerBase
    {
        public readonly ILoggerService _Logger;
        public readonly ApplicationDbContext _db;
        private readonly IExternalFunctions _externalFunctions;

        public DatatoFormsController(ILoggerService Logger, ApplicationDbContext db, IExternalFunctions externalFunctions)
        {
            _Logger = Logger;
            _db = db;
            _externalFunctions = externalFunctions;
        }

        [HttpGet("GetCountry")]
        public async Task<IActionResult> GetCountry()
        {
            //var response = 0;
            try
            {
                var response = await _db.CountryCode.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
            
        }

        [HttpGet("GetCompanyList")]
        public async Task<IActionResult> GetCompanyList()
        {
            //var response = 0;
            try
            {
                var response = await _db.Companies.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpGet("GetJobType")]
        public async Task<IActionResult> GetJobType()
        {
            //var response = 0;
            try
            {
                var response = await _db.Jobtypes.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpGet("GetJobDurations")]
        public async Task<IActionResult> GetJobDurations()
        {
            //var response = 0;
            try
            {
                var response = await _db.Job_durations.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }


        [HttpGet("GetCompanyCategory")]
        public async Task<IActionResult> GetCompanyCategory()
        {
            //var response = 0;
            try
            {
                var response = await _db.CompanyCategory.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpGet("GetDaysPerWeek")]
        public async Task<IActionResult> GetDaysPerWeek()
        {
            //var response = 0;
            try
            {
                var response = await _db.DaysPerWeekOptions.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpGet("GetHoursPerWeek")]
        public async Task<IActionResult> GetHoursPerWeek()
        {
            //var response = 0;
            try
            {
                var response = await _db.HoursPerWeekOptions.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpGet("GetJobCategory")]
        public async Task<IActionResult> GetJobCategory()
        {
            //var response = 0;
            try
            {
                var response = await _db.JobCategory.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpGet("GetJobExperience")]
        public async Task<IActionResult> GetJobExperience()
        {
            //var response = 0;
            try
            {
                var response = await _db.JobExperiences.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpGet("GetJobStatuses")]
        public async Task<IActionResult> GetJobStatuses()
        {
            //var response = 0;
            try
            {
                var response = await _db.JobStatuses.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpGet("GetStatuses")]
        public async Task<IActionResult> GetStatuses()
        {
            //var response = 0;
            try
            {
                var response = await _db.Statuses.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpGet("GetCompanySectors")]
        public async Task<IActionResult> GetCompanySectors()
        {
            //var response = 0;
            try
            {
                var response = await _db.CompanySectors.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpGet("GetPrograms")]
        public async Task<IActionResult> GetPrograms()
        {
            //var response = 0;
            try
            {
                var response = await _db.programs.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpGet("GetHardSkills")]
        public async Task<IActionResult> GetHardSkills()
        {
            //var response = 0;
            try
            {
                var response = await _db.Hardskills.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpGet("GetSoftSkills")]
        public async Task<IActionResult> GetSoftSkills()
        {
            //var response = 0;
            try
            {
                var response = await _db.Softskills.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpGet("GetComputerSkills")]
        public async Task<IActionResult> GetComputerSkills()
        {
            //var response = 0;
            try
            {
                var response = await _db.Computerskills.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpGet("GetLanguages")]
        public async Task<IActionResult> GetLanguages()
        {
            //var response = 0;
            try
            {
                var response = await _db.LanguageNames.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpGet("GetLanguagelevels")]
        public async Task<IActionResult> GetLanguagelevels()
        {
            //var response = 0;
            try
            {
                var response = await _db.LanguageLevels.ToListAsync();
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpGet("Getdashboarddetails")]
        public async Task<IActionResult> Getdashboarddetails()
        {
            //var response = 0;
            try
            {
                var Companyratio = await _externalFunctions.Getcompanyratio();
                var dboardDetails = await _externalFunctions.GetDashboardDetails();

                var dashboardviewModel = new DashboardViewModel { 
                    CompanyRatio = Companyratio,
                    DboardKeyDetails = dboardDetails
                };

                return Ok(dashboardviewModel);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        private ObjectResult InternalError(string message)
        {
            _Logger.LogError(message);
            return StatusCode(500, "Somethin went wrong. Please contact the administrator.");
        }
    }
}
