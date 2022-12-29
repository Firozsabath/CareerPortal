using CUDJobAPiIdentity.Contracts;
using CUDJobAPiIdentity.VIewModels;
using CUDJobAPiIdentity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CUDJobApiIdentity.VIewModels;
using AutoMapper;
using CUDJobApiIdentity.Models;
using CUDJobApiIdentity.Contracts;
using CUDJobAPiIdentity.Data;
using CUDJobApiIdentity.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CUDJobAPiIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentProfileController : ControllerBase
    {
        public readonly ILoggerService _Logger;
        private readonly IStudentRepository _studentrep;
        private readonly IMapper _Mapper;
        private readonly ISupportFunction _SupportFunctions;
        private readonly ApplicationDbContext _db;
        private readonly IEmailConfig _emailConfig;

        public StudentProfileController(ILoggerService logger, IStudentRepository studentrep, IMapper Mapper, ISupportFunction SupportFunctions,
            ApplicationDbContext db, IEmailConfig emailConfig)
        {
            _Logger = logger;
            _studentrep = studentrep;
            _Mapper = Mapper;
            _SupportFunctions = SupportFunctions;
            _db = db;
            _emailConfig = emailConfig;
        }
        // GET: api/<StudentProfile>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var response = await _studentrep.FindAll();
                //var response = students;
                return Ok(response);
            }
            catch (Exception Ex)
            {
               return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        // GET api/<StudentProfile>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudent(int id)
        {
            try
            {
                var response = await _studentrep.FindById(id);             
                
                if (response == null)
                {
                    _Logger.LogInfo($"The student with id:{id} couldn't be find.");
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception Ex)
            {
               return InternalError($"{Ex.Message} - {Ex.InnerException}");                
            }
        }

        // POST api/<StudentProfile>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStudent(StudentProfile StudentProfile)
        {                     
            try
            {
                if(StudentProfile == null)
                {
                    _Logger.LogWarn($"An empty Student Creation request has been send.");
                    return BadRequest(ModelState);
                }
                if(!ModelState.IsValid)
                {
                    _Logger.LogWarn($"StudentDetails was incomplete.");
                    return BadRequest(ModelState);
                }
                var response = await _studentrep.Create(StudentProfile);
                if (response.StudentPersonal.StudentID == 0)
                {
                    return InternalError($"Student Creation Failed.");
                }
                EmailDTO email = new EmailDTO { Recepient = response.StudentPersonal.EmailID, Subject = "New Student Registered", Type = "NewStudent", Name = response.StudentPersonal.FirstName };
                var emailsend = _emailConfig.SendEmail(email);
                return Ok(response.StudentPersonal.StudentID);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }
        /// <summary>
        /// updates the student Repository
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Student"></param>
        // PUT api/<StudentProfile>/5
        [HttpPut("UpdateStudent/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentProfile StudentProfile)
        {            
            try
            {
                if(id < 1 || StudentProfile == null || id != Convert.ToInt32(StudentProfile.StudentPersonal.StudentID))
                {
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.Update(StudentProfile);
                if(!isScuccess)
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

        [HttpPut("UpdateProfileImg/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProfileImg(int id, [FromBody] StudentSeekers Student)
        {
            try
            {
                var isExists = await _studentrep.isExists(id);
                if (isExists)
                {
                    var Studentdt = new StudentSeekers { StudentID = id, profileImgpath = Student.profileImgpath };
                    _db.Students.Attach(Studentdt);
                    _db.Entry(Studentdt).Property(a => a.profileImgpath).IsModified = true;
                    _db.SaveChanges();
                    return NoContent();
                }
                else
                {
                    return NotFound(id);
                }
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpPut("UpdateStudentResume/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStudentResume(int id, [FromBody] StudentSeekers Student)
        {
            try
            {
                var isExists = await _studentrep.isExists(id);
                if (isExists)
                {
                    var Studentdt = new StudentSeekers { StudentID = id, Resumepath = Student.profileImgpath };
                    _db.Students.Attach(Studentdt);
                    _db.Entry(Studentdt).Property(a => a.Resumepath).IsModified = true;
                    _db.SaveChanges();
                    return NoContent();
                }
                else
                {
                    return NotFound(id);
                }
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        // DELETE api/<StudentProfile>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteStudent(int id)
        {            
            try
            {
                if (id < 1)
                {
                    _Logger.LogWarn($"There is no proper id Passed.");
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                var student = await _studentrep.FindById(id);
                var isScuccess = await _studentrep.Delete(student);
                if (!isScuccess)
                {
                    _Logger.LogWarn($"Student deletion failed .");
                    return InternalError($"Something went wrong during update!");
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
