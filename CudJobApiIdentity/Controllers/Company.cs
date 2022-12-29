using AutoMapper;
using AutoMapper.Internal;
using CUDJobApiIdentity.Contracts;
using CUDJobApiIdentity.DTOs;
using CUDJobApiIdentity.Models;
using CUDJobApiIdentity.VIewModels;
using CUDJobAPiIdentity.Contracts;
using CUDJobAPiIdentity.Data;
using CUDJobAPiIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CUDJobAPiIdentity.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class Company : ControllerBase
    {
        public readonly ILoggerService _Logger;
        private readonly ICompanyRepository _Companyrep;
        private readonly IMapper _Mapper;
        private readonly ISupportFunction _SupportFunction;
        private readonly IEmailConfig _emailConfig;
        private readonly INotesRepository _statusnotes;
        private readonly ApplicationDbContext _db;
        public Company(ILoggerService logger, 
            ICompanyRepository Companyrep, 
            IMapper Mapper, 
            ISupportFunction SupportFunction,
            IEmailConfig emailConfig,
            INotesRepository statusnotes,
            ApplicationDbContext db
            )
        {
            _Logger = logger;
            _Companyrep = Companyrep;
            _Mapper = Mapper;
            _SupportFunction = SupportFunction;
            _emailConfig = emailConfig;
            _statusnotes = statusnotes;
            _db = db;
        }
        // GET: api/<Company>
        //[Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetComapnies()
        {
            try
            {
                var response = await _Companyrep.FindAll();               
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }
        

        // GET api/<Company>/5
        [HttpGet("GetCompany/{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var response = await _Companyrep.FindById(id);                
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

        // GET api/<Company>/5
        [HttpGet("GetCompanybyuser/{EmailID}")]
        public async Task<IActionResult> GetCompany(string EmailID)
        {
            try
            {
                var response =  _db.Companies.Where(a => a.UserEmailID == EmailID).FirstOrDefault();
                
                return Ok(new { CompanyID =  response.CompanyID });
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        // POST api/<Company>
        [HttpPost]
        //[Route("api/[controller]/Create")]
        public async Task<IActionResult> Create([FromBody] CompanyViewModel companydtls)
        { 
            try
            {
                if (companydtls == null)
                {
                    _Logger.LogWarn($"An empty Company Creation request has been send.");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _Logger.LogWarn($"CompanyDetails was incomplete.");
                    return BadRequest(ModelState);
                }
                var response = await _Companyrep.Create(companydtls);
                if (response.Companies.CompanyID == 0)
                {
                    return InternalError($"Company Creation Failed.");
                }
                EmailDTO email = new EmailDTO { Recepient = response.Companies.UserEmailID, Subject = "New Company Registered", CompanyName = companydtls.Companies.CompanyName,Type = "NewCompany",Name=companydtls.CompanyContacts.FirstName };
                var emailsend =  _emailConfig.SendEmail(email);
                return Created("Create", new { response.Companies.CompanyID });
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpPut("UpdateProfileImg/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProfileImg(int id, [FromBody] CompanyDTO company)
        {
            try
            {
                var companydt = new Companies { CompanyID = id, profileImgpath = company.profileImgpath };
                _db.Companies.Attach(companydt);
                _db.Entry(companydt).Property(a => a.profileImgpath).IsModified = true;
                _db.SaveChanges();
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpPut("UpdateCompanyResume/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCompanyResume(int id, [FromBody] CompanyDTO company)
        {
            try
            {
                var companydt = new Companies { CompanyID = id, Certificatepath = company.profileImgpath, UpdatedDate = DateTime.Now, LicenseExpiryDate = company.LicenseExpiryDate };
                _db.Companies.Attach(companydt);
                _db.Entry(companydt).Property(a => a.Certificatepath).IsModified = true;
                _db.Entry(companydt).Property(a => a.UpdatedDate).IsModified = true;
                _db.Entry(companydt).Property(a => a.LicenseExpiryDate).IsModified = true;
                _db.SaveChanges();
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpPut("Approve/{id}")]        
        public async Task<IActionResult> Approve(int id, [FromBody] CompanyViewModel companydtls)
        {
            try
            {
                var company = new Companies();
                _db.Companies.Attach(company);
                company.CompanyID = id; company.Approved = true; company.UpdatedDate = DateTime.Now;
                _db.Entry(company).Property(a => a.Approved).IsModified = true;
                _db.Entry(company).Property(a => a.UpdatedDate).IsModified = true;
                _db.SaveChanges();               
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
                throw;
            }
        }

        [HttpPut("MassApprove/{ids}")]
        public async Task<IActionResult> MassApprove(string ids, [FromBody] CompanyViewModel companydtls)
        {
            string[] Companyids = ids.Split(',');
            int[] myInts = Array.ConvertAll(Companyids, s => int.Parse(s));
            try
            {
                if (Companyids.Length > 0)
                { 
                    foreach(var id in Companyids)
                    {
                        var comp = _db.Companies.Include(a => a.CompanyContacts).Where(x => x.CompanyID == Convert.ToInt32(id)).FirstOrDefault();                        
                        comp.StatusIDs = 2;
                        _db.Companies.Attach(comp);
                        _db.Entry(comp).Property(a => a.StatusIDs).IsModified = true;
                        _db.SaveChanges();
                        var emailsendstatus = _emailConfig.SendEmail_Companyapprove(comp);
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
                Companies company = new Companies();
                var existingEntity = _db.Companies.First(x => x.CompanyID == id);
                existingEntity.StatusIDs = notes.StatusID;
                existingEntity.UpdatedDate = DateTime.Now;
                company = _Mapper.Map<Companies>(existingEntity);
                _db.Companies.Update(company);
                _db.SaveChanges();

                var companydt = _db.Companies.Include(e => e.CompanyContacts).Where(x => x.CompanyID == id).FirstOrDefault();
                if(notes.StatusID == '2')
                {
                    var emailsendstatus = _emailConfig.SendEmail_Companyapprove(companydt); 
                }                
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

                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
                throw;
            }
        }

        [HttpGet("GetCompanyforApproval")]
        public async Task<IActionResult> GetComapniesforApproval()
        {
            try
            {
                var Companies = await _db.Companies
               .Include(a => a.CompanyContacts)
               .Include(a=>a.companycategory)
               .Include(a => a.addresses)
               .ThenInclude(a => a.Address).Where(a=>a.StatusIDs != 2).ToListAsync();
                var response = _SupportFunction.ConvertToCompanyViewModelList(Companies);                
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }


        //[Route("api/[controller]/[action]")]
        // PUT api/<Company>/5
        [HttpPut("UpdateCompany/{id}")]        
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyViewModel companydtls)
        {           
            try
            {
                if (id < 1 || companydtls == null || id != companydtls.Companies.CompanyID)
                //if (companydtls.Companies.CompanyID<1)
                {
                    return BadRequest();
                }
                var isExists = await _Companyrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Company with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _Companyrep.Update(companydtls);
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

        // DELETE api/<Company>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                if (id < 1)
                {
                    _Logger.LogWarn($"There is no proper id Passed.");
                    return BadRequest();
                }
                var isExists = await _Companyrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Company with id : {id} was not found.");
                    return NotFound();
                }
                var student = await _Companyrep.FindById(id);
                var isScuccess = await _Companyrep.Delete(student);
                if (!isScuccess)
                {
                    _Logger.LogWarn($"Company deletion failed .");
                    return InternalError($"Something went wrong during Delete!");
                }
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
