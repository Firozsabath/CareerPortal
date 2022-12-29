using AutoMapper;
using CUDJobApiIdentity.Contracts;
using CUDJobApiIdentity.DTOs;
using CUDJobApiIdentity.Models;
using CUDJobAPiIdentity.Contracts;
using CUDJobAPiIdentity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace CUDJobApiIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationController : ControllerBase
    {
        private readonly ILoggerService _Logger;
        private readonly IjobApplicationRespository _JobApprep;
        private readonly IMapper _Mapper;
        private readonly ISupportFunction _supportFunction;
        private readonly ApplicationDbContext _db;
        private readonly IEmailConfig _emailConfig;
        public JobApplicationController(ILoggerService logger, IjobApplicationRespository JobApprep, IMapper Mapper, ISupportFunction supportFunction, ApplicationDbContext db,IEmailConfig emailConfig)
        {
            _Logger = logger;
            _JobApprep = JobApprep;
            _Mapper = Mapper;
            _supportFunction = supportFunction;
            _db = db;
            _emailConfig = emailConfig;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetJob(int id)
        {
            try
            {
                var Jobs = await _JobApprep.FindById(id);
                //var response = _supportFunction.ConvertJobDetais(Jobs);
                //if (response == null)
                //{
                //    _Logger.LogInfo($"The Company with id:{id} couldn't be find.");
                //    return NotFound();
                //}
                //return Ok(response);
                return Ok(Jobs);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpGet("Getjobstatus/{id}/{stdid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Getjobstatus(int id,int stdid)
        {
            try
            {
                var Jobs = await _JobApprep.isApplied(id,stdid);           
                return Ok(Jobs);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        // POST api/<JobApplicationController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateJob([FromBody] AppliedJobsDTO AppliedJobs)
        {
            try
            {
                if (AppliedJobs == null)
                {
                    _Logger.LogWarn($"An empty Company Creation request has been send.");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _Logger.LogWarn($"CompanyDetails was incomplete.");
                    return BadRequest(ModelState);
                }
                var isSuccess = await _JobApprep.Create(AppliedJobs);

                //if (isSuccess.ID == 0)
                //{
                //    return InternalError($"Company Creation Failed.");
                //}
                var job = _db.JobModel.Include(e => e.Companies.CompanyContacts).Where(x => x.Id == AppliedJobs.jobID).FirstOrDefault();
                var student = _db.Students.Where(x => x.StudentID == AppliedJobs.StudentID).FirstOrDefault();
                var emailsendstatus = _emailConfig.SendEmail_JobApplication(student, job);
                return Created("Create", new { AppliedJobs });
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpPut("UpdateJobStatus/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateJobStatus(int id, [FromBody] AppliedJobsDTO Appliedjob)
        {
            try
            {
                if(Appliedjob == null)
                {
                    _Logger.LogWarn($"An empty Company Creation request has been send.");
                    return BadRequest(ModelState);
                }                
                var isExists = await _JobApprep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Jobs with id : {id} was not found.");
                    return NotFound();
                }
                var UpdateJob = new AppliedJobs { ID = Appliedjob.ID, jobID = Appliedjob.jobID, Description = Appliedjob.Description, StatusID = Appliedjob.StatusID };
                _db.AppliedJobs.Attach(UpdateJob);
                _db.Entry(UpdateJob).Property(a => a.StatusID).IsModified = true;
                _db.Entry(UpdateJob).Property(a => a.Description).IsModified = true;                
                _db.SaveChanges();
                return NoContent();
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
