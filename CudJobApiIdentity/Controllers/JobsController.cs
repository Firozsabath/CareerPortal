using AutoMapper;
using AutoMapper.Configuration;
using CudJobApiIdentity.Data.Migrations;
using CUDJobApiIdentity.Contracts;
using CUDJobApiIdentity.DTOs;
using CUDJobApiIdentity.Models;
using CUDJobApiIdentity.VIewModels;
using CUDJobAPiIdentity.Contracts;
using CUDJobAPiIdentity.Data;
using CUDJobAPiIdentity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CUDJobApiIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {       
        private readonly ILoggerService _Logger;
        private readonly IJobRepository _Jobrep;
        private readonly IMapper _Mapper;
        private readonly ISupportFunction _supportFunction;
        private readonly INotesRepository _statusnotes;
        private readonly IExternalFunctions _externalFunctions;
        private readonly ApplicationDbContext _db;
        private readonly IEmailConfig _emailconfig;
        public JobsController(ILoggerService logger, IJobRepository Jobrep, IMapper Mapper, ISupportFunction supportFunction, INotesRepository statusnotes, ApplicationDbContext db,IExternalFunctions externalFunctions,IEmailConfig email)
        {
            _Logger = logger;
            _Jobrep = Jobrep;
            _Mapper = Mapper;
            _supportFunction = supportFunction;
            _statusnotes = statusnotes;
            _db = db;
            _emailconfig = email;
            _externalFunctions = externalFunctions;
        }
        // GET: api/<JobsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetJobs()
        {
            try
            {                
                var Jobs = await _Jobrep.FindAll();
                var response = _supportFunction.ConvertJobDetailsList(Jobs);
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpGet("GetAllJobsforStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllJobsforStudent()
        {
            try
            {
                var Jobs = await _db.JobModel
                .Include(e => e.jobcategories)
                .Include(e => e.Experiences)
                .Include(e => e.Companies.CompanyContacts).Include(e => e.JobOptions).Include(e => e.Jobtypes).Include(e => e.Statuses)
                .Include(e => e.Companies.addresses).ThenInclude(e => e.Address)
                .Include(a => a.JobsWorkAvailability).ThenInclude(a => a.HoursPerWeek).Include(a => a.JobsWorkAvailability).ThenInclude(a => a.DaysPerWeek)
                .Where(a=> a.StatusIDs == 2 && a.LastApplyDate >= DateTime.Now && a.Companies.LicenseExpiryDate >= DateTime.Now)
                .OrderByDescending(a => a.UpdatedDate).ToListAsync();
                var response = _supportFunction.ConvertJobDetailsList(Jobs);
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpGet("GetJobsforApproval")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetJobsforApproval()
        {
            try
            {
                var Jobs = await _db.JobModel
                    .Include(e => e.jobcategories)
                    .Include(e => e.Companies.CompanyContacts)
                    .Include(a => a.Companies.addresses).ThenInclude(a => a.Address)
                    .Include(a => a.StatusesNotes).Where(a=>a.StatusIDs == 1 || a.StatusIDs == null).ToListAsync();             
                var response = _supportFunction.ConvertJobDetailsList(Jobs);
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpGet("GetLatestJobs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLatestJobs()
        {
            try
            {
                var Jobs = await _Jobrep.LatestJoblist();
                var response = _supportFunction.ConvertJobDetailsList(Jobs);
                
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        // GET api/<JobsController>/5
        [HttpGet("GetJob/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetJob(int id)
        {
            try
            {                
                var Jobs = await _Jobrep.FindById(id);
                var response = _supportFunction.ConvertJobDetais(Jobs);
                if (response == null)
                {
                    _Logger.LogInfo($"The Company with id:{id} couldn't be find.");
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpGet("JobSearch/{Search}/{Exp}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> JobSearch(string Search,string Exp)
        {
            int Experience = Convert.ToInt32(Exp);
            try
            {
                List<Jobs> Jobs = new List<Jobs>();

                if (Search != null && Experience != 0)
                { 
                Jobs = await _db.JobModel
                    .Include(a => a.jobcategories)
                     .Include(a => a.Experiences)
                    .Include(a => a.Companies.CompanyContacts)
                    .Include(a=>a.Companies.addresses).ThenInclude(a=>a.Address)
                    .Include(a => a.JobsWorkAvailability).ThenInclude(a => a.HoursPerWeek).Include(a => a.JobsWorkAvailability).ThenInclude(a => a.DaysPerWeek)
                    .Where(x => x.Description.Contains(Search)  || x.Qualification.Contains(Search) || x.Title.Contains(Search)).ToListAsync();

                    Jobs = Jobs.Where(x => x.ExperienceID == Experience && x.StatusIDs == 2 && x.LastApplyDate >= DateTime.Now).ToList();
                 
                }
              
                else if (Experience == 0)
                {
                     Jobs = await _db.JobModel
                    .Include(a => a.jobcategories)
                     .Include(a => a.Experiences)
                    .Include(a => a.Companies.CompanyContacts)
                    .Include(a => a.Companies.addresses).ThenInclude(a => a.Address)
                    .Include(a => a.JobsWorkAvailability).ThenInclude(a => a.HoursPerWeek).Include(a => a.JobsWorkAvailability).ThenInclude(a => a.DaysPerWeek)
                    .Where(a => a.Description.Contains(Search) || a.Qualification.Contains(Search) || a.Title.Contains(Search) && a.StatusIDs == 2 && a.LastApplyDate >= DateTime.Now).ToListAsync();
                }         
                
                var response = _supportFunction.ConvertJobDetailsList(Jobs);
                if (response == null)
                {
                    _Logger.LogInfo($"The Job with descriptiom : {Search}, couldn't be find.");
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpGet("JobbyCompany/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetJobbyCompany(int id)
        {
            try
            {

               var Jobs = await _db.JobModel
                    .Include(e => e.jobcategories)
                    .Include(e=>e.Jobtypes)
                    .Include(e=>e.Statuses)
                     .Include(e => e.Experiences)
                    .Include(a => a.Companies.CompanyContacts)
                    .Include(a => a.Companies.addresses).ThenInclude(a => a.Address)
                    .Include(a => a.JobsWorkAvailability).ThenInclude(a => a.HoursPerWeek).Include(a => a.JobsWorkAvailability).ThenInclude(a => a.DaysPerWeek).Where(a => a.CompanyID == id && a.StatusIDs == 2 && a.LastApplyDate >= DateTime.Now).ToListAsync();

               // var Jobs = await _db.JobModel.Include(e => e.jobcategories).Include(a => a.Companies.CompanyContacts).Include(a=>a.Companies.addresses).ThenInclude(a=>a.Address).Where(a => a.CompanyID == id).ToListAsync();
                var response = _supportFunction.ConvertJobDetailsList(Jobs);
                if (response == null)
                {
                    _Logger.LogInfo($"The Company with id:{id} couldn't be find.");
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpGet("JobforCompany/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetJobforCompany(int id)
        {
            try
            {

                var Jobs = await _db.JobModel
                     .Include(e => e.jobcategories)
                     .Include(e => e.Jobtypes)
                     .Include(e => e.Statuses)
                      .Include(e => e.Experiences)
                     .Include(a => a.Companies.CompanyContacts)
                     .Include(a => a.Companies.addresses).ThenInclude(a => a.Address)
                     .Include(a => a.JobsWorkAvailability).ThenInclude(a => a.HoursPerWeek).Include(a => a.JobsWorkAvailability).ThenInclude(a => a.DaysPerWeek).Where(a => a.CompanyID == id).ToListAsync();

                // var Jobs = await _db.JobModel.Include(e => e.jobcategories).Include(a => a.Companies.CompanyContacts).Include(a=>a.Companies.addresses).ThenInclude(a=>a.Address).Where(a => a.CompanyID == id).ToListAsync();
                var response = _supportFunction.ConvertJobDetailsList(Jobs);
                if (response == null)
                {
                    _Logger.LogInfo($"The Company with id:{id} couldn't be find.");
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpGet("JobbyStudent/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetJobbyStudent(int id)
        {
            try
            {
                var Jobs = await _db.AppliedJobs.Include(a=>a.JobStatuses).Include(a=>a.job).ThenInclude(a=>a.Companies).Where(a=>a.StudentID == id).ToListAsync();
                var count = _db.AppliedJobs.Where(o => o.StudentID == id).Count();
                var response = _supportFunction.ConvertToApplyJobViewByStudent(Jobs);
                if (response == null)
                {
                    _Logger.LogInfo($"The Company with id:{id} couldn't be find.");
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }



        // POST api/<JobsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateJob([FromBody] JobDetailsDTO JobDetails)
        {
            string emailid = string.Empty;
            var availability = _Mapper.Map<IList<JobsWorkAvailability>>(JobDetails.JobsWorkAvail);
            var Joboptionsdt = _Mapper.Map<JobOptions>(JobDetails.JobOption);
            Jobs JobsData = new Jobs();
            JobsData = _Mapper.Map<Jobs>(JobDetails);
            foreach(var items in availability)
            {
                JobsData.JobsWorkAvailability.Add(items);
            }
            JobsData.JobOptions = _Mapper.Map<JobOptions>(JobDetails.JobOption);

            try
            {
                if (JobDetails == null)
                {
                    _Logger.LogWarn($"An empty Company Creation request has been send.");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _Logger.LogWarn($"CompanyDetails was incomplete.");
                    return BadRequest(ModelState);
                }
                var isSuccess = await _Jobrep.Create(JobsData);
                if (isSuccess.Id == 0)
                {
                    return InternalError($"Company Creation Failed.");
                }

                var company = _db.Companies.Where(x => x.CompanyID == JobDetails.CompanyID).FirstOrDefault();
                EmailDTO email = new EmailDTO { Recepient = company.UserEmailID, Subject = "New Job Posted", CompanyName = isSuccess.Title, Type = "NewJob", Name = company.CompanyName };
                var emailsend = _emailconfig.SendEmail(email);
                //EmailDTO email1 = new EmailDTO { Recepient = "adminStudentcareers@cud.ac.ae", Subject = "New Job Posted", CompanyName = isSuccess.Title, Type = "NewJob", Name = company.CompanyName };
                //var emailsend1 = _emailconfig.SendEmail(email1);
                return Created("Create", new { JobDetails });
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        // PUT api/<JobsController>/5
        [HttpPut("UpdateJob/{id}")]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] JobDetailsDTO JobDetails)
        {
            try
            {
                if (id < 1 || JobDetails == null)
                {
                    return BadRequest();
                }
                var isExists = await _Jobrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Company with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var availability = _Mapper.Map<IList<JobsWorkAvailability>>(JobDetails.JobsWorkAvail);
                var Joboptionsdt = _Mapper.Map<JobOptions>(JobDetails.JobOption);
                Jobs JobsData = new Jobs();
                JobsData = _Mapper.Map<Jobs>(JobDetails);
                foreach (var items in availability)
                {
                    JobsData.JobsWorkAvailability.Add(items);
                }
                JobsData.JobOptions = _Mapper.Map<JobOptions>(JobDetails.JobOption);

                var isScuccess = await _Jobrep.Update(JobsData);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpPut("Approve/{id}")]
        public async Task<IActionResult> Approve(int id, [FromBody] JobDetailsDTO JobDetails)
        {            
            var Jobs = new Jobs { Id = id, Approved = true };
            _db.JobModel.Attach(Jobs);
            _db.Entry(Jobs).Property(a => a.Approved).IsModified = true;
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("MassApprove/{ids}")]
        public async Task<IActionResult> MassApprove(string ids, [FromBody] JobDetailsDTO companydtls)
        {
            string[] jobids = ids.Split(',');
            int[] myInts = Array.ConvertAll(jobids, s => int.Parse(s));
            try
            {
                if (jobids.Length > 0)
                {
                    foreach(int id in myInts)
                    {
                        var some = _db.JobModel.Where(x => x.Id == id).FirstOrDefault();
                        some.StatusIDs = 2;
                        some.UpdatedDate = DateTime.Now;                                                 
                        _db.JobModel.Update(some);
                        _db.SaveChanges();
                        var s = _db.JobModel.Include(e => e.Companies).Where(a => a.Id == id).FirstOrDefault();
                       var emailsendstatus = _emailconfig.SendEmail_jobapprove(s);
                    }                   
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpPut("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusesNotes notes)
        {
            try
            {
                Jobs Job = new Jobs();
                //var existingEntity = _db.JobModel.Include(a=>a.JobOptions).First(x => x.Id == id);
                var existingEntity = _db.JobModel.First(x => x.Id == id);
                existingEntity.StatusIDs = notes.StatusID;
                existingEntity.UpdatedDate = DateTime.Now;
                Job = _Mapper.Map<Jobs>(existingEntity);
                _db.JobModel.Update(Job);
                _db.SaveChanges();

                
                if (notes.NotesID == 0)
                {
                    return BadRequest();
                }
                var isExists = await _statusnotes.isExists(Convert.ToInt32(notes.NotesID));
                if (!isExists)
                {
                    _Logger.LogWarn($"Notes with id : {id} was not found.");
                    return NotFound();
                }
                if (notes.Notes != null || notes.Notes != string.Empty)
                {
                    var response = _statusnotes.Update(notes);
                }
                var job = _db.JobModel.Include(e=>e.Companies).Where(x => x.Id == id).FirstOrDefault();
                if(Job.StatusIDs == 2)
                {
                    var emailsendstatus = _emailconfig.SendEmail_jobapprove(job);
                }                
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
                throw;
            }
        }

        // DELETE api/<JobsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            try
            {
                if (id < 1)
                {
                    _Logger.LogWarn($"There is no proper id Passed.");
                    return BadRequest();
                }
                var isExists = await _Jobrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Company with id : {id} was not found.");
                    return NotFound();
                }
                var student = await _Jobrep.FindById(id);
                var isScuccess = await _Jobrep.Delete(student);
                if (!isScuccess)
                {
                    _Logger.LogWarn($"Company deletion failed.");
                    return InternalError($"Something went wrong during Delete!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        //public string StudentCountByJob(int jobid)
        //{

        //}

        private ObjectResult InternalError(string message)
        {
            _Logger.LogError(message);
            return StatusCode(500, "Somethin went wrong. Please contact the administrator.");
        }
    }
}
